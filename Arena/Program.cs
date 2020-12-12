using System;
using Arena.Characters;
using Arena.MapGenerator;
using Arena.Characters.Monsters;
using System.Collections.Generic;

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

            //testing - display monsters with X,Y cords
            //for (int i = 0; i < activeMonsters.Count; i++)
            //{
            //    ActivityLog.AddToLog($"{i}) {activeMonsters[i].Name} X: {activeMonsters[i].X} Y:  {activeMonsters[i].Y}");
            //}

            map.Display();


            //for testing to fill log with text
            //for (int i = 0; i < 6; i++)
            //{
            //    ActivityLog.AddToLog("log " + i);
            //}
            //ActivityLog.AddToLog("this is the activity / combat log");



            ActivityLog.Display();

            StartGame(player, map, activeMonsters);


            
        }

        public static void StartGame(Player player, Map map, List<Monster> activeMonsters)
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
                    if (map.MovePlayer("North", player, map, activeMonsters))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.DownArrow || aInput == ConsoleKey.NumPad2)
                {
                    if (map.MovePlayer("South", player, map, activeMonsters))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.RightArrow || aInput == ConsoleKey.NumPad6)
                {
                    if (map.MovePlayer("East", player, map, activeMonsters))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.LeftArrow || aInput == ConsoleKey.NumPad4)
                {
                    if (map.MovePlayer("West", player, map, activeMonsters))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.NumPad9)
                {
                    if (map.MovePlayer("NorthEast", player, map, activeMonsters))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.NumPad7)
                {
                    if (map.MovePlayer("NorthWest", player, map, activeMonsters))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.NumPad3)
                {
                    if (map.MovePlayer("SouthEast", player, map, activeMonsters))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.NumPad1)
                {
                    if (map.MovePlayer("SouthWest", player, map, activeMonsters))
                    {
                        map.MoveMonster(map, activeMonsters);
                    }
                }
                if (aInput == ConsoleKey.Oem3) // cheat console activation key / for possible cheat codes
                {
                    ActivityLog.AddToLog("cheater!");
                }

                //testing
                for (int i = 0; i < activeMonsters.Count; i++)
                {
                    ActivityLog.AddToLog($"{i, 2}) {activeMonsters[i].Name, -10} X: {activeMonsters[i].X, 2} Y: {activeMonsters[i].Y, 2}");
                }


                map.Display();
                ActivityLog.Display();
            } while (_keepPlaying);

        }
    }
}
