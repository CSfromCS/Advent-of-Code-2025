using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025.Common
{
    public static class Logger
    {
        public static bool showLogs = false;
        public static void Log(object? message = null)
        {
            if (showLogs)
            {
                Console.WriteLine(message);
            }
        }

        public static void LogNoEnter(object? message = null)
        {
            if (showLogs)
            {
                Console.Write(message);
            }
        }

        public static void Report(object? message = null)
        {
            Console.WriteLine(message);
        }
    }
}
