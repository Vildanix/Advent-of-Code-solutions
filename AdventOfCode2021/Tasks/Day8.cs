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
    class Day8 : DailySolution {

        private readonly int[] RECOGNIZABLE_SEGMENTS = new int[] { 2, 4, 3, 7 };

        public Day8(string baseInputFileName) : base(baseInputFileName) { }

        protected override void PrintFirstSolution(string inputFile)
        {
            /*
             * You barely reach the safety of the cave when the whale smashes into the cave mouth, collapsing it. 
             * Sensors indicate another exit to this cave at a much greater depth, so you have no choice but to press on.
             * 
             * As your submarine slowly makes its way through the cave system, you notice that the four-digit seven-segment displays 
             * in your submarine are malfunctioning; they must have been damaged during the escape. 
             * You'll be in a lot of trouble without them, so you'd better figure out what's wrong.
             * 
             * The problem is that the signals which control the segments have been mixed up on each display. 
             * The submarine is still trying to display numbers by producing output on signal wires a through g, 
             * but those wires are connected to segments randomly. Worse, the wire/segment connections are mixed up 
             * separately for each four-digit display! (All of the digits within a display use the same connections, though.)
             * 
             * So, you might know that only signal wires b and g are turned on, but that doesn't mean segments b and g are turned on:
             *      the only digit that uses two segments is 1, so it must mean segments c and f are meant to be on. 
             *      With just that information, you still can't tell which wire (b/g) goes to which segment (c/f). 
             *      For that, you'll need to collect more information.
             *      
             * For each display, you watch the changing signals for a while, make a note of all ten unique signal patterns you see,
             * and then write down a single four digit output value (your puzzle input).
             * Using the signal patterns, you should be able to work out which pattern corresponds to which digit.
             */

            var solution = SolveSelectedNumberOccurence(inputFile);
            Console.WriteLine($"Number of segment 1, 4, 7 and 8 occured in output is {solution}");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /*
             * Through a little deduction, you should now be able to determine the remaining digits.
             * 
             * For each entry, determine all of the wire/segment connections and decode the four-digit output values. 
             * What do you get if you add up all of the output values?
             */

            var solution = SolveOutputNumberSum(inputFile);
            Console.WriteLine($"Sum of displayed outputs is {solution}");
        }

        private int SolveSelectedNumberOccurence(string inputFile)
        {
            return SeparateLines(inputFile).Select(line => line.Split('|', StringSplitOptions.RemoveEmptyEntries).Last())
                                           .Select(output => output.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                           .Where(symbol => RECOGNIZABLE_SEGMENTS.Contains(symbol.Length))
                                           .Count())
                                           .Sum();
        }

        private int SolveOutputNumberSum(string inputFile)
        {
            return SeparateLines(inputFile).Select(line =>
            {
                var parts = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                var inputs = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var unrecognizedSymbols = inputs.Where(symbol => !RECOGNIZABLE_SEGMENTS.Contains(symbol.Length)).Select(symbol => symbol.ToCharArray());

                Dictionary<int, char[]> symbols = new Dictionary<int, char[]>();
                symbols.Add(1, inputs.Where(symbol => symbol.Length == 2).First().ToCharArray());
                symbols.Add(4, inputs.Where(symbol => symbol.Length == 4).First().ToCharArray());
                symbols.Add(7, inputs.Where(symbol => symbol.Length == 3).First().ToCharArray());
                symbols.Add(8, inputs.Where(symbol => symbol.Length == 7).First().ToCharArray());

                symbols.Add(3, unrecognizedSymbols.Where(symbol => symbol.Length == 5).Where(symbol => IsSegmentsFullOverlap(symbols[7], symbol)).First());
                unrecognizedSymbols = unrecognizedSymbols.Where(symbol => !IsSegmentsEquals(symbol, symbols[3]));
                symbols.Add(9, unrecognizedSymbols.Where(symbol => symbol.Length == 6).Where(symbol => IsSegmentsFullOverlap(symbols[4], symbol)).First());
                unrecognizedSymbols = unrecognizedSymbols.Where(symbol => !IsSegmentsEquals(symbol, symbols[9]));
                symbols.Add(0, unrecognizedSymbols.Where(symbol => symbol.Length == 6).Where(symbol => IsSegmentsFullOverlap(symbols[7], symbol)).First());
                unrecognizedSymbols = unrecognizedSymbols.Where(symbol => !IsSegmentsEquals(symbol, symbols[0]));
                symbols.Add(6, unrecognizedSymbols.Where(symbol => symbol.Length == 6).First());
                unrecognizedSymbols = unrecognizedSymbols.Where(symbol => !IsSegmentsEquals(symbol, symbols[6]));
                symbols.Add(5, unrecognizedSymbols.Where(symbol => symbol.Length == 5).Where(symbol => IsSegmentsFullOverlap(symbol, symbols[6])).First());
                unrecognizedSymbols = unrecognizedSymbols.Where(symbol => !IsSegmentsEquals(symbol, symbols[5]));
                symbols.Add(2, unrecognizedSymbols.Where(symbol => symbol.Length == 5).First());

                // parse output value
                var numParts = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                               .Select(symbol => symbols.Where(seq => IsSegmentsEquals(seq.Value, symbol.ToCharArray())).First().Key);

                int repairedNumber = 0;
                for (int i = 0; i < numParts.Count(); i++)
                {
                    repairedNumber += numParts.ElementAt(numParts.Count() - i - 1) * (int)Math.Pow(10, i);
                }

                return repairedNumber;
            }).Sum();
        }

        private bool IsSegmentsFullOverlap(char[] symbol, char[] compared)
        {
            return symbol.Intersect(compared).Count() == symbol.Length;
        }

        private bool IsSegmentsEquals(char[] symbol, char[] compared)
        {
            return symbol.All(segment => compared.Contains(segment)) && compared.All(segment => symbol.Contains(segment));
        }
    }
}
