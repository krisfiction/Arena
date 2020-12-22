using System;
using Arena.Characters;
using Arena.MapGenerator;
using Arena.Characters.Monsters;
using System.Collections.Generic;
using Arena.Generator;
using Arena.Items;
using Arena.Items.Potions;
using Arena.Items.Scrolls;



/*
 * Arena
 * a testing room
 */


namespace Arena
{
    internal static class Program
    {
        public static void Main()
        {
            Console.Title = "Arena";
            Console.SetWindowSize(140, 46); //map will be 110x45, giving 30 spaces on the side and 10 lines below, +1 to prevent scroll on window
            Console.CursorVisible = false; //to hide the cursor
            Console.Clear();

            Map map = new Map();

            map.FillMap();
            map.FillRooms();

            //create 1 room or create entire map
            map.CreateOneRoom();
            //map.Create();

            // make at least 4 rooms
            //if (map.NumberOfRooms < 4)
            //{
            //    Main();
            //}

            map.CreateHallways();



            Player player = new Player
            {
                Name = "Tunk",
                HealthMax = 100,
                Health = 100
            };
            map.PlacePlayer(player);



            //Potion potion = new Potion();
            //potion.Cast();

            //Health health = new Health(4, 5); // 4,5 are pos X, Y
            //health.Cast();


            List<Item> activeItems = new List<Item>();

            //generate 6 items
            for (int i = 0; i < 3; i++)
            {
                activeItems.Add(Generate.Potion());
                activeItems.Add(Generate.Scroll());
            }

            //add items to map
            for (int i = 0; i < activeItems.Count; i++)
            {
                var (x, y) = map.PlaceItem(activeItems[i]);

                activeItems[i].X = x;
                activeItems[i].Y = y;
            }

            //? combine both of these loops

            List<Monster> activeMonsters = new List<Monster>();

            //generate 5 monsters
            for (int i = 0; i < 5; i++)
            {
                activeMonsters.Add(Generate.Monster());
            }

            //add monsters to map
            for (int i = 0; i < activeMonsters.Count; i++)
            {
                var (x, y) = map.PlaceMonster(activeMonsters[i]);

                activeMonsters[i].X = x;
                activeMonsters[i].Y = y;
            }


            map.Display();


            //for testing to fill log with text
            //for (int i = 0; i < 6; i++)
            //{
            //    ActivityLog.AddToLog("log " + i);
            //}
            //ActivityLog.AddToLog("this is the activity / combat log");

            Inventory inventory = new Inventory();
            inventory.Initialize();

            ActivityLog.Display();

            StartGame(player, map, activeMonsters, activeItems);


            
        }

        public static void StartGame(Player player, Map map, List<Monster> activeMonsters, List<Item> activeItems)
        {

            const bool _keepPlaying = true;
            do
            {
                ConsoleKey aInput = Console.ReadKey(true).Key; //true hides input
                if (aInput == ConsoleKey.F5) //reload for testing
                {
                    ActivityLog.ClearLog();
                    Main();
                }
                if (aInput == ConsoleKey.UpArrow || aInput == ConsoleKey.NumPad8)
                {
                    if (map.MovePlayer("North", player, map, activeMonsters, activeItems))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.DownArrow || aInput == ConsoleKey.NumPad2)
                {
                    if (map.MovePlayer("South", player, map, activeMonsters, activeItems))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.RightArrow || aInput == ConsoleKey.NumPad6)
                {
                    if (map.MovePlayer("East", player, map, activeMonsters, activeItems))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.LeftArrow || aInput == ConsoleKey.NumPad4)
                {
                    if (map.MovePlayer("West", player, map, activeMonsters, activeItems))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.NumPad9)
                {
                    if (map.MovePlayer("NorthEast", player, map, activeMonsters, activeItems))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.NumPad7)
                {
                    if (map.MovePlayer("NorthWest", player, map, activeMonsters, activeItems))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.NumPad3)
                {
                    if (map.MovePlayer("SouthEast", player, map, activeMonsters, activeItems))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.NumPad1)
                {
                    if (map.MovePlayer("SouthWest", player, map, activeMonsters, activeItems))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.Oem3) // cheat console activation key / for possible cheat codes
                {
                    ActivityLog.AddToLog("cheater!");
                }



                if (aInput == ConsoleKey.I) //inventory
                {
                    Console.Clear();
                    Inventory.Loop("all");
                }
                if (aInput == ConsoleKey.C) //cast potion
                {
                    Console.Clear();
                    Inventory.Loop("potion");
                }
                if (aInput == ConsoleKey.R) //read scroll
                {
                    Console.Clear();
                    Inventory.Loop("scroll");
                }



                if (aInput == ConsoleKey.G) //get item
                {
                    for (int i = 0; i < activeItems.Count; i++)
                    {
                        if (activeItems[i].X == player.X && activeItems[i].Y == player.Y)
                        {
                            ActivityLog.AddToLog("you pick up " + activeItems[i].Name);
                            
                            if (activeItems[i].Type == "Potion")
                            {
                                Inventory.PotionInventory.Add((Health)activeItems[i]);
                            }
                            if (activeItems[i].Type == "Scroll")
                            {
                                Inventory.ScrollInventory.Add((Teleportation)activeItems[i]);
                            }

                            activeItems.RemoveAt(i); // remove item from active list
                        }
                    }
                }


                
                //testing
                //for (int i = 0; i < activeMonsters.Count; i++)
                //{
                //    ActivityLog.AddToLog($"{i, 2}) {activeMonsters[i].Name, -10} X: {activeMonsters[i].X, 2} Y: {activeMonsters[i].Y, 2}");
                //}


                map.Display();
                ActivityLog.Display();
                StatBar.Display(player);
            } while (_keepPlaying);

        }
    }
}
