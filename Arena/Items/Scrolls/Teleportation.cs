using System;
using System.Collections.Generic;
using System.Text;

namespace Arena.Items.Scrolls
{
    public class Teleportation : Scroll
    {
        public Teleportation()
        {
            Name = "Teleportation Scroll";
            Icon = "%"; //temp icon for 
            Type = "Scroll";
        }

        public static void Cast()
        {
            Console.WriteLine("you read a Telportation Scroll");
        }
    }
}
