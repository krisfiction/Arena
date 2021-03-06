﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Arena
{
    internal static class ActivityLog
    {
        private static readonly List<string> Log = new List<string>();

        public static void AddToLog(string _input)
        {
            Log.Add(_input + "                                                "); //may need to adjust blank space or even find a different approach
        }

        public static void ClearLog()
        {
            Log.Clear();
        }

        public static void Display()
        {
            Console.SetCursorPosition(0, 36); //! quick add may needs adjusted
            if (Log.Count > 10)
            {
                for (int i = Log.Count - 10; i < Log.Count; i++)
                {
                    Console.WriteLine(Log[i]);
                }
            }
            else
            {
                for (int i = 0; i <= Log.Count - 1; i++)
                {
                    Console.WriteLine(Log[i]);
                }
            }
        }
    }
}

