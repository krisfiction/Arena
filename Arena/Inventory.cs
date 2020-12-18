using System;
using System.Collections.Generic;
using System.Text;

namespace Arena
{
    public class Inventory
    {
        public static void Display()
        {
            Console.Clear();
            Console.WriteLine("this is the inventory");
            


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
