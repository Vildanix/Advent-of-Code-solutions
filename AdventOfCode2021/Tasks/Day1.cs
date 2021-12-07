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
    class Day1 : DailySolution {

        public Day1(string baseInputFileName) : base(baseInputFileName) { }

        protected override void PrintFirstSolution(string inputFile)
        {
            /*  Distance to Easter Bunny HQ
             *  
             *  As the submarine drops below the surface of the ocean, it automatically performs a sonar sweep of the nearby sea floor. 
             *  On a small screen, the sonar sweep report (your puzzle input) appears: each line is a measurement of the sea floor depth 
             *  as the sweep looks further and further away from the submarine.

             *  The first order of business is to figure out how quickly the depth increases, just so you know what you're dealing with 
             *  - you never know if the keys will get carried into deeper water by an ocean current or a fish or something.
             *  To do this, count the number of times a depth measurement increases from the previous measurement. (There is no measurement before the first measurement.)
             */

            Console.WriteLine($"Number of larger coordinates: {SolveInput(inputFile)}");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /*  Distance to Easter Bunny HQ
             *  
             *  As the submarine drops below the surface of the ocean, it automatically performs a sonar sweep of the nearby sea floor. 
             *  On a small screen, the sonar sweep report (your puzzle input) appears: each line is a measurement of the sea floor depth 
             *  as the sweep looks further and further away from the submarine.

             *  The first order of business is to figure out how quickly the depth increases, just so you know what you're dealing with 
             *  - you never know if the keys will get carried into deeper water by an ocean current or a fish or something.
             *  To do this, count the number of times a depth measurement increases from the previous measurement. (There is no measurement before the first measurement.)
             */

            Console.WriteLine($"Number of larger coordinates: {SolveInputSlidingWindow(inputFile)}");
        }

        private int SolveInput(string input)
        {
            var readings = ConvertInputToNumbers(input);

            return readings.Where((reading, index) => index > 0 && reading > readings[index - 1]).Count();
        }

        private int SolveInputSlidingWindow(string input)
        {
            var readings = ConvertInputToNumbers(input);

            var slidingWindowValues = readings.Where((reading, index) => index < readings.Length - 2)
                                              .Select((reading, index) => reading + readings[index + 1] + readings[index + 2])
                                              .ToArray();

            return slidingWindowValues.Where((reading, index) => index > 0 && reading > slidingWindowValues[index - 1]).Count();
        }

        private int[] ConvertInputToNumbers(string input)
        {
            return SeparateLines(input).Select(int.Parse).ToArray();
        }
    }
}
