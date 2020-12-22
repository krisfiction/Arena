using Arena.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arena
{
    internal static class StatBar
    {
        public static void Display(Player player)
        {
            Console.SetCursorPosition(110, 0);
            Console.WriteLine(player.Name);
            Console.SetCursorPosition(110, 1);
            Console.WriteLine($"Health: {player.Health}/{player.HealthMax}   ");
            
            Console.SetCursorPosition(110, 3);
            Console.WriteLine($"Player Position: x{player.X}, y{player.Y}   ");

        }
    }
}
