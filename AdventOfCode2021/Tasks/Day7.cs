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
    class Day7 : DailySolution {

        public Day7(string baseInputFileName) : base(baseInputFileName) { }

        protected override void PrintFirstSolution(string inputFile)
        {
            /*
             * A giant whale has decided your submarine is its next meal, and it's much faster than you are. There's nowhere to run!
             * 
             * Suddenly, a swarm of crabs (each in its own tiny submarine - it's too deep for them otherwise) zooms in to rescue you!
             * They seem to be preparing to blast a hole in the ocean floor; sensors indicate a massive underground cave system just beyond where they're aiming!
             * 
             * The crab submarines all need to be aligned before they'll have enough power to blast a large enough hole for your submarine to get through. 
             * However, it doesn't look like they'll be aligned before the whale catches you! Maybe you can help?
             * 
             * There's one major catch - crab submarines can only move horizontally.
             * 
             * You quickly make a list of the horizontal position of each crab (your puzzle input). 
             * Crab submarines have limited fuel, so you need to find a way to make all of their horizontal positions match while requiring them to spend as little fuel as possible.
             * 
             * Determine the horizontal position that the crabs can align to using the least fuel possible. How much fuel must they spend to align to that position?
             */

            var solution = SolveAlignFuelCost(inputFile);
            Console.WriteLine($"Minimal fuel value to align all crabs to position {solution.Item1} is: {solution.Item2}");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /*
             * The crabs don't seem interested in your proposed solution. Perhaps you misunderstand crab engineering?
             * As it turns out, crab submarine engines don't burn fuel at a constant rate. Instead, each change of 1 step in horizontal
             * position costs 1 more unit of fuel than the last: the first step costs 1, the second step costs 2, the third step costs 3, and so on.
             * 
             * As each crab moves, moving further becomes more expensive. This changes the best horizontal position to align them all on
             * 
             * Determine the horizontal position that the crabs can align to using the least fuel possible so they can make you an escape route! 
             * How much fuel must they spend to align to that position?
             */

            var solution = SolveAlignFuelCostWithUnevenFuelConsumption(inputFile);
            Console.WriteLine($"Minimal fuel value to align all crabs to position {solution.Item1} is: {solution.Item2}");
        }

        private (int, int) SolveAlignFuelCost(string fileInput)
        {
            var crabPositions = fileInput.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var availablePositions = crabPositions.Max();
            var fuelCosts = new int[availablePositions];
            for (int targetPosition = 0; targetPosition < availablePositions; targetPosition++)
            {
                fuelCosts[targetPosition] = crabPositions.Aggregate(0, (fuelCost, crabPosition) => fuelCost + Math.Abs(crabPosition - targetPosition));
            }

            var minFuelCost = fuelCosts.Min();

            return (Array.IndexOf(fuelCosts, minFuelCost), minFuelCost);
        }

        private (int, int) SolveAlignFuelCostWithUnevenFuelConsumption(string fileInput)
        {
            var crabPositions = fileInput.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var availablePositions = crabPositions.Max();
            var fuelCosts = new int[availablePositions];
            for (int targetPosition = 0; targetPosition < availablePositions; targetPosition++)
            {
                fuelCosts[targetPosition] = crabPositions.Aggregate(0, (fuelCost, crabPosition) => fuelCost + GetArithmeticSequenceSum(Math.Abs(crabPosition - targetPosition)));
            }

            var minFuelCost = fuelCosts.Min();

            return (Array.IndexOf(fuelCosts, minFuelCost), minFuelCost);
        }

        private int GetArithmeticSequenceSum(int length)
        {
            return length * (1 + length) / 2;
        }
    }
}
