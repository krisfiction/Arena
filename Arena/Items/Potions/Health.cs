using System;
using System.Collections.Generic;
using System.Text;

namespace Arena.Items.Potions
{
    public class Health : Potion
    {
        //public Health(int _x, int _y)
        public Health()
        {
            Name = "Health Potion"; //? remove potion from name
            Icon = "!"; //? move to be set in Potion.cs

           // X = _x; //! will this work - set position at creation 
           // Y = _y;
        }
    }
}
