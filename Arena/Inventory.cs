using System;
using System.Collections.Generic;
using System.Text;
using Arena.Items.Potions;
using Arena.Items.Scrolls;

namespace Arena
{
    public class Inventory
    {
        public static List<Items.Potion> PotionInventory { get; set; }
        public static List<Items.Scroll> ScrollInventory { get; set; }

        public void Initialize()
        {
            PotionInventory = new List<Items.Potion>();
            ScrollInventory = new List<Items.Scroll>();
        }



        public static void Display(string _type)
        {
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
                    if (_type == "potion")
                    {
                        GetInput("potion");
                    }
                    
                }
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
                    if (_type == "scroll")
                    {
                        GetInput("scroll");
                    }
                }
            }
            if (_type == "all")
            {
                Display("potion");
                Display("scroll");
            }
            
            
            //? should i still display activity log (and statbar) or maybe a menu bar
        }

        public static void GetInput(string _type)
        {
            //? conver to letter instead of number
            Console.WriteLine("Enter Number:");

            int i = Convert.ToInt32(Console.ReadLine());

            if (_type == "potion")
            {
                if (PotionInventory[i].Name == "Health Potion")
                {
                    Health.Cast();
                }
                PotionInventory.RemoveAt(i);
            }
            if (_type == "scroll")
            {
                if (ScrollInventory[i].Name == "Teleportation Scroll")
                {
                    Teleportation.Cast();
                }
                ScrollInventory.RemoveAt(i);
            }
        }


        public static void Loop(string _type)
        {

            bool _displayInventory = true;
            do
            {
                Display(_type);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("press 'esc' to return to the map.");

                ConsoleKey bInput = Console.ReadKey(true).Key; //true hides input
                if (bInput == ConsoleKey.Escape)
                {
                    _displayInventory = false;
                }
            } while (_displayInventory);

            
        }
    }
}
