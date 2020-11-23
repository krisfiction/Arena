using System;
using System.Collections.Generic;
using System.Text;
using Arena.Characters;
using Arena.MapGenerator;

namespace Arena
{
    internal static class Combat
    {
        private static readonly Random random = new Random();

        public static void PlayerAttacks(Player player, Monster monster, Map map) // remove ??
        {
            
            if (monster.Health > 0)
            {
                //if monster is alive, player does damage
                ActivityLog.AddToLog(player.Name + " has hit a " + monster.Name + " for 5 damage");
                monster.Health -= 5; // fake player damage of 5 to monster
            }

            if (monster.Health <= 0)
            {
                //if monster is dead, tell player and remove monster from map
                ActivityLog.AddToLog(player.Name + " has killed a " + monster.Name);
                map.RemoveMonster(monster);
            }

        }

    }
}
