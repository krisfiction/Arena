using System;
using System.Collections.Generic;
using System.Text;

namespace Arena.MapGenerator
{
    internal class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Icon { get; set; }
        public bool IsWalkable { get; set; }
        public bool IsHallway { get; set; }

        public bool IsMonster { get; set; }

        public Tile(int _x, int _y, string _icon, bool _iswalkable, bool _ishallway, bool _ismonster)
        {
            X = _x;
            Y = _y;
            Icon = _icon;
            IsWalkable = _iswalkable;
            IsHallway = _ishallway;
            IsMonster = _ismonster;
        }

        public string DisplayIcon()
        {
            return Icon;
        }
    }
}
