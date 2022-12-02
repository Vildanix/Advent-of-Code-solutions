using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2022

namespace AdventOfCode2022.Tasks
{
    class Day2 : DailySolution {

        public Day2(string baseInputFileName) : base(baseInputFileName) { }

        protected override void PrintFirstSolution(string inputFile)
        {
            /*  
             *  Get total score for rock paper scizors with strategy guide
             *  
             *  https://adventofcode.com/2022/day/2
             */

            var solution = SolveInput(inputFile);
            Console.WriteLine($"Total score with guide: {solution}");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /*  
             *  
             *  https://adventofcode.com/2022/day/2
             */
            Console.WriteLine($"Solution");
        }

        private int SolveInput(string input)
        {
            return ParseInput(input).Select(GetRoundScore).Select(s => s.Item1 + s.Item2).Sum();
        }

        private List<string> ParseInput(string input)
        {
            return SeparateLines(input).ToList();
        }

        private (int, int) GetRoundScore(string round)
        {
            Dictionary<string, (int, int)> scoreValues = new Dictionary<string, (int, int)>
            {
                ["A X"] = (3, 1),
                ["A Y"] = (6, 2),
                ["A Z"] = (0, 3),
                ["B X"] = (0, 1),
                ["B Y"] = (3, 2),
                ["B Z"] = (6, 3),
                ["C X"] = (6, 1),
                ["C Y"] = (0, 2),
                ["C Z"] = (3, 3),
            };

            return scoreValues[round];
        }
    }
}
