using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2021

namespace AdventOfCode2022.Tasks
{
    class Day1 : DailySolution {

        public Day1(string baseInputFileName) : base(baseInputFileName) { }

        protected override void PrintFirstSolution(string inputFile)
        {
            /*  
             *  Elfs carrying calories
             *  
             *  Find the Elf carrying the most Calories. How many total Calories is that Elf carrying?
             *  
             *  https://adventofcode.com/2022/day/1
             */

            var solution = SolveInput(inputFile);
            Console.WriteLine($"Elf carrying the most Calories is: {solution.Item1} with {solution.Item2} Calories");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /* 
             */

            Console.WriteLine($"Solution 2: ");
        }

        private (int, int) SolveInput(string input)
        {
            var readings = ParseInput(input);
            var maxValue = readings.Max();

            return (readings.IndexOf(maxValue) + 1, maxValue);
        }

        private List<int> ParseInput(string input)
        {
            return GetCaloriesForElfs(SeparateLines(input, StringSplitOptions.None)).ToList();
        }

        public IEnumerable<int> GetCaloriesForElfs(IEnumerable<string> input)
        {
            var elfCalories = 0;
            foreach (var item in input)
            {
                if (string.IsNullOrEmpty(item))
                {
                    yield return elfCalories;
                    elfCalories = 0;
                }
                else 
                    elfCalories += int.Parse(item);
            }

            yield return elfCalories;
        }
    }
}
