using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2021

namespace AdventOfCode2021 {
    class Day1 {

        public void createSolution() {
            /*  Distance to Easter Bunny HQ
             *  
             *  As the submarine drops below the surface of the ocean, it automatically performs a sonar sweep of the nearby sea floor. 
             *  On a small screen, the sonar sweep report (your puzzle input) appears: each line is a measurement of the sea floor depth 
             *  as the sweep looks further and further away from the submarine.

             *  The first order of business is to figure out how quickly the depth increases, just so you know what you're dealing with 
             *  - you never know if the keys will get carried into deeper water by an ocean current or a fish or something.
             *  To do this, count the number of times a depth measurement increases from the previous measurement. (There is no measurement before the first measurement.)
             */

            String testInput = File.ReadAllText("./Inputs/input_day1_test.txt");

            // load and prepare file containing input
            String inputFile = File.ReadAllText("./Inputs/input_day1.txt");

            // sort by most common letter and print it
            Console.Write("Test first solution: ");
            Console.WriteLine($"Number of larger coordinates: {SolveInput(testInput)}");

            Console.Write("First solution: ");
            Console.WriteLine($"Number of larger coordinates: {SolveInput(inputFile)}");

            /*  Distance to Easter Bunny HQ
             *  
             *  Considering every single measurement isn't as useful as you expected: there's just too much noise in the data.
             *  Instead, consider sums of a three-measurement sliding window.
             *  
             *  The first order of business is to figure out how quickly the depth increases, just so you know what you're dealing with
             *  - you never know if the keys will get carried into deeper water by an ocean current or a fish or something.
             *  To do this, count the number of times a depth measurement increases from the previous measurement. (There is no measurement before the first measurement.)
             */

            Console.Write("Test second solution: ");
            Console.WriteLine($"Number of larger coordinates: {SolveInputSlidingWindow(testInput)}");

            Console.Write("Second solution: ");
            Console.WriteLine($"Number of larger coordinates: {SolveInputSlidingWindow(inputFile)}");

            Console.ReadKey();

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
            return input.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        }
    }
}
