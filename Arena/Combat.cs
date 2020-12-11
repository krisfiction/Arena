using System;
using System.Collections.Generic;
using System.Text;
using Arena.Characters;
using Arena.MapGenerator;

namespace Arena
{
    internal static class Combat
    {
        public static bool PlayerAttacks(Player player, Monster monster, Map map)
        {
            
            if (monster.Health > 0)
            {
                //if monster is alive, player does damage
                monster.Health -= 5; // fake player damage of 5 to monster
                ActivityLog.AddToLog($"{player.Name} [{player.Health}/{player.HealthMax}] has hit a {monster.Name} [{monster.Health}/{monster.HealthMax}] for 5 damage.");
                
                MonsterAttacks(player, monster);// auto hit
            }

            if (monster.Health <= 0)
            {
                //if monster is dead, tell player and remove monster from map
                ActivityLog.AddToLog($"{player.Name} [{player.Health}/{player.HealthMax}] has killed a {monster.Name}");//? add to map.RemoveMonster()
                map.RemoveMonster(monster);
                return true;
            }

            return false;
        }

        public static void MonsterAttacks(Player player, Monster monster)
        {
            player.Health -= 5;
            ActivityLog.AddToLog($"{monster.Name} [{monster.Health}/{monster.HealthMax}] hits {player.Name} for 5 damage.");
            StatBar.Display(player);
        }

    }
}
