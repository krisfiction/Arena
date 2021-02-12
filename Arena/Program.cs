using Arena.Characters;
using Arena.Generator;
using Arena.Items;
using Arena.Items.Potions;
using Arena.Items.Scrolls;
using Arena.MapGenerator;
using System;
using System.Collections.Generic;

/*
 * Arena
 * a testing room
 */

namespace Arena
{
    internal static class Program
    {
        private static List<Item> activeItems = new List<Item>();
        private static readonly Map map = new Map();

        public static void Main()
        {
            activeItems.Clear();

            Console.Title = "Arena";
            Console.SetWindowSize(140, 46); //map will be 110x45, giving 30 spaces on the side and 10 lines below, +1 to prevent scroll on window
            Console.CursorVisible = false; //to hide the cursor
            Console.Clear();

            // Map map = new Map();

            map.FillMap();
            map.FillRooms();

            //create 1 room or create entire map
            //map.CreateOneRoom();
            map.Create();

            // make at least 4 rooms
            if (map.NumberOfRooms < 4)
            {
                Main();
            }

            if (map.CheckLoneRooms())
            {
                Main();
            }


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

            //List<Item> activeItems = new List<Item>();

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
                bool _moveKeyPressed = false;
                string _moveKeyDirection = null;

                if (aInput == ConsoleKey.F5) //reload for testing
                {
                    ActivityLog.ClearLog();
                    Main();
                }

                // move keys
                if (aInput == ConsoleKey.UpArrow || aInput == ConsoleKey.NumPad8)
                {
                    _moveKeyPressed = true;
                    _moveKeyDirection = "North";
                }
                if (aInput == ConsoleKey.DownArrow || aInput == ConsoleKey.NumPad2)
                {
                    _moveKeyPressed = true;
                    _moveKeyDirection = "South";
                }
                if (aInput == ConsoleKey.RightArrow || aInput == ConsoleKey.NumPad6)
                {
                    _moveKeyPressed = true;
                    _moveKeyDirection = "East";
                }
                if (aInput == ConsoleKey.LeftArrow || aInput == ConsoleKey.NumPad4)
                {
                    _moveKeyPressed = true;
                    _moveKeyDirection = "West";
                }
                if (aInput == ConsoleKey.NumPad9)
                {
                    _moveKeyPressed = true;
                    _moveKeyDirection = "NorthEast";
                }
                if (aInput == ConsoleKey.NumPad7)
                {
                    _moveKeyPressed = true;
                    _moveKeyDirection = "NorthWest";
                }
                if (aInput == ConsoleKey.NumPad3)
                {
                    _moveKeyPressed = true;
                    _moveKeyDirection = "SouthEast";
                }
                if (aInput == ConsoleKey.NumPad1)
                {
                    _moveKeyPressed = true;
                    _moveKeyDirection = "SouthWest";
                }
                if (_moveKeyPressed) //if a moved key is pressed do this
                {
                    map.MovePlayer(_moveKeyDirection, player, map, activeMonsters, activeItems);
                    map.MoveMonster(map, activeMonsters);
                }

                if (aInput == ConsoleKey.Oem3) // cheat console activation key / for possible cheat codes
                {
                    ActivityLog.AddToLog("cheater!");
                }

                if (aInput == ConsoleKey.I) //inventory
                {
                    Console.Clear();
                    Inventory.Loop(player, map, "all");
                }
                if (aInput == ConsoleKey.Q) //quaff potion
                {
                    Console.Clear();
                    Inventory.Loop(player, map, "potion");
                }
                if (aInput == ConsoleKey.R) //read scroll
                {
                    Console.Clear();
                    Inventory.Loop(player, map, "scroll");
                }

                //! curently broken --> seems fixed - set CurrentTile.IsItem = false on pickup
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

                            map.ProcessItemTile(player); //set tile.IsItem to false
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