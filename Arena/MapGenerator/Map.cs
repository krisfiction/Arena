﻿using Arena.Characters;
using Arena.Items;
using Arena.Generator;
using System;
using System.Collections.Generic;

namespace Arena.MapGenerator
{
    public class Map
    {
        private readonly string WallxIcon = "═";
        private readonly string WallyIcon = "║";
        private readonly string FloorIcon = "·"; // unicode #183 middle dot
        private readonly string DoorIcon = "+";

        public string NWcornerIcon = "╔";
        public string NEcornerIcon = "╗";
        public string SWcornerIcon = "╚";
        public string SEcornerIcon = "╝";

        //private Tile PreviousTile; //! may need for replacing item icon if player chooses not to pick item up

        private readonly string PlayerIcon = "@"; // move ?

        private const int MapSizeX = 110;
        private const int MapSizeY = 35;

        private readonly Tile[,] GameMap = new Tile[MapSizeX, MapSizeY];

        private readonly Room[,] rooms = new Room[3, 3];

        //private int RoomNumber = 1;

        public int NumberOfRooms = 0;
        public int NumberOfHallways = 0;

        public void Build()
        {
            FillMap();
            FillRooms();
            Create();
            CreateHallways();

            if (CheckLoneRooms())
            {
                Reset();
                Build();
            }

            if (NumberOfRooms < 4 || NumberOfHallways < 3)
            {
                Reset();
                Build();
            }
        }

        public void Reset()
        {
            NumberOfRooms = 0;
            NumberOfHallways = 0;
            GameMap.Initialize();
            rooms.Initialize();
        }


        public void CreateOneRoom() //create 1 room for testing
        {
            const int RoomHeight = 20; //y
            const int RoomWidth = 25; //x

            const int RoomPOSX = 0;
            const int RoomPOSY = 0;

            rooms[0, 0] = new Room(1, RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
            CreateRoom(RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
        }

        public void Create()
        {
            Random random = new Random();

            //! 3x3 room structure - may enlarge in the future

            // row 0
            int buildRoom = random.Next(1, 3);
            if (buildRoom == 1)
            {
                int RoomHeight = random.Next(3, 9);
                int RoomWidth = random.Next(3, 34);

                int RoomPOSX = random.Next(0, 34 - 1 - RoomWidth);
                int RoomPOSY = random.Next(0, 9 - 1 - RoomHeight);

                rooms[0, 0] = new Room(1, RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
                CreateRoom(RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
            }

            buildRoom = random.Next(1, 3);
            if (buildRoom == 1)
            {
                int RoomHeight = random.Next(3, 9);
                int RoomWidth = random.Next(3, 34);

                int RoomPOSX = random.Next(38, 72 - 1 - RoomWidth);
                int RoomPOSY = random.Next(0, 9 - 1 - RoomHeight);

                rooms[0, 1] = new Room(2, RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
                CreateRoom(RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
            }

            buildRoom = random.Next(1, 3);
            if (buildRoom == 1)
            {
                int RoomHeight = random.Next(3, 9);
                int RoomWidth = random.Next(3, 34);

                int RoomPOSX = random.Next(76, 110 - 1 - RoomWidth);
                int RoomPOSY = random.Next(0, 9 - 1 - RoomHeight);

                rooms[0, 2] = new Room(3, RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
                CreateRoom(RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
            }

            // row 1
            buildRoom = random.Next(1, 3);
            if (buildRoom == 1)
            {
                int RoomHeight = random.Next(3, 9);
                int RoomWidth = random.Next(3, 34);

                int RoomPOSX = random.Next(0, 34 - 1 - RoomWidth);
                int RoomPOSY = random.Next(13, 22 - 1 - RoomHeight);

                rooms[1, 0] = new Room(4, RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
                CreateRoom(RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
            }

            buildRoom = random.Next(1, 3);
            if (buildRoom == 1)
            {
                int RoomHeight = random.Next(3, 9);
                int RoomWidth = random.Next(3, 34);

                int RoomPOSX = random.Next(38, 72 - 1 - RoomWidth);
                int RoomPOSY = random.Next(13, 22 - 1 - RoomHeight);

                rooms[1, 1] = new Room(5, RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
                CreateRoom(RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
            }

            buildRoom = random.Next(1, 3);
            if (buildRoom == 1)
            {
                int RoomHeight = random.Next(3, 9);
                int RoomWidth = random.Next(3, 34);

                int RoomPOSX = random.Next(76, 110 - 1 - RoomWidth);
                int RoomPOSY = random.Next(13, 22 - 1 - RoomHeight);

                rooms[1, 2] = new Room(6, RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
                CreateRoom(RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
            }

            // row 2
            buildRoom = random.Next(1, 3);
            if (buildRoom == 1)
            {
                int RoomHeight = random.Next(3, 9);
                int RoomWidth = random.Next(3, 34);

                int RoomPOSX = random.Next(0, 34 - 1 - RoomWidth);
                int RoomPOSY = random.Next(26, 35 - 1 - RoomHeight);

                rooms[2, 0] = new Room(7, RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
                CreateRoom(RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
            }

            buildRoom = random.Next(1, 3);
            if (buildRoom == 1)
            {
                int RoomHeight = random.Next(3, 9);
                int RoomWidth = random.Next(3, 34);

                int RoomPOSX = random.Next(38, 72 - 1 - RoomWidth);
                int RoomPOSY = random.Next(26, 35 - 1 - RoomHeight);

                rooms[2, 1] = new Room(8, RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
                CreateRoom(RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
            }

            buildRoom = random.Next(1, 3);
            if (buildRoom == 1)
            {
                int RoomHeight = random.Next(3, 9);
                int RoomWidth = random.Next(3, 34);

                int RoomPOSX = random.Next(76, 110 - 1 - RoomWidth);
                int RoomPOSY = random.Next(26, 35 - 1 - RoomHeight);

                rooms[2, 2] = new Room(9, RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
                CreateRoom(RoomPOSX, RoomPOSY, RoomHeight, RoomWidth);
            }



            //if (NumberOfRooms < 4 && NumberOfHallways < 3)
            //{
            //    Reset();
            //    Program.Main();
            //}


        }


        public bool CheckLoneRooms()
        {
            bool RoomAlone = false;

            Room room1 = (Room)rooms[0, 0];
            Room room2 = (Room)rooms[0, 1];
            Room room3 = (Room)rooms[0, 2];

            Room room4 = (Room)rooms[1, 0];
            Room room5 = (Room)rooms[1, 1];
            Room room6 = (Room)rooms[1, 2];

            Room room7 = (Room)rooms[2, 0];
            Room room8 = (Room)rooms[2, 1];
            Room room9 = (Room)rooms[2, 2];

            if (room1.Number != 0 && room2.Number == 0 && room3.Number == 0 && room4.Number == 0 && room7.Number == 0)
            {
                RoomAlone = true;
            }
            if (room1.Number == 0 && room2.Number != 0 && room3.Number == 0 && room5.Number == 0 && room8.Number == 0)
            {
                RoomAlone = true;
            }
            if (room1.Number == 0 && room2.Number == 0 && room3.Number != 0 && room6.Number == 0 && room6.Number == 0)
            {
                RoomAlone = true;
            }

            if (room4.Number != 0 && room5.Number == 0 && room6.Number == 0 && room1.Number == 0 && room7.Number == 0)
            {
                RoomAlone = true;
            }
            if (room5.Number != 0 && room4.Number == 0 && room6.Number == 0 && room2.Number == 0 && room8.Number == 0)
            {
                RoomAlone = true;
            }
            if (room6.Number != 0 && room4.Number == 0 && room5.Number == 0 && room3.Number == 0 && room9.Number == 0)
            {
                RoomAlone = true;
            }

            if (room7.Number != 0 && room8.Number == 0 && room9.Number == 0 && room1.Number == 0 && room4.Number == 0)
            {
                RoomAlone = true;
            }
            if (room8.Number != 0 && room7.Number == 0 && room9.Number == 0 && room2.Number == 0 && room5.Number == 0)
            {
                RoomAlone = true;
            }
            if (room9.Number != 0 && room7.Number == 0 && room8.Number == 0 && room6.Number == 0 && room3.Number == 0)
            {
                RoomAlone = true;
            }

            return RoomAlone;
        }

        public void CreateHallways()
        {
            Room room1 = (Room)rooms[0, 0];
            Room room2 = (Room)rooms[0, 1];
            Room room3 = (Room)rooms[0, 2];

            Room room4 = (Room)rooms[1, 0];
            Room room5 = (Room)rooms[1, 1];
            Room room6 = (Room)rooms[1, 2];

            Room room7 = (Room)rooms[2, 0];
            Room room8 = (Room)rooms[2, 1];
            Room room9 = (Room)rooms[2, 2];

            // create L hallways
            // 1 - -
            // - 5 6
            // - 8 9
            // connect 1 to 5

            //todo create a L hallway method




            // create horizontal hallways
            if (room1.Number != 0 && room2.Number != 0)
            {
                CreateHorizontalHallways(0, 0, 0, 1);
            }
            if (room2.Number != 0 && room3.Number != 0)
            {
                CreateHorizontalHallways(0, 1, 0, 2);
            }
            if (room1.Number != 0 && room3.Number != 0)
            {
                if (room2.Number == 0)
                {
                    CreateHorizontalHallways(0, 0, 0, 2);
                }
            }

            if (room4.Number != 0 && room5.Number != 0)
            {
                CreateHorizontalHallways(1, 0, 1, 1);
            }
            if (room5.Number != 0 && room6.Number != 0)
            {
                CreateHorizontalHallways(1, 1, 1, 2);
            }
            if (room4.Number != 0 && room6.Number != 0)
            {
                if (room5.Number == 0)
                {
                    CreateHorizontalHallways(1, 0, 1, 2);
                }
            }

            if (room7.Number != 0 && room8.Number != 0)
            {
                CreateHorizontalHallways(2, 0, 2, 1);
            }
            if (room8.Number != 0 && room9.Number != 0)
            {
                CreateHorizontalHallways(2, 1, 2, 2);
            }
            if (room7.Number != 0 && room9.Number != 0)
            {
                if (room8.Number == 0)
                {
                    CreateHorizontalHallways(2, 0, 2, 2);
                }
            }

            //veritcal hallways
            if (room1.Number != 0 && room4.Number != 0)
            {
                CreateVeritcalHallways(0, 0, 1, 0);
            }
            if (room4.Number != 0 && room7.Number != 0)
            {
                CreateVeritcalHallways(1, 0, 2, 0);
            }
            if (room1.Number != 0 && room7.Number != 0)
            {
                if (room4.Number == 0)
                {
                    CreateVeritcalHallways(0, 0, 2, 0);
                }
            }

            if (room2.Number != 0 && room5.Number != 0)
            {
                CreateVeritcalHallways(0, 1, 1, 1);
            }
            if (room5.Number != 0 && room8.Number != 0)
            {
                CreateVeritcalHallways(1, 1, 2, 1);
            }
            if (room2.Number != 0 && room8.Number != 0)
            {
                if (room5.Number == 0)
                {
                    CreateVeritcalHallways(0, 1, 2, 1);
                }
            }

            if (room3.Number != 0 && room6.Number != 0)
            {
                CreateVeritcalHallways(0, 2, 1, 2);
            }
            if (room6.Number != 0 && room9.Number != 0)
            {
                CreateVeritcalHallways(1, 2, 2, 2);
            }
            if (room3.Number != 0 && room9.Number != 0)
            {
                if (room6.Number == 0)
                {
                    CreateVeritcalHallways(0, 2, 2, 2);
                }
            }
        }

        public void CreateVeritcalHallways(int OneX, int OneY, int TwoX, int TwoY)
        {
            Random random = new Random();
            //! room 1
            Room Room1 = (Room)rooms[OneX, OneY];
            int _number1 = Room1.Number;
            int _posx1 = Room1.POSx;
            int _posy1 = Room1.POSy;
            int _height1 = Room1.Height;
            int _width1 = Room1.Width;

            //! room 2
            Room Room2 = (Room)rooms[TwoX, TwoY];
            int _number2 = Room2.Number;
            int _posx2 = Room2.POSx;
            int _posy2 = Room2.POSy;
            int _height2 = Room2.Height;
            int _width2 = Room2.Width;

            //! connect room 1 to room 2

            //! door 1 x, y
            int RandomDoor1 = random.Next(1, _width1);
            int Door1x = _posx1 + RandomDoor1;
            int Door1y = _posy1 + _height1;

            //! door 2 x, y
            int RandomDoor2 = random.Next(1, _width2);
            int Door2x = _posx2 + RandomDoor2;
            int Door2y = _posy2;

            //! hallway
            int HallLength = Door2y - _posy1 - _height1;
            int HallRandom = random.Next(1, HallLength);
            int HallpartA = HallLength - HallRandom;
            int HallpartB = Door1y + HallLength - HallpartA;

            //Console.SetCursorPosition(110, 1);
            //Console.WriteLine("hallrandom: " + HallRandom + " HallLength: " + HallLength);

            //Console.SetCursorPosition(110, 2);
            //Console.WriteLine("door1x: " + Door1x + " Door1y: " + Door1y);
            //Console.SetCursorPosition(110, 3);
            //Console.WriteLine("door2x: " + Door2x + " Door2y: " + Door2y);
            //Console.SetCursorPosition(110, 4);
            //Console.WriteLine("hallA: " + HallpartA + " HallB: " + HallpartB);

            //todo check if door1 and door2 are on same y path
            if (Door1x == Door2x)
            {
                int x = Door1x;
                for (int y = Door1y; y <= Door2y; y++)
                {
                    Tile CurrentTile = (Tile)GameMap[x, y];
                    CurrentTile.Icon = FloorIcon;
                    CurrentTile.IsWalkable = true;
                    CurrentTile.IsHallway = true;

                    if (y == _posy1 + _height1)
                    {
                        Tile UcornerTile = (Tile)GameMap[x, y];
                        UcornerTile.Icon = DoorIcon;
                        UcornerTile.IsWalkable = true;
                        UcornerTile.IsHallway = true;
                    }

                    if (y == Door2y)
                    {
                        Tile UcornerTile = (Tile)GameMap[x, y];
                        UcornerTile.Icon = DoorIcon;
                        UcornerTile.IsWalkable = true;
                        UcornerTile.IsHallway = true;
                    }
                }
            }
            else
            {
                //todo build z hallway
                int x = Door1x;
                for (int y = Door1y; y <= HallpartB; y++)
                {
                    Tile CurrentTile = (Tile)GameMap[x, y];
                    if (y == Door1y)
                    {
                        CurrentTile.Icon = DoorIcon;
                        CurrentTile.IsWalkable = true;
                        CurrentTile.IsHallway = true;
                    }
                    else
                    {
                        CurrentTile.Icon = FloorIcon;
                        CurrentTile.IsWalkable = true;
                        CurrentTile.IsHallway = true;
                    }
                }
                x = Door2x;
                for (int y = HallpartB; y <= Door2y; y++)
                {
                    Tile CurrentTile = (Tile)GameMap[x, y];
                    if (y == Door2y)
                    {
                        CurrentTile.Icon = DoorIcon;
                        CurrentTile.IsWalkable = true;
                        CurrentTile.IsHallway = true;
                    }
                    else
                    {
                        CurrentTile.Icon = FloorIcon;
                        CurrentTile.IsWalkable = true;
                        CurrentTile.IsHallway = true;
                    }
                }

                if (Door1x < Door2x)
                {
                    int y1 = HallpartB;
                    for (int x1 = Door1x; x1 <= Door2x; x1++)
                    {
                        Tile CurrentTile = (Tile)GameMap[x1, y1];
                        CurrentTile.Icon = FloorIcon;
                        CurrentTile.IsWalkable = true;
                        CurrentTile.IsHallway = true;
                    }
                }
                else
                {
                    int y1 = HallpartB;
                    for (int x1 = Door2x; x1 <= Door1x; x1++)
                    {
                        Tile CurrentTile = (Tile)GameMap[x1, y1];
                        CurrentTile.Icon = FloorIcon;
                        CurrentTile.IsWalkable = true;
                        CurrentTile.IsHallway = true;
                    }
                }
            }
            
            NumberOfHallways++;
        }

        //! connecting room 2 [0, 1] to room 3 [0, 2]
        public void CreateHorizontalHallways(int OneX, int OneY, int TwoX, int TwoY)
        {
            Random random = new Random();
            //! room 1
            Room Room1 = (Room)rooms[OneX, OneY];
            int _number1 = Room1.Number;
            int _posx1 = Room1.POSx;
            int _posy1 = Room1.POSy;
            int _height1 = Room1.Height;
            int _width1 = Room1.Width;

            //! room 2
            Room Room2 = (Room)rooms[TwoX, TwoY];
            int _number2 = Room2.Number;
            int _posx2 = Room2.POSx;
            int _posy2 = Room2.POSy;
            int _height2 = Room2.Height;
            int _width2 = Room2.Width;

            //! connect room 1 to room 2

            //! door 1 x, y
            int RandomDoor1 = random.Next(1, _height1);
            int Door1x = _posx1 + _width1;
            int Door1y = _posy1 + RandomDoor1;

            //! door 2 x, y
            int RandomDoor2 = random.Next(1, _height2);
            int Door2x = _posx2;
            int Door2y = _posy2 + RandomDoor2;

            //! hallway
            int HallLength = Door2x - _posx1 - _width1;
            int HallRandom = random.Next(1, HallLength);
            int HallpartA = HallLength - HallRandom;
            int HallpartB = Door1x + HallLength - HallpartA;

            //todo check if door1 and door2 are on same y path
            if (Door1y == Door2y)
            {
                int y = Door1y;
                for (int x = Door1x; x <= Door2x; x++)
                {
                    Tile CurrentTile = (Tile)GameMap[x, y];
                    CurrentTile.Icon = FloorIcon;
                    CurrentTile.IsWalkable = true;
                    CurrentTile.IsHallway = true;

                    //! walls and corner walls around the hallway
                    //! needs tweaked but mostly works however it is disabled untill i get hallways working correctly
                    //Tile uTile = (Tile)GameMap[x, y - 1];
                    //uTile.Icon = WallxIcon;
                    //uTile.IsWalkable = false;

                    //Tile bTile = (Tile)GameMap[x, y + 1];
                    //bTile.Icon = WallxIcon;
                    //bTile.IsWalkable = false;

                    //? check center tile and all 8 tiles around it to apply correct wall icon
                    if (x == _posx1 + _width1)
                    {
                        Tile UcornerTile = (Tile)GameMap[x, y];
                        UcornerTile.Icon = DoorIcon;
                        UcornerTile.IsWalkable = true;
                        UcornerTile.IsHallway = true;

                        //    Tile UcornerTile = (Tile)GameMap[x, y - 1];
                        //    UcornerTile.Icon = SWcornerIcon;
                        //    UcornerTile.IsWalkable = false;

                        //    Tile BcornerTile = (Tile)GameMap[x, y + 1];
                        //    BcornerTile.Icon = NWcornerIcon;
                        //    BcornerTile.IsWalkable = false;
                    }

                    if (x == Door2x)
                    {
                        Tile UcornerTile = (Tile)GameMap[x, y];
                        UcornerTile.Icon = DoorIcon;
                        UcornerTile.IsWalkable = true;
                        UcornerTile.IsHallway = true;

                        //    Tile UcornerTile = (Tile)GameMap[x, y - 1];
                        //    UcornerTile.Icon = SEcornerIcon;
                        //    UcornerTile.IsWalkable = false;

                        //    Tile BcornerTile = (Tile)GameMap[x, y + 1];
                        //    BcornerTile.Icon = NEcornerIcon;
                        //    BcornerTile.IsWalkable = false;
                    }
                }
            }
            else
            {
                //todo build z hallway

                //Console.SetCursorPosition(110, 1);
                //Console.WriteLine("hallrandom: " + HallRandom + " HallLength: " + HallLength);

                //Console.SetCursorPosition(110, 2);
                //Console.WriteLine("door1x: " + Door1x + " Door1y: " + Door1y);
                //Console.SetCursorPosition(110, 3);
                //Console.WriteLine("door2x: " + Door2x + " Door2y: " + Door2y);
                //Console.SetCursorPosition(110, 4);
                //Console.WriteLine("hallA: " + HallpartA + " HallB: " + HallpartB);

                int y = Door1y;
                for (int x = Door1x; x <= HallpartB; x++)
                {
                    Tile CurrentTile = (Tile)GameMap[x, y];
                    if (x == Door1x)
                    {
                        CurrentTile.Icon = DoorIcon;
                        CurrentTile.IsWalkable = true;
                        CurrentTile.IsHallway = true;
                    }
                    else
                    {
                        CurrentTile.Icon = FloorIcon;
                        CurrentTile.IsWalkable = true;
                        CurrentTile.IsHallway = true;
                    }
                }
                y = Door2y;
                for (int x = HallpartB; x <= Door2x; x++)
                {
                    Tile CurrentTile = (Tile)GameMap[x, y];
                    if (x == Door2x)
                    {
                        CurrentTile.Icon = DoorIcon;
                        CurrentTile.IsWalkable = true;
                        CurrentTile.IsHallway = true;
                    }
                    else
                    {
                        CurrentTile.Icon = FloorIcon;
                        CurrentTile.IsWalkable = true;
                        CurrentTile.IsHallway = true;
                    }
                }

                if (Door1y < Door2y)
                {
                    int x1 = HallpartB;
                    for (int y1 = Door1y; y1 <= Door2y; y1++)
                    {
                        Tile CurrentTile = (Tile)GameMap[x1, y1];
                        CurrentTile.Icon = FloorIcon;
                        CurrentTile.IsWalkable = true;
                        CurrentTile.IsHallway = true;
                    }
                }
                else
                {
                    int x1 = HallpartB;
                    for (int y1 = Door2y; y1 <= Door1y; y1++)
                    {
                        Tile CurrentTile = (Tile)GameMap[x1, y1];
                        CurrentTile.Icon = FloorIcon;
                        CurrentTile.IsWalkable = true;
                        CurrentTile.IsHallway = true;
                    }
                }
            }
            
            NumberOfHallways++;
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
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(RoomPOSX + x, RoomPOSY + y, WallxIcon, false, false, false, false);
                    }
                    else if (x == 0 || x == RoomWidth) // "║"
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(RoomPOSX + x, RoomPOSY + y, WallyIcon, false, false, false, false);
                    }
                    //apply floors
                    else // "."
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(RoomPOSX + x, RoomPOSY + y, FloorIcon, true, false, false, false);
                    }

                    // apply corner walls
                    if (x == 0 && y == 0)
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(RoomPOSX + x, RoomPOSY + y, NWcornerIcon, false, false, false, false);
                    }
                    if (y == 0 && x == RoomWidth)
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(RoomPOSX + x, RoomPOSY + y, NEcornerIcon, false, false, false, false);
                    }
                    if (y == RoomHeight && x == RoomWidth)
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(RoomPOSX + x, RoomPOSY + y, SEcornerIcon, false, false, false, false);
                    }
                    if (y == RoomHeight && x == 0)
                    {
                        GameMap[RoomPOSX + x, RoomPOSY + y] = new Tile(RoomPOSX + x, RoomPOSY + y, SWcornerIcon, false, false, false, false);
                    }
                }
            }

            //RoomNumber++;
        }

        public void FillMap()
        {
            for (int x = 0; x <= MapSizeX - 1; x++)
            {
                for (int y = 0; y <= MapSizeY - 1; y++)
                {
                    //! fill array with blank tiles
                    GameMap[x, y] = new Tile(x, y, " ", false, false, false, false);
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
            int plX = 0;
            int plY = 0;

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
                        plX = x;
                        plY = y;
                    }
                    else if (CurrentTile.IsMonster) //set monster icon to red / may change this, red is hard to see
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(_icon);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(_icon);
                    }

                    //disabled for being slow
                    //POV(plX, plY);

                }
            }
        }

        //! this is horribly slow
        public void POV(int posX, int posY)
        {
            Tile CurrentTile = (Tile)GameMap[posX, posY];
            string _icon = CurrentTile.Icon;

            for (int x = (posX - 1); x <= (posX + 1); x++)
            {
                for (int y = (posY - 1); y <= (posY + 1); y++)
                {
                    if (posX != 0 && posY != 0)
                    {
                        CurrentTile = (Tile)GameMap[x, y];
                        _icon = CurrentTile.Icon;

                        Console.SetCursorPosition(x, y);
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.WriteLine(_icon);
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
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
                    CurrentTile.IsWalkable = false;

                    player.X = _randX;
                    player.Y = _randY;
                    _placed = 1;
                }
            } while (_placed == 0);

            StatBar.Display(player);
        }

        public (int X, int Y) PlaceItem(Item item)
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
                bool _isitem = CurrentTile.IsItem;

                if (_iswalkable && _placed == 0 && !_ishallway && !_isitem) //? remove !_ishallway to allow item to spawn in hallway
                {
                    CurrentTile.Icon = item.Icon;
                    _placed = 1;
                    CurrentTile.IsWalkable = true;
                    CurrentTile.IsItem = true;

                    item.X = _randX;
                    item.Y = _randY;
                }
            } while (_placed == 0);
            return (item.X, item.Y);
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

                if (_iswalkable && _placed == 0 && !_ishallway) //? remove !_ishallway to allow monster to spawn in hallway
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

        //? rename to ChangeTileIconToFloor(X, Y)
        //? or expand to ChangeTileIcon(X, Y, Icon)
        public void ChangeTileIcon(int X, int Y)
        {
            Tile CurrentTile = (Tile)GameMap[X, Y];
            CurrentTile.Icon = FloorIcon;
        }

        public void RemoveMonster(Monster monster)
        {
            //code to remove a monster after it has been killed
            //! add code to remove monster from list - currently being done at MovePlayer() --> Combat, return true then remove from list

            Tile CurrentTile = (Tile)GameMap[monster.X, monster.Y];

            CurrentTile.IsWalkable = true;
            CurrentTile.IsMonster = false;

            CurrentTile.Icon = FloorIcon;
            Display();
        }

        public void CurrentTileIsItem(Tile CurrentTile, Tile NextTile, List<Item> activeItems, Player player, string _direction)
        {
            for (int i = 0; i < activeItems.Count; i++)
            {
                if (activeItems[i].X == CurrentTile.X && activeItems[i].Y == CurrentTile.Y)
                {
                    CurrentTile.Icon = activeItems[i].Icon;
                    CurrentTile.IsWalkable = true;
                    NextTile.Icon = PlayerIcon;
                    NextTile.IsWalkable = false;
                    player.X = NextTile.X;
                    player.Y = NextTile.Y;
                    StatBar.Display(player);
                    ActivityLog.AddToLog("You move " + _direction + ".");
                }
            }
        }

        public void NextTileIsWalkable(Tile CurrentTile, Tile NextTile, Player player, string _direction)
        {
            CurrentTile.Icon = FloorIcon;
            CurrentTile.IsWalkable = true;
            NextTile.Icon = PlayerIcon;
            NextTile.IsWalkable = false;
            player.X = NextTile.X;
            player.Y = NextTile.Y;
            StatBar.Display(player);
            ActivityLog.AddToLog("You move " + _direction + ".");
        }

        public void HandleMovement(Tile CurrentTile, Tile NextTile, List<Item> activeItems, Player player, Map map, List<Monster> activeMonsters, string _direction)
        {
            if (NextTile.IsWalkable && CurrentTile.IsItem)
            {
                CurrentTileIsItem(CurrentTile, NextTile, activeItems, player, _direction);
            }
            else if (NextTile.IsWalkable)
            {
                NextTileIsWalkable(CurrentTile, NextTile, player, _direction);
            }
            else if (NextTile.IsMonster)
            {
                NextTileIsMonster(NextTile, player, map, activeMonsters, activeItems);
            }
        }

        public bool MovePlayer(string _direction, Player player, Map map, List<Monster> activeMonsters, List<Item> activeItems)
        {
            //! door is being over written

            Tile CurrentTile = GameMap[player.X, player.Y];
            Tile NextTile;

            switch (_direction)
            {
                case "North":
                    NextTile = GameMap[player.X, player.Y - 1];
                    HandleMovement(CurrentTile, NextTile, activeItems, player, map, activeMonsters, _direction);
                    return true;

                case "South":
                    NextTile = GameMap[player.X, player.Y + 1];
                    HandleMovement(CurrentTile, NextTile, activeItems, player, map, activeMonsters, _direction);
                    return true;

                case "West":
                    NextTile = GameMap[player.X - 1, player.Y];
                    HandleMovement(CurrentTile, NextTile, activeItems, player, map, activeMonsters, _direction);
                    return true;

                case "East":
                    NextTile = GameMap[player.X + 1, player.Y];
                    HandleMovement(CurrentTile, NextTile, activeItems, player, map, activeMonsters, _direction);
                    return true;

                case "NorthWest":
                    NextTile = GameMap[player.X - 1, player.Y - 1];
                    HandleMovement(CurrentTile, NextTile, activeItems, player, map, activeMonsters, _direction);
                    return true;

                case "NorthEast":
                    NextTile = GameMap[player.X + 1, player.Y - 1];
                    HandleMovement(CurrentTile, NextTile, activeItems, player, map, activeMonsters, _direction);
                    return true;

                case "SouthWest":
                    NextTile = GameMap[player.X - 1, player.Y + 1];
                    HandleMovement(CurrentTile, NextTile, activeItems, player, map, activeMonsters, _direction);
                    return true;

                case "SouthEast":
                    NextTile = GameMap[player.X + 1, player.Y + 1];
                    HandleMovement(CurrentTile, NextTile, activeItems, player, map, activeMonsters, _direction);
                    return true;
            }
            ActivityLog.AddToLog("You can't move that way.");
            return false;
        }

        private void NextTileIsMonster(Tile NextTile, Player player, Map map, List<Monster> activeMonsters, List<Item> activeItems)
        {
            for (int i = 0; i < activeMonsters.Count; i++)
            {
                if (activeMonsters[i].Y == NextTile.Y && activeMonsters[i].X == NextTile.X)
                {
                    if (Combat.PlayerAttacks(player, activeMonsters[i], map)) //if return true
                    {
                        activeMonsters.RemoveAt(i);

                        //! Loot Code - move to its own thing
                        activeItems.Add(Generate.Potion());

                        activeItems[activeItems.Count - 1].X = NextTile.X;
                        activeItems[activeItems.Count - 1].Y = NextTile.Y;

                        NextTile.Icon = "*"; //test icon
                    }
                }
            }
        }

        public void MoveMonster(Map map, List<Monster> activeMonsters)
        {
            //? rename RandomMoveMonster()
            //? recreate MoveMonster(current-pos, next-pos)

            Random random = new Random();

            int _randX;
            int _randY;

            foreach (Monster monster in activeMonsters)
            {
                //? make random chance if monster moves or leave at random 0,0

                //! monsters erase icon when they walk over a item
                //? fix or remove item from activeList as if the monster took the item

                Tile CurrentTile = GameMap[monster.X, monster.Y];

                _randX = random.Next(-1, 2);
                _randY = random.Next(-1, 2);

                Tile NextTile = (Tile)GameMap[monster.X + _randX, monster.Y + _randY];

                if (NextTile.IsWalkable)
                {
                    ProcessMonsterTile(CurrentTile, NextTile, monster);

                    monster.X += _randX;
                    monster.Y += _randY;
                }
            }
        }

        // change tile stats when a monster moves, MoveMonster()
        //? recreate to ProcessTile - use for all tile changes - Player, Monsters, Items
        private void ProcessMonsterTile(Tile CurrentTile, Tile NextTile, Monster monster)
        {
            CurrentTile.Icon = FloorIcon;
            CurrentTile.IsMonster = false;
            CurrentTile.IsWalkable = true;
            NextTile.Icon = monster.Icon;
            NextTile.IsMonster = true;
            NextTile.IsWalkable = false;
        }

        public void ProcessItemTile(Player player)
        {
            Tile CurrentTile = GameMap[player.X, player.Y];
            CurrentTile.IsItem = false;
        }
    }
}