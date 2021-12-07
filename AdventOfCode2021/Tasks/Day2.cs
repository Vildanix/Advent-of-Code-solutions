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
    class Day2: DailySolution {
        public Day2(string baseInputFileName) : base(baseInputFileName) { }

        private enum COMMAND_TYPE {
            FORWARD = 1,
            UP = 2,
            DOWN = 3
        }

        protected override void PrintFirstSolution(string inputFile)
        {
            /*  
             *  Now, you need to figure out how to pilot this thing.
             *  It seems like the submarine can take a series of commands like forward 1, down 2, or up 3:
             *  
             *  forward X increases the horizontal position by X units.
             *  down X increases the depth by X units.
             *  up X decreases the depth by X units.
             *  
             *  Note that since you're on a submarine, down and up affect your depth, and so they have the opposite result of what you might expect.
             *  The submarine seems to already have a planned course (your puzzle input)
             */
            var solution = SolveInput(inputFile);
            Console.WriteLine($"Distance: {solution.Item1}, Depth: {solution.Item2}, Result: {solution.Item1 * solution.Item2}");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /*
             * Based on your calculations, the planned course doesn't seem to make any sense. You find the submarine manual and discover that
             * the process is actually slightly more complicated.
             * In addition to horizontal position and depth, you'll also need to track a third value, aim, which also starts at 0.
             * The commands also mean something entirely different than you first thought:
             * 
             * down X increases your aim by X units.
             * up X decreases your aim by X units.
             * forward X does two things:
             *      It increases your horizontal position by X units.
             *      It increases your depth by your aim multiplied by X.
             *      
             * Again note that since you're on a submarine, down and up do the opposite of what you might expect: "down" means aiming in the positive direction..
             * After following these new instructions, you would have a horizontal position of 15 and a depth of 60. (Multiplying these produces 900.)
             * Using this new interpretation of the commands, calculate the horizontal position and depth you would have after following the planned course.
             * What do you get if you multiply your final horizontal position by your final depth?
            */
            var solution = SolveInputWithHeading(inputFile);
            Console.WriteLine($"Distance: {solution.Item1}, Depth: {solution.Item2}, Result: {solution.Item1 * solution.Item2}");
        }

        private Tuple<int, int> SolveInput(string input)
        {
            int horizontalDistance = 0;
            int depth = 0;

            SeparateLines(input).ToList().ForEach(instruction => {
                var command = ParseInstruction(instruction);
                switch (command.Item1) {
                    case COMMAND_TYPE.FORWARD:
                        horizontalDistance += command.Item2;
                        break;
                    case COMMAND_TYPE.UP:
                        depth -= command.Item2;
                        break;
                    case COMMAND_TYPE.DOWN:
                        depth += command.Item2;
                        break;
                }
            });

            return new Tuple<int, int>(horizontalDistance, depth);
        }

        private Tuple<int, int> SolveInputWithHeading(string input)
        {
            int horizontalDistance = 0;
            int depth = 0;
            int heading = 0;

            SeparateLines(input).ToList().ForEach(instruction => {
                var command = ParseInstruction(instruction);
                switch (command.Item1)
                {
                    case COMMAND_TYPE.FORWARD:
                        horizontalDistance += command.Item2;
                        depth += heading * command.Item2;
                        break;
                    case COMMAND_TYPE.UP:
                        heading -= command.Item2;
                        break;
                    case COMMAND_TYPE.DOWN:
                        heading += command.Item2;
                        break;
                }
            });

            return new Tuple<int, int>(horizontalDistance, depth);
        }

        private Tuple<COMMAND_TYPE, int> ParseInstruction(string command)
        {
            var parts = command.Split(' ');
            switch (parts[0])
            {
                case "forward":
                    return new Tuple<COMMAND_TYPE, int>(COMMAND_TYPE.FORWARD, int.Parse(parts[1]));
                case "up":
                    return new Tuple<COMMAND_TYPE, int>(COMMAND_TYPE.UP, int.Parse(parts[1]));
                default:
                    return new Tuple<COMMAND_TYPE, int>(COMMAND_TYPE.DOWN, int.Parse(parts[1]));
            }
            
        }
    }
}
