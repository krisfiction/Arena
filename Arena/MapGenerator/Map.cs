using Arena.Characters;
using Arena;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arena.MapGenerator
{
    public class Map
    {
        private readonly string WallxIcon = "═";
        private readonly string WallyIcon = "║";
        private readonly string FloorIcon = "·"; // unicode #183 middle dot 

        public string NWcornerIcon = "╔";
        public string NEcornerIcon = "╗";
        public string SWcornerIcon = "╚";
        public string SEcornerIcon = "╝";

       
        private readonly string PlayerIcon = "@"; // move ?

        private const int MapSizeX = 110;
        private const int MapSizeY = 35;

        private readonly Tile[,] GameMap = new Tile[MapSizeX, MapSizeY];

        private readonly Room[,] rooms = new Room[3, 3];

        private int RoomNumber = 1;

        public int NumberOfRooms = 0;


        public void Create()
        {
            const int RoomHeight = 15; //y
            const int RoomWidth = 20; //x

            const int RoomPOSX = 0;
            const int RoomPOSY = 0;

            rooms[0, 0] = new Room(1, RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
            CreateRoom(RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
        }




        public void CreateRoom(int RoomPOSX, int RoomPOSY, int RoomHeight, int RoomWidth)
        {
            NumberOfRooms++;

            for (int y = 0; y <= RoomHeight; y++)
            {
                for (int x = 0; x <= RoomWidth; x++)
                {
                    //apply walls
                    if (y == 0 || y == RoomHeight) // "═"
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(x, y, WallxIcon, false, false, false);
                    }
                    else if (x == 0 || x == RoomWidth) // "║"
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(x, y, WallyIcon, false, false, false);
                    }
                    //apply floors
                    else // "."
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(x, y, FloorIcon, true, false, false);
                    }

                    // apply corner walls
                    if (x == 0 && y == 0)
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(x, y, NWcornerIcon, false, false, false);
                    }
                    if (y == 0 && x == RoomWidth)
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(x, y, NEcornerIcon, false, false, false);
                    }
                    if (y == RoomHeight && x == RoomWidth)
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(x, y, SEcornerIcon, false, false, false);
                    }
                    if (y == RoomHeight && x == 0)
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(x, y, SWcornerIcon, false, false, false);
                    }

                }
            }

            RoomNumber++;
        }






        public void FillMap()
        {
            for (int x = 0; x <= MapSizeX - 1; x++)
            {
                for (int y = 0; y <= MapSizeY - 1; y++)
                {
                    GameMap[x, y] = new Tile(x, y, " ", false, false, false);
                }
            }
        }


        public void FillRooms()
        {
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    //! fill array with blank rooms
                    rooms[x, y] = new Room(0, 0, 0, 0, 0);
                }
            }
        }








        public void Display()
        {
            for (int x = 0; x <= MapSizeX - 1; x++)
            {
                for (int y = 0; y <= MapSizeY - 1; y++)
                {
                    Tile CurrentTile = (Tile)GameMap[x, y];
                    string _icon = CurrentTile.Icon;


                    if (_icon == PlayerIcon) //set player icon to blue
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(_icon);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (_icon == "M") //set monster icon to red
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(_icon);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.SetCursorPosition(x, y);
                        Console.WriteLine(_icon);
                    }
                }
            }
        }


        public void PlacePlayer(Player player)
        {
            Random random = new Random();
            int _placed = 0;

            do
            {
                int _randX = random.Next(0, MapSizeX);
                int _randY = random.Next(0, MapSizeY);

                Tile CurrentTile = (Tile)GameMap[_randX, _randY];
                bool _iswalkable = CurrentTile.IsWalkable;
                bool _ishallway = CurrentTile.IsHallway;

                if (_iswalkable && _placed == 0 && !_ishallway)
                {
                    CurrentTile.Icon = PlayerIcon;

                    player.X = _randX;
                    player.Y = _randY;
                    _placed = 1;

                    

                }
            } while (_placed == 0);

            //DisplayPlayerPosition();
            StatBar.Display(player);


        }


        public (int X, int Y) PlaceMonster(Monster monster)
        {
            Random random = new Random();
            int _placed = 0;

            do
            {
                int _randX = random.Next(0, MapSizeX);
                int _randY = random.Next(0, MapSizeY);

                Tile CurrentTile = (Tile)GameMap[_randX, _randY];
                bool _iswalkable = CurrentTile.IsWalkable;
                bool _ishallway = CurrentTile.IsHallway;

                if (_iswalkable && _placed == 0 && !_ishallway)
                {
                    CurrentTile.Icon = monster.Icon;
                    _placed = 1;
                    CurrentTile.IsMonster = true;
                    CurrentTile.IsWalkable = false;

                    monster.X = _randX;
                    monster.Y = _randY;

                }
            } while (_placed == 0);
            return (monster.X, monster.Y);

        }

        public void RemoveMonster(Monster monster)
        {
            //code to remove a monster after it has been killed
            

            Tile CurrentTile = (Tile)GameMap[monster.X, monster.Y];

            CurrentTile.IsWalkable = true;

            CurrentTile.Icon = FloorIcon;
            Display();

        }


        //public void DisplayPlayerPosition(Player player)
        //{
        //    Console.SetCursorPosition(110, 0);
        //    Console.WriteLine($"Player Position: x{player.X}, y{player.Y} ");
        //    StatBar.Display(player);
        //}


        //public void MovePlayer(string _direction, Player player, Monster monster, Map map)
        public void MovePlayer(string _direction, Player player, Map map, List<Monster> activeMonsters)
        {
            Tile CurrentTile = GameMap[player.X, player.Y];

            if (_direction == "North")
            {
                Tile NextTile = GameMap[player.X, player.Y - 1];

                if (NextTile.IsWalkable)
                {
                    CurrentTile.Icon = FloorIcon;
                    NextTile.Icon = PlayerIcon;
                    player.Y--;
                    StatBar.Display(player);
                    ActivityLog.AddToLog("You move " + _direction + ".");//! needs to fix space formatting - if you attack then move the attack text will still be visible
                }
                else if (NextTile.IsMonster)
                {
                    ActivityLog.AddToLog("You attack!"); //! needs to fix space formatting - see above

                    //for (int i = 0; i < activeMonsters.Count; i++)
                    //{
                    //    ActivityLog.AddToLog($"monster {i} - {activeMonsters[i].X}, {activeMonsters[i].Y}");
                    //}


                    //Combat.PlayerAttacks(player, monster, map);

                    for (int i = 0; i < activeMonsters.Count; i++)
                    {
                        if (activeMonsters[i].Y == NextTile.Y && activeMonsters[i].X == NextTile.X)
                        {
                            Combat.PlayerAttacks(player, activeMonsters[i], map);
                        }
                    }



                }
                else
                {
                    ActivityLog.AddToLog("You can't move that way.");
                }
            }
            if (_direction == "South")
            {
                Tile NextTile = GameMap[player.X, player.Y + 1];

                if (NextTile.IsWalkable)
                {
                    CurrentTile.Icon = FloorIcon;
                    NextTile.Icon = PlayerIcon;
                    player.Y++;
                    StatBar.Display(player);
                    ActivityLog.AddToLog("You move " + _direction + ".");
                }
                else if (NextTile.IsMonster)
                {
                    ActivityLog.AddToLog("You attack!");
                    for (int i = 0; i < activeMonsters.Count; i++)
                    {
                        if (activeMonsters[i].Y == NextTile.Y && activeMonsters[i].X == NextTile.X)
                        {
                            Combat.PlayerAttacks(player, activeMonsters[i], map);
                        }
                    }
                }
                else
                {
                    ActivityLog.AddToLog("You can't move that way.");
                }
            }
            if (_direction == "West")
            {
                Tile NextTile = (Tile)GameMap[player.X - 1, player.Y];

                if (NextTile.IsWalkable)
                {
                    CurrentTile.Icon = FloorIcon;
                    NextTile.Icon = PlayerIcon;
                    player.X--;
                    StatBar.Display(player);
                    ActivityLog.AddToLog("You move " + _direction + ".");
                }
                else if (NextTile.IsMonster)
                {
                    ActivityLog.AddToLog("You attack!");
                    for (int i = 0; i < activeMonsters.Count; i++)
                    {
                        if (activeMonsters[i].Y == NextTile.Y && activeMonsters[i].X == NextTile.X)
                        {
                            Combat.PlayerAttacks(player, activeMonsters[i], map);
                        }
                    }
                }
                else
                {
                    ActivityLog.AddToLog("You can't move that way.");
                }
            }
            if (_direction == "East")
            {
                Tile NextTile = (Tile)GameMap[player.X + 1, player.Y];

                
                if (NextTile.IsWalkable)
                {
                    CurrentTile.Icon = FloorIcon;
                    NextTile.Icon = PlayerIcon;
                    player.X++;
                    StatBar.Display(player);
                    ActivityLog.AddToLog("You move " + _direction + ".");
                }
                else if (NextTile.IsMonster)
                {
                    ActivityLog.AddToLog("You attack!");
                    for (int i = 0; i < activeMonsters.Count; i++)
                    {
                        if (activeMonsters[i].Y == NextTile.Y && activeMonsters[i].X == NextTile.X)
                        {
                            Combat.PlayerAttacks(player, activeMonsters[i], map);
                        }
                    }
                }
                else
                {
                    ActivityLog.AddToLog("You can't move that way.");
                }
            }
            if (_direction == "NorthWest")
            {
                Tile NextTile = (Tile)GameMap[player.X - 1, player.Y - 1];
               
                if (NextTile.IsWalkable)
                {
                    CurrentTile.Icon = FloorIcon;
                    NextTile.Icon = PlayerIcon;
                    player.X--;
                    player.Y--;
                    StatBar.Display(player);
                    ActivityLog.AddToLog("You move " + _direction + ".");
                }
                else if (NextTile.IsMonster)
                {
                    ActivityLog.AddToLog("You attack!");
                    for (int i = 0; i < activeMonsters.Count; i++)
                    {
                        if (activeMonsters[i].Y == NextTile.Y && activeMonsters[i].X == NextTile.X)
                        {
                            Combat.PlayerAttacks(player, activeMonsters[i], map);
                        }
                    }
                }
                else
                {
                    ActivityLog.AddToLog("You can't move that way.");
                }
            }
            if (_direction == "NorthEast")
            {
                Tile NextTile = (Tile)GameMap[player.X + 1, player.Y - 1];

                if (NextTile.IsWalkable)
                {
                    CurrentTile.Icon = FloorIcon;
                    NextTile.Icon = PlayerIcon;
                    player.X++;
                    player.Y--;
                    StatBar.Display(player);
                    ActivityLog.AddToLog("You move " + _direction + ".");
                }
                else if (NextTile.IsMonster)
                {
                    ActivityLog.AddToLog("You attack!");
                    for (int i = 0; i < activeMonsters.Count; i++)
                    {
                        if (activeMonsters[i].Y == NextTile.Y && activeMonsters[i].X == NextTile.X)
                        {
                            Combat.PlayerAttacks(player, activeMonsters[i], map);
                        }
                    }
                }
                else
                {
                    ActivityLog.AddToLog("You can't move that way.");
                }
            }
            if (_direction == "SouthWest")
            {
                Tile NextTile = (Tile)GameMap[player.X - 1, player.Y + 1];

                if (NextTile.IsWalkable)
                {
                    CurrentTile.Icon = FloorIcon;
                    NextTile.Icon = PlayerIcon;
                    player.X--;
                    player.Y++;
                    StatBar.Display(player);
                    ActivityLog.AddToLog("You move " + _direction + ".");
                }
                else if (NextTile.IsMonster)
                {
                    ActivityLog.AddToLog("You attack!");
                    for (int i = 0; i < activeMonsters.Count; i++)
                    {
                        if (activeMonsters[i].Y == NextTile.Y && activeMonsters[i].X == NextTile.X)
                        {
                            Combat.PlayerAttacks(player, activeMonsters[i], map);
                        }
                    }
                }
                else
                {
                    ActivityLog.AddToLog("You can't move that way.");
                }
            }
            if (_direction == "SouthEast")
            {
                Tile NextTile = (Tile)GameMap[player.X + 1, player.Y + 1];

                if (NextTile.IsWalkable)
                {
                    CurrentTile.Icon = FloorIcon;
                    NextTile.Icon = PlayerIcon;
                    player.X++;
                    player.Y++;
                    StatBar.Display(player);
                    ActivityLog.AddToLog("You move " + _direction + ".");
                }
                else if (NextTile.IsMonster)
                {
                    ActivityLog.AddToLog("You attack!");
                    for (int i = 0; i < activeMonsters.Count; i++)
                    {
                        if (activeMonsters[i].Y == NextTile.Y && activeMonsters[i].X == NextTile.X)
                        {
                            Combat.PlayerAttacks(player, activeMonsters[i], map);
                        }
                    }
                }
                else
                {
                    ActivityLog.AddToLog("You can't move that way.");
                }
            }
        }
    }
}
