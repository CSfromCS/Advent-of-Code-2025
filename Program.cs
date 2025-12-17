using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Advent of Code 2025!");

            Console.WriteLine("Please enter the DAY you want to run (e.g., 1):");
            string input = "3";// Console.ReadLine();
            if (int.TryParse(input, out int dayNumber))
            {
                string dayClassName = $"AdventOfCode2025.Day{dayNumber:D2}";
                Type? dayType = Type.GetType(dayClassName);
                var mainMethod = dayType.GetMethod("Main", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                mainMethod.Invoke(null, null);
                // Not safe yet but works for simple starting UI
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid DAY number.");
            }
        }
    }
}
