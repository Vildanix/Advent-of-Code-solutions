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

            var solution = SolveInput(inputFile, 1);
            Console.WriteLine($"Elf carrying the most Calories is: {solution.Item1} with {solution.Item2} Calories");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /* 
             *  Find first three elf with most Calories
             * 
             *  https://adventofcode.com/2022/day/1
             */
            int numberOfElves = 3;
            var solution = SolveInput(inputFile, numberOfElves);
            Console.WriteLine($"Top {numberOfElves} elves carrying the most Calories are: {solution.Item1} with {solution.Item2} Calories");
        }

        private (string, int) SolveInput(string input, int elfCount)
        {
            var readings = ParseInput(input);
            readings.Sort((a, b) => b.Item2 - a.Item2);
            var maxValues = readings.Take(elfCount);

            return (string.Join(", ", maxValues.Select(v => v.Item1.ToString())), maxValues.Sum(v => v.Item2));
        }

        private List<(int, int)> ParseInput(string input)
        {
            return GetCaloriesForElfs(SeparateLines(input, StringSplitOptions.None)).ToList();
        }

        public IEnumerable<(int, int)> GetCaloriesForElfs(IEnumerable<string> input)
        {
            var elfCalories = 0;
            var elfNumber = 1;
            foreach (var item in input)
            {
                if (string.IsNullOrEmpty(item))
                {
                    yield return (elfNumber, elfCalories);
                    elfCalories = 0;
                    elfNumber++;
                }
                else 
                    elfCalories += int.Parse(item);
            }

            yield return (elfNumber, elfCalories);
        }
    }
}
