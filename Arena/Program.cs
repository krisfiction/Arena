using System;
using Arena.Characters;
using Arena.MapGenerator;
using Arena.Characters.Monsters;

/*
 * Arena
 * a testing room
 */


namespace Arena
{
    internal static class Program
    {
        private static readonly Random random = new Random();

        private static void Main()
        {


            Player player = new Player
            {
                Name = "Tunk",
                HealthMax = 100,
                Health = 100
            };

            Monster monster = new Monster(); // for testing needs moved to generation class

            switch (random.Next(1, 3))
            {
                case 1:
                    Rat rat = new Rat();
                    monster.Name = rat.Name;
                    monster.Icon = rat.Icon;
                    //monster.HealthMax = monster.Health = random.Next(rat.HealthLow, rat.HealthHigh);
                    monster.HealthMax = monster.Health = rat.SetHealth();
                    monster.WeaponDamageLow = rat.WeaponDamageLow;
                    monster.WeaponDamageHigh = rat.WeaponDamageHigh;
                    break;
                case 2:
                    Skeleton skeleton = new Skeleton();
                    monster.Name = skeleton.Name;
                    monster.Icon = skeleton.Icon;
                    //monster.HealthMax = monster.Health = random.Next(skeleton.HealthLow, skeleton.HealthHigh);
                    monster.HealthMax = monster.Health = skeleton.SetHealth();
                    monster.WeaponDamageLow = skeleton.WeaponDamageLow;
                    monster.WeaponDamageHigh = skeleton.WeaponDamageHigh;
                    monster.Gold = skeleton.Gold;
                    break;
            }

              


        Console.Title = "Arena";
            Console.SetWindowSize(140, 46); //map will be 110x45, giving 30 spaces on the side and 10 lines below, +1 to prevent scroll on window
            Console.CursorVisible = false; //to hide the cursor
            Console.Clear();

            //Console.SetCursorPosition(110, 34);
            //Console.WriteLine(("").PadLeft(30, '#'));

            Map map = new Map();

            map.FillMap();
            map.FillRooms();

            map.Create();

            map.PlacePlayer(player);
            map.PlaceMonster(monster);


            map.Display();


            // for testing
            for (int i = 0; i < 6; i++)
            {
                ActivityLog.AddToLog("log " + i);
            }
            ActivityLog.AddToLog("this is the activity / combat log");



            ActivityLog.Display();

            StartGame(player, monster, map);


            
        }

        public static void StartGame(Player player, Monster monster, Map map)
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
                    map.MovePlayer("North", player, monster, map);
                }
                if (aInput == ConsoleKey.DownArrow || aInput == ConsoleKey.NumPad2)
                {
                    map.MovePlayer("South", player, monster, map);
                }
                if (aInput == ConsoleKey.RightArrow || aInput == ConsoleKey.NumPad6)
                {
                    map.MovePlayer("East", player, monster, map);
                }
                if (aInput == ConsoleKey.LeftArrow || aInput == ConsoleKey.NumPad4)
                {
                    map.MovePlayer("West", player, monster, map);
                }
                if (aInput == ConsoleKey.NumPad9)
                {
                    map.MovePlayer("NorthEast", player, monster, map);
                }
                if (aInput == ConsoleKey.NumPad7)
                {
                    map.MovePlayer("NorthWest", player, monster, map);
                }
                if (aInput == ConsoleKey.NumPad3)
                {
                    map.MovePlayer("SouthEast", player, monster, map);
                }
                if (aInput == ConsoleKey.NumPad1)
                {
                    map.MovePlayer("SouthWest", player, monster, map);
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
