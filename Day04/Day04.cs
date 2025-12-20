using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2025.Common;
using Common;
using Microsoft.Win32;

namespace AdventOfCode2025
{
    internal class Day04
    {
        string[][] map = new string[][] { };
        string[] lines;
        int rows, cols;
        int[][] countOfAdjacents;
        public static void Main()
        {
            Console.WriteLine("Hello from Day04!");
            Day04 day04 = new Day04();
            day04.LoadMap();
            day04.part1();
            day04.part2();
        }

        public void LoadMap()
        {
            lines = InputReader.ReadLines("Day04", "input.txt").ToArray();
            rows = lines.Length;
            cols = lines[0].Length;
        }
        public void AddToAdjacents(int r, int c)
        {
            for (int i = r - 1; i <= r + 1; i++)
            {
                for (int j = c - 1; j <= c + 1; j++)
                {
                    AddCountToAdjacent(i, j);
                }
            }
            countOfAdjacents[r][c]--;   // subtract the center cell
        }
        public void AddCountToAdjacent(int r, int c)
        {
            if(r < 0) { return; }
            if(c < 0) { return; }
            if(r >= rows) { return; }
            if(c >= cols) { return; }

            countOfAdjacents[r][c]++;
        }

        public void part1()
        {
            Console.WriteLine("Part 1");

            countOfAdjacents = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                countOfAdjacents[i] = new int[cols];
            }

            char ch;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    ch = lines[r][c];
                    if (ch == '@')
                    {
                        AddToAdjacents(r, c);
                    }
                }
            }

            int accessibleRolls = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    ch = lines[r][c];
                    if (ch == '@' && countOfAdjacents[r][c] < 4)
                    {
                        accessibleRolls++;
                    }
                    Logger.LogNoEnter(countOfAdjacents[r][c]);
                }
                Logger.Log();
            }
            Console.WriteLine($"Accessible Rolls: {accessibleRolls}");
        }

        public void part2()
        {
            Console.WriteLine("Part 2");
            int accessibleRolls;
            int totalRolls = 0;

            do
            {
                countOfAdjacents = new int[rows][];
                for (int i = 0; i < rows; i++)
                {
                    countOfAdjacents[i] = new int[cols];
                }

                char ch;
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        ch = lines[r][c];
                        if (ch == '@')
                        {
                            AddToAdjacents(r, c);
                        }
                    }
                }

                accessibleRolls = 0;
                for (int r = 0; r < rows; r++)
                {
                    string newLine = "";
                    for (int c = 0; c < cols; c++)
                    {
                        ch = lines[r][c];
                        if (ch == '@' && countOfAdjacents[r][c] < 4)
                        {
                            accessibleRolls++;
                            newLine += '.';
                        }
                        else
                        {
                            newLine += ch;
                        }
                        Logger.LogNoEnter(countOfAdjacents[r][c]);
                    }
                    Logger.Log();
                    lines[r] = newLine;
                }
                Logger.Report($"Accessible Rolls: {accessibleRolls}");
                totalRolls += accessibleRolls;
            } while (accessibleRolls > 0);
            Logger.Report($"Total Rolls: {totalRolls}");
        }
    }
}
