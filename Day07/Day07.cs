using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2025.Common;
using Common;

namespace AdventOfCode2025
{
    internal class Day07 : IDay
    {
        public static void Main()
        {
            Logger.Report("Hello from Day07!");
            Day07 day07 = new Day07();
            day07.loadInput();
            day07.part1();
            day07.part2();
        }
        string[] inputLines = Array.Empty<string>();
        bool[] beams = Array.Empty<bool>();

        public void loadInput()
        {
            inputLines = InputReader.ReadLines("Day07", "sample.txt").ToArray();
        }
        public void part1()
        {
            Logger.Report("Part 1");
            int width = inputLines[0].Length;
            beams = new bool[width];
            int totalSplits = 0;
            foreach (string line in inputLines)
            {
                for (int i = 0; i < width; i++)
                {
                    if (line[i] == 'S')
                    {
                        beams[i] = true;
                        continue;
                    }
                    if (line[i] == '^' && beams[i])
                    {
                        totalSplits++;
                        splitBeams(i);
                    }
                }
                Logger.Log(string.Join("", beams.Select(b => b ? '|' : '.')));
            }
            Logger.Report($"Total splits: {totalSplits}");
        }
        private void splitBeams(int index)
        {
            if (index < 0 || index >= beams.Length)
            {
                return;
            }
            if (!beams[index])
            {
                return;
            }
            if (index - 1 >= 0)
            {
                beams[index - 1] = true;
            }
            if (index + 1 < beams.Length)
            {
                beams[index + 1] = true;
            }
            beams[index] = false;
        }

        public class Particle
        {
            public List<Particle> NextParticles { get; } = new List<Particle>();
            public (int, int) position { get; set; }
        }

        Dictionary<(int, int), Particle> particleMap = new Dictionary<(int, int), Particle>();
        Particle startParticle = new Particle();

        public void part2()
        {
            Logger.Report("Part 2");
            int width = inputLines[0].Length;
            beams = new bool[width];
            long totalTimelines = 1;
            particleMap = new Dictionary<(int, int), Particle>();

            // Generate Graph
            for (int y = 0; y < inputLines.Length; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (inputLines[y][x] == 'S')
                    {
                        var newParticle = new Particle(){ position = (x, y) };
                        startParticle = newParticle;
                        particleMap[(x, y)] = startParticle;
                        beams[x] = true;
                        continue;
                    }

                    if (beams[x])
                    {
                        if (y - 1 >= 0 && particleMap.ContainsKey((x, y - 1)))
                        {
                            var particle = new Particle(){ position = (x, y) };
                            particleMap[(x, y - 1)].NextParticles.Add(particle);
                            particleMap.TryAdd((x, y), particle);
                        }

                        if (inputLines[y][x] == '^')
                        {
                            splitTimeline(x, y);
                        }
                    }
                }
            }

            // Print Graph
            totalTimelines = dfs(startParticle);

            Logger.Report($"Total timelines: {totalTimelines}");
        }

        public int dfs(Particle particle)
        {
            Logger.Log($"Visiting particle at {particle.position}");
            if (particle.NextParticles.Count == 0)
            {
                Logger.Log("Reached end of timeline");
                return 1;
            }
            int sum = 0;
            foreach (var next in particle.NextParticles)
            {
                sum += dfs(next);
                Logger.Log();
            }
            return sum;
        }

        private void splitTimeline(int x, int y)
        {
            if (x < 0 || x >= beams.Length)
            {
                return;
            }
            if (!beams[x])
            {
                return;
            }

            beams[x] = false;
            if (x - 1 >= 0) beams[x - 1] = true;
            if (x + 1 < beams.Length) beams[x + 1] = true;

            var leftKey = (x - 1, y + 1);
            var rightKey = (x + 1, y + 1);

            if (!particleMap.TryGetValue(leftKey, out var leftParticle))
            {
                leftParticle = new Particle(){ position = leftKey };
                particleMap[leftKey] = leftParticle;
            }

            if (!particleMap.TryGetValue(rightKey, out var rightParticle))
            {
                rightParticle = new Particle(){ position = rightKey };
                particleMap[rightKey] = rightParticle;
            }

            if (particleMap.TryGetValue((x, y), out var currentParticle))
            {
                currentParticle.NextParticles.Add(leftParticle);
                currentParticle.NextParticles.Add(rightParticle);
            }
        }
    }
}
