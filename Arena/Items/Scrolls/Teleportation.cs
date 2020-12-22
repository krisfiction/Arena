using System;
using System.Collections.Generic;
using System.Text;
using Arena.Characters;

namespace Arena.Items.Scrolls
{
    public class Teleportation : Scroll
    {
        public Teleportation()
        {
            Name = "Teleportation Scroll";
            Icon = "?";
            Type = "Scroll";
        }

        public static void Cast(Player player)
        {
            Console.WriteLine("you read a Telportation Scroll");
        }
    }
}
