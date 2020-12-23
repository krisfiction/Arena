using System;
using System.Collections.Generic;
using System.Text;
using Arena.Characters;
using Arena.MapGenerator;

namespace Arena.Items.Scrolls
{
    public class Teleportation : Scroll
    {
        public Teleportation()
        {
            Name = "Teleportation Scroll";
            Icon = "?";
            Type = "Scroll";
        }

        public static void Cast(Player player, Map map)
        {
            map.ChangeTileIcon(player.X, player.Y);

            map.PlacePlayer(player);
            Console.WriteLine("you read a Telportation Scroll");
            ActivityLog.AddToLog("you read a Telportation Scroll");
        }
    }
}
