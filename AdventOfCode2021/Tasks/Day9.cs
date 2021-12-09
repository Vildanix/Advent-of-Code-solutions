using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2021

namespace AdventOfCode2021.Tasks
{
    class Day9 : DailySolution {

        public Day9(string baseInputFileName) : base(baseInputFileName) { }

        protected override void PrintFirstSolution(string inputFile)
        {
            /*
             * These caves seem to be lava tubes. Parts are even still volcanically active; small hydrothermal vents release smoke into the caves that slowly settles like rain.
             * If you can model how the smoke flows through the caves, you might be able to avoid it and be that much safer. 
             * The submarine generates a heightmap of the floor of the nearby caves for you (your puzzle input).
             * 
             * Smoke flows to the lowest point of the area it's in.
             * 
             * Your first goal is to find the low points - the locations that are lower than any of its adjacent locations. Most locations have four adjacent locations (up, down, left, and right); 
             * locations on the edge or corner of the map have three or two adjacent locations, respectively. (Diagonal locations do not count as adjacent.)
             * 
             * The risk level of a low point is 1 plus its height.
             * Find all of the low points on your heightmap. What is the sum of the risk levels of all low points on your heightmap?
             */

            var solution = SolveRiskLevel(inputFile);
            Console.WriteLine($"Local minima detected: {solution.Item1}, Sum of risk levels is {solution.Item2}");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /*
             * Next, you need to find the largest basins so you know what areas are most important to avoid.
             * A basin is all locations that eventually flow downward to a single low point. Therefore, every low point has a basin, 
             * although some basins are very small. Locations of height 9 do not count as being in any basin, 
             * and all other locations will always be part of exactly one basin.
             * 
             * The size of a basin is the number of locations within the basin, including the low point. The example above has four basins.
             * 
             * What do you get if you multiply together the sizes of the three largest basins?
             */

            var solution = SolveBasins(inputFile);
            Console.WriteLine($"Largest 3 basins detected: {solution.Item1}, product {solution.Item2}");
        }

        private (string, int) SolveRiskLevel(string fileInput)
        {
            var map = LoadMap(fileInput);
            var localMinima = GetLocalMinima(map);
            var localMinimaValues = localMinima.Select(min => map[min.Item2][min.Item1]);

            return (string.Join(',', localMinimaValues), localMinimaValues.Select(min => min + 1).Sum());
        }

        private (string, int) SolveBasins(string fileInput)
        {
            var map = LoadMap(fileInput);
            var localMinima = GetLocalMinima(map);
            var basins = localMinima.Select(minimum => GetBasinSize(minimum, map)).ToList();
            basins.Sort();
            return (string.Join(',', basins.TakeLast(3)), basins.TakeLast(3).Aggregate(1, (acc, basinSize) => acc *= basinSize));
        }

        private List<(int, int)> GetLocalMinima(int[][] map)
        {
            var width = map.First().Length;
            var height = map.Length;
            var localMinima = new List<(int, int)>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var current = map[y][x];
                    if (x > 0 && map[y][x - 1] <= current) continue;
                    if (y > 0 && map[y - 1][x] <= current) continue;
                    if (x < width - 1 && map[y][x + 1] <= current) continue;
                    if (y < height - 1 && map[y + 1][x] <= current) continue;

                    localMinima.Add((x, y));
                }
            }

            return localMinima;
        }

        private int GetBasinSize((int, int) localMinimum, int[][] map)
        {
            var width = map.First().Length;
            var height = map.Length;
            var basinCells = new List<(int, int)>();
            var newCells = new List<(int, int)>() { localMinimum };

            while (newCells.Count > 0)
            {
                basinCells.AddRange(newCells);
                newCells.Clear();

                foreach (var cell in basinCells)
                {
                    int x = cell.Item1;
                    int y = cell.Item2;
                    var current = map[y][x];

                    if (x > 0 && map[y][x - 1] >= current && map[y][x - 1] < 9 && !basinCells.Contains((x - 1, y))) newCells.Add((x - 1, y));
                    if (y > 0 && map[y - 1][x] >= current && map[y - 1][x] < 9 && !basinCells.Contains((x, y - 1))) newCells.Add((x, y - 1));
                    if (x < width - 1 && map[y][x + 1] >= current && map[y][x + 1] < 9 && !basinCells.Contains((x + 1, y))) newCells.Add((x + 1, y));
                    if (y < height - 1 && map[y + 1][x] >= current && map[y + 1][x] < 9 && !basinCells.Contains((x, y + 1))) newCells.Add((x, y + 1));

                    newCells = newCells.GroupBy(cell => cell).Select(cells => cells.FirstOrDefault()).ToList();
                }
            }

            return basinCells.Count;
        }

        private int[][] LoadMap(string fileInput)
        {
            return SeparateLines(fileInput).Select(line => line.ToCharArray()).Select(numbers => numbers.Select(symbol => int.Parse(symbol.ToString())).ToArray()).ToArray();
        }
    }
}
