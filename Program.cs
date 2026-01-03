using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2025.Common;

namespace AdventOfCode2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2025!");

            Console.WriteLine("Please enter the DAY you want to run (e.g., 1):");
            string input = "7";// Console.ReadLine();
            if (!int.TryParse(input, out int dayNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid DAY number.");
                return;
            }

            Console.WriteLine("Do you want to enable logging? (y/n):");
            if (Console.ReadLine().Trim().ToLower() == "y")
            {
                Logger.showLogs = true;
            }

            string dayClassName = $"AdventOfCode2025.Day{dayNumber:D2}";
            Type? dayType = Type.GetType(dayClassName);
            var mainMethod = dayType.GetMethod("Main", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            mainMethod.Invoke(null, null);
            // Not safe yet but works for simple starting UI\
        }
    }
}
