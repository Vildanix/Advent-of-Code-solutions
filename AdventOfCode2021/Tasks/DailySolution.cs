using AdventOfCode2021.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Tasks
{
    abstract class DailySolution : IDailySoultion
    {
        private string baseFileName = "";

        public DailySolution(string baseInputFileName)
        {
            baseFileName = baseInputFileName;
        }

        public void CreateSolution()
        {
            String testInput = File.ReadAllText($"./Inputs/{baseFileName}_test.txt");

            // load and prepare file containing input
            String inputFile = File.ReadAllText($"./Inputs/{baseFileName}.txt");

            Console.WriteLine("Advent of code 2021");
            Console.WriteLine("-------------------");

            Console.Write("First solution (test): ");
            PrintFirstSolution(testInput);

            Console.Write("First solution: ");
            PrintFirstSolution(inputFile);

            Console.WriteLine();

            Console.Write("Second solution (test): ");
            PrintSecondSolution(testInput);

            Console.Write("Second solution: ");
            PrintSecondSolution(inputFile);

            Console.ReadKey();

        }

        protected abstract void PrintFirstSolution(string inputFile);

        protected abstract void PrintSecondSolution(string inputFile);

        protected IEnumerable<string> SeparateLines(string input, StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
        {
            return input.Split(new string[] { "\r\n", "\r", "\n" }, splitOptions);
        }
    }
}
