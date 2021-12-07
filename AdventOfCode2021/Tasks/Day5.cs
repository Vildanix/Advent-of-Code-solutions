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
    class Day5 : DailySolution {

        private struct LineCoord
        {
            public int startX;
            public int startY;

            public int endX;
            public int endY;

            public bool IsAxisAligned()
            {
                return startX == endX || startY == endY;
            }
        }

        public Day5(string baseInputFileName) : base(baseInputFileName) { }

        protected override void PrintFirstSolution(string inputFile)
        {
            /*
             * You come across a field of hydrothermal vents on the ocean floor! These vents constantly produce large, opaque clouds, so it would be best to avoid them if possible.
             * They tend to form in lines; the submarine helpfully produces a list of nearby lines of vents (your puzzle input) for you to review.
             * 
             * Each line of vents is given as a line segment in the format x1,y1 -> x2,y2 where x1,y1 are the coordinates of one end the line segment and x2,y2 are the coordinates of the other end. 
             * These line segments include the points at both ends.
             * 
             * For now, only consider horizontal and vertical lines: lines where either x1 = x2 or y1 = y2.
             * 
             * To avoid the most dangerous areas, you need to determine the number of points where at least two lines overlap. In the above example,
             * this is anywhere in the diagram with a 2 or larger - a total of 5 points.
             * Consider only horizontal and vertical lines. At how many points do at least two lines overlap?
            */

            var solution = SolveAxisAlignedLines(inputFile);
            Console.WriteLine($"Number of right angle crossing is: {solution}");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /*
             * Unfortunately, considering only horizontal and vertical lines doesn't give you the full picture; you need to also consider diagonal lines.
             * Because of the limits of the hydrothermal vent mapping system, the lines in your list will only ever be horizontal,
             * vertical, or a diagonal line at exactly 45 degrees.
             * 
             * You still need to determine the number of points where at least two lines overlap. In the above example, 
             * this is still anywhere in the diagram with a 2 or larger - now a total of 12 points.
             * Consider all of the lines. At how many points do at least two lines overlap?
             */

            var solution = SolveAllLines(inputFile);
            Console.WriteLine($"Number of right angle crossing is: {solution}");
        }

        private int SolveAxisAlignedLines(string inputFile)
        {
            var lines = ParseCoords(inputFile).Where(coord => coord.IsAxisAligned()).ToList();
            var field = ConstructField(lines);

            return field.Where(cell => cell > 1).Count();
        }

        private int SolveAllLines(string inputFile)
        {
            var lines = ParseCoords(inputFile).ToList();
            var field = ConstructField(lines);

            return field.Where(cell => cell > 1).Count();
        }

        private int[] ConstructField(List<LineCoord> lines)
        {
            var fieldSize = lines.Select(coord => (Math.Max(coord.startX, coord.endX), Math.Max(coord.startY, coord.endY)))
                                 .Aggregate((0, 0), (max, current) => (Math.Max(max.Item1, current.Item1), Math.Max(max.Item2, current.Item2)));

            int fieldWitdh = fieldSize.Item1 + 1;
            int fieldHeight = fieldSize.Item2 + 1;

            int[] field = new int[fieldWitdh * fieldHeight];

            lines.ForEach(line => {
                int lineLength = Math.Max(Math.Abs(line.endX - line.startX), Math.Abs(line.endY - line.startY));

                for (int i = 0; i <= lineLength; i++)
                {
                    float proportion = i / (float)lineLength;
                    int x = line.startX + (int)Math.Round((line.endX - line.startX) * proportion);
                    int y = line.startY + (int)Math.Round((line.endY - line.startY) * proportion);

                    field[x + y * fieldWitdh]++;
                }
            });

            return field;
        }

        private IEnumerable<LineCoord> ParseCoords(string inputFile)
        {
            return SeparateLines(inputFile).Select(line =>
            {
                var coords = line.Split("->").Select(coord => coord.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray();

                return new LineCoord()
                {
                    startX = coords[0][0],
                    startY = coords[0][1],
                    endX = coords[1][0],
                    endY = coords[1][1],
                };
            }
            );
        }
    }
}
