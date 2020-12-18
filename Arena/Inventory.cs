using System;
using System.Collections.Generic;
using System.Text;
using Arena.Items.Potions;

namespace Arena
{
    public class Inventory
    {
        //public static List<Inventory> Inventories { get; set; }
        public static List<Items.Potion> PotionInventory { get; set; }

        public void Initialize()
        {
            //Inventories = new List<Inventory>();
            PotionInventory = new List<Items.Potion>();


        }









        public static void Display()
        {
            Console.Clear();
            if (PotionInventory.Count == 0)
            {
                Console.WriteLine("Potion Inventory Empty.");
            }
            else
            {
                Console.WriteLine("Potion Inventory:");
                Console.WriteLine();

                int _lineNumber = 0;
                foreach (var Potion in PotionInventory)
                {
                    Console.WriteLine($"{_lineNumber} {Potion.Name}");
                    _lineNumber++;
                }
            }
        }




        public static void Loop()
        {

            bool _displayInventory = true;
            do
            {
                Display();

                ConsoleKey bInput = Console.ReadKey(true).Key; //true hides input
                if (bInput == ConsoleKey.Escape)
                {
                    _displayInventory = false;
                }
            } while (_displayInventory);

        }
    }
}
