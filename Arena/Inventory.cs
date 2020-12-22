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
        public static List<Items.Scroll> ScrollInventory { get; set; }

        public void Initialize()
        {
            //Inventories = new List<Inventory>();
            PotionInventory = new List<Items.Potion>();
            ScrollInventory = new List<Items.Scroll>();


        }









        public static void Display(string _type)
        {
            Console.Clear();


            if (_type == "potion")
            {
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
                GetInput("potion");
            }
            if (_type == "scroll")
            {
                if (ScrollInventory.Count == 0)
                {
                    Console.WriteLine("Scroll Inventory Empty.");
                }
                else
                {
                    Console.WriteLine("Scroll Inventory:");
                    Console.WriteLine();

                    int _lineNumber = 0;
                    foreach (var Scroll in ScrollInventory)
                    {
                        Console.WriteLine($"{_lineNumber} {Scroll.Name}");
                        _lineNumber++;
                    }
                }
            }
            if (_type == "all")
            {
                //Display("potion");
                //Display("scroll");
            }
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("press 'esc' to return to the map.");
            //? should i still display activity log (and statbar) or maybe a menu bar
        }

        public static void GetInput(string _type)
        {
            Console.WriteLine("Enter Number:");

            if (_type == "potion")
            {
                PotionInventory[Convert.ToInt32(Console.ReadLine())].Cast();  //! broken
            }
        }


        public static void Loop(string _type)
        {

            bool _displayInventory = true;
            do
            {
                Display(_type);

                ConsoleKey bInput = Console.ReadKey(true).Key; //true hides input
                if (bInput == ConsoleKey.Escape)
                {
                    _displayInventory = false;
                }
            } while (_displayInventory);

        }
    }
}
