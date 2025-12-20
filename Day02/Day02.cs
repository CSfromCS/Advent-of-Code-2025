using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2025.Common;
using Common;

namespace AdventOfCode2025
{
    internal class Day02
    {
        const string DAY = "Day02";
        string[] ranges = new string[] { };
        public static void Main()
        {
            Console.WriteLine("Hello from Day02!");

            // Experimenting with OOP approach of making these calls

            Day02 day02 = new Day02();
            day02.LoadRanges();
            //day02.printRanges();
            //day02.part1();
            day02.part2();
        }

        public void LoadRanges()
        {
            string inputLine = InputReader.ReadLines(DAY, "input.txt").First();
            ranges = inputLine.Split(',');
        }

        public void printRanges()
        {
            foreach (string range in ranges)
            {
                Console.WriteLine(range);
            }
        }

        public void part1()
        {
            long sumOfInvalids = 0;

            string[] parts;
            long startNumber, endNumber;

            int length, halfLength;
            long firstHalf, secondHalf;

            foreach (var range in ranges)
            {
                Console.WriteLine(range);
                parts = range.Split('-');
                startNumber = long.Parse(parts[0]);
                endNumber = long.Parse(parts[1]);

                for (long i = startNumber; i <= endNumber; i++)
                {
                    //isOdd = parts[0].Length % 2 == 1;
                    //Console.WriteLine(parts[0][..(halfLength - (isOdd ? 1 : 0))]);
                    //Console.WriteLine(parts[0][(halfLength - (isOdd ? 1 : 0))..]);
                    length = i.ToString().Length;
                    if(length % 2 == 1)
                    {
                         continue; //skip odd lengths because 0101 is not counted -> 101
                    }
                    halfLength = (length + 1) / 2;
                    firstHalf = i / (long)Math.Pow(10, halfLength);
                    secondHalf = i % (long)Math.Pow(10, halfLength);
                    //Console.WriteLine($"{i.ToString()} : {i/(int)Math.Pow(10,halfLength)} & {i%Math.Pow(10,halfLength)}");
                    if(firstHalf == secondHalf)
                    {
                        sumOfInvalids += i;
                        Console.WriteLine($"Invalid: {i}");
                    }
                }
            }
            Console.WriteLine($"Total Sum of Invalids: {sumOfInvalids}");
        }

        public void part2()
        {
            long sumOfInvalids = 0;

            string[] parts;
            long startNumber, endNumber;

            int startLength, endLength, lengthRange;
            long currentNumber;
            HashSet<long> invalidNumbers = new HashSet<long>();

            foreach (var range in ranges)
            {
                Logger.Report($"Range: {range}");
                parts = range.Split('-');
                startNumber = long.Parse(parts[0]);
                endNumber = long.Parse(parts[1]);

                startLength = parts[0].Length;
                endLength = parts[1].Length;

                lengthRange = endLength - startLength;

                for (int length = startLength; length <= endLength; length++)
                {
                    Logger.Log($"  Processing length: {length}");

                    for (int repeatSize = 1; repeatSize <= length/2; repeatSize++)
                    {
                        Logger.Log($"    Processing repeat size: {repeatSize}");
                        if (length % repeatSize != 0)
                        {
                            Logger.Log($"        Skipping repeat size {repeatSize} for length {length} as it is not a multiple.");
                            continue; //skip lengths that are not multiple of repeat size
                        }
                        for( long repeatNumber = startNumber/(long)Math.Pow(10, length - repeatSize); repeatNumber < (long)Math.Pow(10, repeatSize); repeatNumber++)
                        {
                            // Process each repeatNumber
                            currentNumber = long.Parse(string.Concat(Enumerable.Repeat(repeatNumber.ToString(), length / repeatSize)));
                            Logger.Log($"        Processing repeat number: {repeatNumber}; Current number: {currentNumber}");
                            if (currentNumber > endNumber)
                            {
                                break; //no need to continue if we are past the end number
                            }

                            if(currentNumber >= startNumber && !invalidNumbers.Contains(currentNumber))
                            {
                                sumOfInvalids += currentNumber;
                                invalidNumbers.Add(currentNumber);
                                Logger.Log($"            Invalid: {currentNumber}");
                            }
                        }
                    }
                }
            }
            Logger.Report($"Total Sum of Invalids: {sumOfInvalids}");
        }
    }
}
