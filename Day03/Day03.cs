using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace AdventOfCode2025
{
    internal class Day03
    {
        string[] lines = new string[] { };
        public static void Main()
        {
            Console.WriteLine("Hello from Day03!");
            Day03 day03 = new Day03();
            day03.lines = InputReader.ReadLines("Day03", "input.txt").ToArray();
            //day03.part1();
            day03.part2();
        }

        public void part1()
        {
            int digit;
            int left, right;
            long total = 0;

            int lineLength;

            foreach (string line in lines)
            {
                //number = ulong.Parse(line);
                left = 0; right = 0;
                lineLength = line.Length;

                right = int.Parse(line[lineLength-1].ToString());
                lineLength--;

                while (lineLength > 0)
                {
                    // Process each nextDigit
                    digit = int.Parse(line[lineLength - 1].ToString());
                    lineLength--;

                    if (digit >= left)
                    {
                        right = Math.Max(left, right);
                        left = digit;
                    }
                }
                total += left*10 + right;
                Console.WriteLine($"Left: {left}, Right: {right}, Total so far: {total}");
            }
            Console.WriteLine($"Final Total: {total}");
        }

        public void part2()
        {
            int nextDigit;
            long total = 0, subtotal = 0;

            int lineLength;
            LinkedList<int> digits = new LinkedList<int>();
            int batteriesToConsider = 12;


            foreach (string line in lines)
            {
                lineLength = line.Length;

                digits = new LinkedList<int>();
                for(int i = 0; i < batteriesToConsider; i++)
                {
                    var digit = int.Parse(line[lineLength - 1 - i].ToString());
                    //Console.WriteLine($"    Adding initial digit: {digit}");
                    digits.AddFirst(digit);
                }
                lineLength -= batteriesToConsider;

                //foreach (var d in digits)
                //{
                //    Console.Write($"{d}");
                //}
                //Console.WriteLine();

                while (lineLength > 0)
                {
                    nextDigit = int.Parse(line[lineLength - 1].ToString());
                    lineLength--;

                    digits.AddFirst(nextDigit);

                    //foreach (var d in digits)
                    //{
                    //    Console.Write($"{d}");
                    //}
                    //Console.WriteLine();

                    var currentDigit = digits.First;
                    do
                    {
                        if (currentDigit.Value < currentDigit.Next.Value)
                        {
                            digits.Remove(currentDigit);
                            break;
                        }
                        currentDigit = currentDigit.Next;
                    }
                    while (currentDigit.Next != null);

                    if (digits.Count > batteriesToConsider)
                    {
                        digits.RemoveLast();
                    }
                }
                subtotal = 0;
                foreach (var digit in digits)
                {
                    subtotal *= 10;
                    subtotal += digit;
                }
                total += subtotal;
                Console.WriteLine($"Subtotal: {subtotal}, Total so far: {total}");
            }
            Console.WriteLine($"Final Total: {total}");
        }
    }
}
