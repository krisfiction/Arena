using System;
using System.Collections.Generic;
using System.Text;
using Arena.Characters;

namespace Arena.Items.Potions
{
    public class Health : Potion
    {
        //public Health(int _x, int _y)
        public Health()
        {
            Name = "Health Potion"; //? remove potion from name
            Icon = "!"; //? move to be set in Potion.cs
            Type = "Potion";

           // X = _x; //! will this work - set position at creation 
           // Y = _y;
        }

        public static void Cast(Player player)
        {
            //fix to be useable
            player.Health += 50;
            Console.WriteLine("you quaff a health potion");
        }
    }
}
