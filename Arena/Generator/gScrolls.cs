using System;
using System.Collections.Generic;
using System.Text;
using Arena.Items;
using Arena.Items.Scrolls;

namespace Arena.Generator
{
    public static partial class Generate
    {
        public static Scroll Scroll()
        {
            switch (1)
            {
                case 1:
                    Teleportation teleportation = new Teleportation();
                    return teleportation;
            }
            //return null;
        }
    }
}
