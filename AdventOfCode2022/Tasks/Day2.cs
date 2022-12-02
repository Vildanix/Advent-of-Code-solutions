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
             *  Get total score for rock paper scissors with strategy guide
             *  assuming required shape in each round
             *  
             *  https://adventofcode.com/2022/day/2
             */

            var solution = SolveInput(inputFile);
            Console.WriteLine($"Total score with guide: {solution}");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /*  
             *  Get total score for rock paper scissors with strategy guide
             *  with required outcome in each round
             *  
             *  https://adventofcode.com/2022/day/2
             */
            var solution = SolveInputWithExpectedResult(inputFile);
            Console.WriteLine($"Total score with guide: {solution}");
        }

        private int SolveInput(string input)
        {
            return ParseInput(input).Select(GetRoundScoreByAction).Select(s => s.Item1 + s.Item2).Sum();
        }

        private int SolveInputWithExpectedResult(string input)
        {
            return ParseInput(input).Select(GetRoundScoreByResult).Select(s => s.Item1 + s.Item2).Sum();
        }

        private List<string> ParseInput(string input)
        {
            return SeparateLines(input).ToList();
        }

        private (int, int) GetRoundScoreByAction(string round)
        {
            // X - rock (1), Y - paper (2), Z - scissor (3)
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

        private (int, int) GetRoundScoreByResult(string round)
        {
            // X - lose (0), Y - draw (3), Z - win (6)
            Dictionary<string, (int, int)> scoreValues = new Dictionary<string, (int, int)>
            {
                ["A X"] = (0, 3),
                ["A Y"] = (3, 1),
                ["A Z"] = (6, 2),
                ["B X"] = (0, 1),
                ["B Y"] = (3, 2),
                ["B Z"] = (6, 3),
                ["C X"] = (0, 2),
                ["C Y"] = (3, 3),
                ["C Z"] = (6, 1),
            };

            return scoreValues[round];
        }
    }
}
