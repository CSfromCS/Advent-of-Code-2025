using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2025.Common;
using Common;

namespace AdventOfCode2025
{
    internal class Day05
    {
        struct freshRange
        {
            public long start;
            public long end;
        }
        freshRange[]? freshRanges;
        long[] ingredients = new long[] { };

        public static void Main()
        {
            Logger.Report("Hello from Day05!");
            Day05 day05 = new Day05();
            day05.part1();
            day05.part2();
        }

        public void loadData()
        {
            var lines = InputReader.ReadLines("Day05", "input.txt").ToArray();
            // find index of first empty line
            int sizeOfRanges = Array.IndexOf(lines, "");
            freshRanges = new freshRange[sizeOfRanges];

            int i = 0;
            while (!string.IsNullOrEmpty(lines[i]))
            {
                var parts = lines[i].Split('-');
                freshRange fr;
                fr.start = long.Parse(parts[0]);
                fr.end = long.Parse(parts[1]);
                Logger.Log($"Parsed fresh range: {fr.start}-{fr.end}");
                freshRanges[i] = fr;
                i++;
            }
            ingredients = lines[(1 + sizeOfRanges)..].Select(line => long.Parse(line)).ToArray();

            Logger.Report($"Loaded {freshRanges.Length} fresh ranges and {ingredients.Length} ingredients.");
        }



        public void part1()
        {
            Logger.Report("Part 1");
            loadData();
            // sort ingredients
            Array.Sort(ingredients);
            Logger.Log($"Sorted ingredients: {string.Join(", ", ingredients)}");
            Array.Sort(freshRanges!, (a, b) => a.start.CompareTo(b.start));

            int currentFreshRangeIndex = 0;
            int totalFreshIngredients = 0;

            int ingredientIndex = 0;
            while (ingredientIndex < ingredients.Length)
            {
                var ingredient = ingredients[ingredientIndex];
                if (ingredient < freshRanges![currentFreshRangeIndex].start)
                {
                    ingredientIndex++;
                    continue;
                }
                if (ingredient <= freshRanges![currentFreshRangeIndex].end)
                {
                    Logger.Log($"Ingredient {ingredient} is fresh (in range {freshRanges![currentFreshRangeIndex].start}-{freshRanges![currentFreshRangeIndex].end})");
                    ingredientIndex++;
                    totalFreshIngredients++;
                }
                else
                {
                    Logger.Log($"Done checking range {freshRanges![currentFreshRangeIndex].start}-{freshRanges![currentFreshRangeIndex].end}");
                    currentFreshRangeIndex++;
                    if (currentFreshRangeIndex >= freshRanges!.Length)
                    {
                        Logger.Log("No more fresh ranges to check.");
                        break;
                    }
                }
            }
            Logger.Report($"Total fresh ingredients: {totalFreshIngredients} out of {ingredients.Length}");
        }

        public void part2()
        {
            Logger.Report("Part 2");
            long totalFreshIngredients = 1;
            long currentID = freshRanges![0].start;
            for (int i = 0; i < freshRanges!.Length; i++)
            {
                Logger.Log($"Fresh range {i}: {freshRanges[i].start}-{freshRanges[i].end}");
                if(freshRanges[i].end < currentID)
                {
                    Logger.Log($"Range {i} is completely before currentID {currentID}, skipping.");
                    continue;
                }

                if (currentID < freshRanges[i].start)
                {
                    Logger.Log($"   There is a gap before range {i}: {currentID}-{freshRanges[i].start}. Setting currentID to {freshRanges[i].start}");
                    currentID = freshRanges[i].start;
                }
                else
                {
                    totalFreshIngredients--;
                }
                totalFreshIngredients += freshRanges[i].end - currentID + 1;
                currentID = freshRanges[i].end;
                Logger.Log($"       After processing range {i}, total fresh ingredients: {totalFreshIngredients}, currentID: {currentID}");
            }
            Logger.Report($"Total fresh ingredients (Part 2): {totalFreshIngredients}");
        }
    }
}