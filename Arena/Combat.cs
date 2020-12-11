using System;
using System.Collections.Generic;
using System.Text;
using Arena.Characters;
using Arena.MapGenerator;

namespace Arena
{
    internal static class Combat
    {
        //private static readonly Random random = new Random();

        public static bool PlayerAttacks(Player player, Monster monster, Map map) // remove ??
        {
            
            if (monster.Health > 0)
            {
                //if monster is alive, player does damage
                monster.Health -= 5; // fake player damage of 5 to monster
                ActivityLog.AddToLog(player.Name + " has hit a " + monster.Name + " for 5 damage (" + monster.Health + " / " + monster.HealthMax + ")");
                
                MonsterAttacks(player, monster, map);// auto hit
            }

            if (monster.Health <= 0)
            {
                //if monster is dead, tell player and remove monster from map
                ActivityLog.AddToLog(player.Name + " has killed a " + monster.Name);
                map.RemoveMonster(monster);
                return true;
            }
            return false;

        }

        public static void MonsterAttacks(Player player, Monster monster, Map map)
        {
            player.Health -= 5;
            ActivityLog.AddToLog(monster.Name + " hits " + player.Name + " for 5 damage.");
            StatBar.Display(player);
        }

    }
}
