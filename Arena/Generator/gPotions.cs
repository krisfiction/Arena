using System;
using System.Collections.Generic;
using System.Text;
using Arena.Items;
using Arena.Items.Potions;


namespace Arena.Generator
{
    public static partial class Generate
    {
        public static Potion Potion()
        {
            switch (random.Next(1,3))
            {
                case 1:
                    Health health = new Health();
                    return health;
                case 2:
                    Teleportation teleportation = new Teleportation();
                    return teleportation;
            }
            return null;
        }
    }
}
