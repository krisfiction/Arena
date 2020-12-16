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
            switch (1)
            {
                case 1:
                    Health health = new Health();
                    return health;
            }
        }
    }
}
