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
    class Day6 : DailySolution {

        public Day6(string baseInputFileName) : base(baseInputFileName) { }

        protected override void PrintFirstSolution(string inputFile)
        {
            /*
             * The sea floor is getting steeper. Maybe the sleigh keys got carried this way?
             * A massive school of glowing lanternfish swims past. They must spawn quickly to reach such large numbers
             * - maybe exponentially quickly? You should model their growth rate to be sure.
             * Although you know nothing about this specific species of lanternfish, you make some guesses about their attributes. 
             * Surely, each lanternfish creates a new lanternfish once every 7 days.
             * However, this process isn't necessarily synchronized between every lanternfish - one lanternfish might have 2 days left
             * until it creates another lanternfish, while another might have 4. So, you can model each fish as a single number
             * that represents the number of days until it creates a new lanternfish.
             * 
             * Furthermore, you reason, a new lanternfish would surely need slightly longer before it's capable of producing more lanternfish: two more days for its first cycle.
             * So, suppose you have a lanternfish with an internal timer value of 3:
             *  After one day, its internal timer would become 2.
             *  After another day, its internal timer would become 1.
             *  After another day, its internal timer would become 0.
             *  After another day, its internal timer would reset to 6, and it would create a new lanternfish with an internal timer of 8.
             *  After another day, the first lanternfish would have an internal timer of 5, and the second lanternfish would have an internal timer of 7.
             *  
             *  A lanternfish that creates a new fish resets its timer to 6, not 7 (because 0 is included as a valid timer value). 
             *  The new lanternfish starts with an internal timer of 8 and does not start counting down until the next day.
            */

            //var solution = SolveLanternfishGenerations(inputFile); // replaced with more efficinet method
            var solution = SolveLanternfishGenerationsEfectively(inputFile, 80);
            Console.WriteLine($"Number of lanternfish after 80 generations is: {solution}");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /*
             * Suppose the lanternfish live forever and have unlimited food and space. Would they take over the entire ocean?
             * How many lanternfish would there be after 256 days?
             */

            var solution = SolveLanternfishGenerationsEfectively(inputFile, 256);
            Console.WriteLine($"Number of lanternfish after 256 generations is: {solution}");
        }

        private int SolveLanternfishGenerations(string inputFile)
        {
            var lanterFishTimers = inputFile.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            for (int i = 0; i < 80; i++)
            {
                var newLanternFish = lanterFishTimers.Where(timer => timer == 0).Count();
                lanterFishTimers = lanterFishTimers.Select(timer => timer == 0 ? 6 : --timer).ToList();
                lanterFishTimers.AddRange(Enumerable.Repeat(8, newLanternFish));
            }

            return lanterFishTimers.Count();
        }

        private long SolveLanternfishGenerationsEfectively(string inputFile, int generations)
        {
            var lanterFishTimers = new Dictionary<int, long>() { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 } };
            var initialTimers = inputFile.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            foreach (var timer in initialTimers)
            {
                lanterFishTimers[timer]++;
            }
                                            
            long reproducers = 0;
            for (int i = 0; i < generations; i++)
            {
                reproducers = lanterFishTimers[0];
                for (int timer = 1; timer <= 8; timer++)
                {
                    lanterFishTimers[timer - 1] = lanterFishTimers[timer];
                }
                lanterFishTimers[6] += reproducers;
                lanterFishTimers[8] = reproducers;
            }

            return lanterFishTimers.Sum(group => group.Value);
        }

    }
}
