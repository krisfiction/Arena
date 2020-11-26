using System;
using System.Collections.Generic;
using System.Text;

namespace Arena.Characters
{
    public class Monster : Character
    {
        // values for random health based on high/low - so monsters will have varying health
        public int HealthLow { get; set; }
        public int HealthHigh { get; set; }


    }
}
