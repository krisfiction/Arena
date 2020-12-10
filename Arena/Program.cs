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
        private static readonly Random random = new Random();

        public static void Main()
        {
            Console.Title = "Arena";
            Console.SetWindowSize(140, 46); //map will be 110x45, giving 30 spaces on the side and 10 lines below, +1 to prevent scroll on window
            Console.CursorVisible = false; //to hide the cursor
            Console.Clear();


            Map map = new Map();

            map.FillMap();
            map.FillRooms();

            map.Create();


            //// make at least 4 rooms
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


        //public List<Monster> activeMonsters
        //{
        //    get { return activeMonsters};
        //}

            //generate 5 monsters
            for (int i = 0; i < 5; i++)
            {
                activeMonsters.Add(Generate.Monster());
            }

            //add monsters to map
            for (int i = 0; i < activeMonsters.Count; i++)
            {
                //map.PlaceMonster(activeMonsters[i]);


                var (x, y) = map.PlaceMonster(activeMonsters[i]);

                activeMonsters[i].X = x;
                activeMonsters[i].Y = y;
            }

            //monsters are added to the map though they are inactive
            //this may need moved

            //add to map.MovePlayer() to check which monster 
            //replace 1 with variable
            if (activeMonsters[1].Y == 2 && activeMonsters[1].X == 2) //2 should be NextTile.X and NextTile.Y 
            {
                Combat.PlayerAttacks(player, activeMonsters[1], map);
            }


            map.Display();


            // for testing
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
                    //Array.Clear(map.GameMap, 0, map.GameMap.Length);
                    ActivityLog.ClearLog();
                    Main();
                }
                if (aInput == ConsoleKey.UpArrow || aInput == ConsoleKey.NumPad8)
                {
                    map.MovePlayer("North", player, map, activeMonsters);
                }
                if (aInput == ConsoleKey.DownArrow || aInput == ConsoleKey.NumPad2)
                {
                    map.MovePlayer("South", player, map, activeMonsters);
                }
                if (aInput == ConsoleKey.RightArrow || aInput == ConsoleKey.NumPad6)
                {
                    map.MovePlayer("East", player, map, activeMonsters);
                }
                if (aInput == ConsoleKey.LeftArrow || aInput == ConsoleKey.NumPad4)
                {
                    map.MovePlayer("West", player, map, activeMonsters);
                }
                if (aInput == ConsoleKey.NumPad9)
                {
                    map.MovePlayer("NorthEast", player, map, activeMonsters);
                }
                if (aInput == ConsoleKey.NumPad7)
                {
                    map.MovePlayer("NorthWest", player, map, activeMonsters);
                }
                if (aInput == ConsoleKey.NumPad3)
                {
                    map.MovePlayer("SouthEast", player, map, activeMonsters);
                }
                if (aInput == ConsoleKey.NumPad1)
                {
                    map.MovePlayer("SouthWest", player, map, activeMonsters);
                }
                if (aInput == ConsoleKey.Oem3) // cheat console activation key / for possible cheat codes
                {
                    ActivityLog.AddToLog("cheater!");
                }
                map.Display();
                ActivityLog.Display();
            } while (_keepPlaying);

        }

    }
}
