using System;
using System.Collections.Generic;
using System.Text;
using Arena.Characters;
using Arena.Characters.Monsters;

namespace Arena
{
    internal static class Generate
    {
        private static readonly Random random = new Random();

        public static Monster Monster()
        {
            switch (random.Next(1, 3))
            {
                case 1:
                    Rat rat = new Rat();
                    rat.HealthMax = rat.Health = rat.SetHealth();
                    return rat;
                case 2:
                    Skeleton skeleton = new Skeleton();
                    skeleton.HealthMax = skeleton.Health = skeleton.SetHealth();
                    return skeleton;
            }

            return null;
        }














    }
}