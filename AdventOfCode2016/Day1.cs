using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2016

namespace AdventOfCode2016 {
    class Day1 {
        Dictionary<KeyValuePair<int, int>, int> walked;
        bool intersectionFound = false;

        public void createSolution() {
            /*  Distance to Easter Bunny HQ
             *  
             *  Following R2, L3 leaves you 2 blocks East and 3 blocks North, or 5 blocks away.
             *  R2, R2, R2 leaves you 2 blocks due South of your starting position, which is 2 blocks away.
             *  R5, L5, R5, R3 leaves you 12 blocks away.

             *  How many blocks away is Easter Bunny HQ?
             */

            //string instructions = "R2, L3";
            //string instructions = "R8, R4, R4, R8";
            string instructions = "L1, L5, R1, R3, L4, L5, R5, R1, L2, L2, L3, R4, L2, R3, R1, L2, R5, R3, L4, R4, L3, R3, R3, L2, R1, L3, R2, L1, R4, L2, R4, L4, R5, L3, R1, R1, L1, L3, L2, R1, R3, R2, L1, R4, L4, R2, L189, L4, R5, R3, L1, R47, R4, R1, R3, L3, L3, L2, R70, L1, R4, R185, R5, L4, L5, R4, L1, L4, R5, L3, R2, R3, L5, L3, R5, L1, R5, L4, R1, R2, L2, L5, L2, R4, L3, R5, R1, L5, L4, L3, R4, L3, L4, L1, L5, L5, R5, L5, L2, L1, L2, L4, L1, L2, R3, R1, R1, L2, L5, R2, L3, L5, L4, L2, L1, L2, R3, L1, L4, R3, R3, L2, R5, L1, L3, L3, L3, L5, R5, R1, R2, L3, L2, R4, R1, R1, R3, R4, R3, L3, R3, L5, R2, L2, R4, R5, L4, L3, L1, L5, L1, R1, R2, L1, R3, R4, R5, R2, R3, L2, L1, L5";
            int currentDirection = 0;   // 0 - north, 1 - east, 2 - south, 3 - west
            int x = 0;
            int y = 0;

            // for second part
            walked = new Dictionary<KeyValuePair<int, int>, int>();

            // prepare instructions
            string[] instructionList = instructions.Split(new string[] { ", " }, StringSplitOptions.None);
            foreach (string instruction in instructionList) {
                int numSteps = 0;
                int.TryParse(instruction.Substring(1), out numSteps);

                if (instruction[0] == 'L') {
                    currentDirection--;
                }
                else {
                    currentDirection++;
                }

                if (currentDirection < 0) {
                    currentDirection = 3;
                }

                if (currentDirection > 3) {
                    currentDirection = 0;
                }

                switch (currentDirection) {
                    case 0:
                        markWalkedY(x, y, y + numSteps);
                        y += numSteps;
                        break;
                    case 2:
                        markWalkedY(x, y, y - numSteps);
                        y -= numSteps;
                        break;
                    case 1:
                        markWalkedX(x, x + numSteps, y);
                        x += numSteps;
                        break;
                    case 3:
                        markWalkedX(x, x - numSteps, y);
                        x -= numSteps;
                        break;
                }
            }


            Console.Write("HQ: {0}, {1}, distance: {2}", x, y, Math.Abs(x) + Math.Abs(y));
            Console.ReadKey();
            /* Second part
             * 
             * Then, you notice the instructions continue on the back of the Recruiting Document. Easter Bunny HQ is actually at the first location you visit twice.
             *
             * For example, if your instructions are R8, R4, R4, R8, the first location you visit twice is 4 blocks away, due East.
             *
             * How many blocks away is the first location you visit twice?
             */

            //Console.ReadKey();

        }

        private void markWalkedY(int startX, int startY, int endY) {
            if (intersectionFound)
                return;

            if (startY > endY) {
                for (int y = startY - 1; y >= endY; y--) {
                    KeyValuePair<int, int> key = new KeyValuePair<int, int>(startX, y);
                    if (walked.ContainsKey(key)) {
                        Console.WriteLine("Visited {0}, {1}, distance: {2}", startX, y, Math.Abs(startX) + Math.Abs(y));
                        intersectionFound = true;
                    }
                    else {
                        walked.Add(key, 1);
                    }
                }

                return;
            }

            for (int y = startY + 1; y <= endY; y++) {
                KeyValuePair<int, int> key = new KeyValuePair<int, int>(startX, y);
                if (walked.ContainsKey(key)) {
                    Console.WriteLine("Visited {0}, {1}, distance: {2}", startX, y, Math.Abs(startX) + Math.Abs(y));
                    intersectionFound = true;
                }
                else {
                    walked.Add(key, 1);
                }
            }
        }

        private void markWalkedX(int startX, int endX, int startY) {
            if (intersectionFound)
                return;

            if (startX > endX) {
                for (int x = startX - 1; x >= endX; x--) {
                    KeyValuePair<int, int> key = new KeyValuePair<int, int>(x, startY);
                    if (walked.ContainsKey(key)) {
                        Console.WriteLine("Visited {0}, {1}, distance: {2}", x, startY, Math.Abs(x) + Math.Abs(startY));
                        intersectionFound = true;
                    }
                    else {
                        walked.Add(key, 1);
                    }
                }
                return;
            }

            for (int x = startX + 1; x <= endX; x++) {
                KeyValuePair<int, int> key = new KeyValuePair<int, int>(x, startY);
                if (walked.ContainsKey(key)) {
                    Console.WriteLine("Visited {0}, {1}, distance: {2}", x, startY, Math.Abs(x) + Math.Abs(startY));
                    intersectionFound = true;
                }
                else {
                    walked.Add(key, 1);
                }
            }
        }

    }
}
