using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2025.Common;
using Common;

namespace AdventOfCode2025
{
    internal class Day06 : IDay
    {
        string[] lines;
        long[] totals;
        public static void Main()
        {
            Logger.Report("Hello from Day06!");
            Day06 day06 = new Day06();
            day06.loadLines();
            day06.part1();
            day06.part2();
        }

        public void loadLines()
        {
            lines = InputReader.ReadLines("Day06", "input.txt");
        }

        public void part1()
        {
            Logger.Report("Part 1");
            string[] operands = lines[lines.Length - 1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);


            foreach (string operand in operands)
            {
                Logger.Log(operand);
            }
            foreach (string line in lines[..(lines.Length - 1)])
            {
                Logger.Log(line);
                long[] numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(n => long.Parse(n)).ToArray();

                if (totals == null)
                {
                    totals = (long[])numbers.Clone();
                }
                else
                {
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        if (operands[i] == "+")
                        {
                            totals[i] += numbers[i];
                        }
                        else if (operands[i] == "*")
                        {
                            totals[i] *= numbers[i];
                        }
                    }
                }
                Logger.Log($"Totals: {string.Join(", ", totals)}");
            }
            Logger.Log($"Final Totals: {string.Join(", ", totals)}");
            Logger.Report($"Final Sum: {totals.Sum()}");
        }

        public void part2()
        {
            foreach (string line in lines)
            {
                Logger.Log(line);
            }

            string allText = "";
            string currentProblem = "";

            long totalSum = 0;


            for (int i = lines[0].Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    if (lines[j][i] == '+' || lines[j][i] == '*')
                    {
                        allText += " \n";
                    }
                    switch(lines[j][i])
                    {
                        case '+':
                            totalSum += currentProblem.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).AsLongs().Sum();
                            Logger.Log($"Current Problem: {currentProblem}, Total Sum: {totalSum}");
                            currentProblem = "";
                            break;
                        case '*':
                            totalSum += currentProblem.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).AsLongs().Aggregate((long)1, (acc, x) => acc * x);
                            Logger.Log($"Current Problem: {currentProblem}, Total Sum: {totalSum}");
                            currentProblem = "";
                            break;
                        default:
                                currentProblem += lines[j][i];
                            break;
                    }
                }
            }
            Logger.Report($"Part 2: {totalSum}");
        }
    }
}
