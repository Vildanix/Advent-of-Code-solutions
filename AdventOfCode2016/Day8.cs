using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2016

namespace AdventOfCode2016 {
    class Day8 {
        private int columns = 50;
        private int rows = 6;
        bool[] displayMatrix;

        public void createSolution() {
            /*
             * 50 x 6 px display
             * Instructions:
             * rect AxB
             * rotate row y=A by B
             * rotate column x=A by B
             */

            // test string
            string test = "rect 3x2\nrotate column x=1 by 1\nrotate row y=0 by 4\nrotate column x=1 by 1";
            //string[] displayInstructions = test.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // load and prepare file containing input
            string[] displayInstructions = File.ReadAllLines("../../Resources/input-day8.txt");

            // display matrix
            displayMatrix = new bool[columns * rows];

            foreach (string instruction in displayInstructions) {
                // parse instruction
                string[] instructionParts = instruction.Split(' ');

                // skip empty instructions
                if (instructionParts.Length < 2) continue;

                // proccess instruction
                Regex regex;
                Match match;
                switch (instructionParts[0]) {

                    case "rect":
                        regex = new Regex(@"(\d+)x(\d+)");
                        match = regex.Match(instructionParts[1]);
                        if (match.Success) {
                            // create new rectangle in top left corner
                            paintRect(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)); 
                        }
                        break;

                    case "rotate":
                        regex = new Regex(@"(\d+)");
                        match = regex.Match(instructionParts[2]);
                        
                        if (match.Success) {
                            // rotate row or column
                            if (instructionParts[1] == "row") {
                                rotateRow(int.Parse(match.Groups[1].Value), int.Parse(instructionParts[4]));
                            } else {
                                rotateColumn(int.Parse(match.Groups[1].Value), int.Parse(instructionParts[4]));
                            }
                        }
                        break;
                }
            }

            // display enabled pixels and count them
            int enabledCount = 0;
            for (int i = 0; i < columns * rows; i++) {
                if (displayMatrix[i]) {
                    enabledCount++;
                }

                Console.Write("{0}", displayMatrix[i] ? '#' : ' ');
                if (i % columns == columns - 1) {
                    Console.Write("\n");
                }
            }

            Console.WriteLine("Pixels lit: {0}", enabledCount);

            Console.ReadKey();
        }

        private void paintRect(int width, int height) {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    displayMatrix[x + y * columns] = true;
                }
            }
        }

        private void rotateColumn(int column, int steps) {
            steps = steps % rows;
            if (steps == 0)
                return;

            // cache first value and start shifting
            bool[] original = new bool[rows];
            for (int y = 0; y < rows; y++) {
                original[y] = displayMatrix[column + y * columns];
            }

            // shift to new position
            for (int y = 0; y < rows; y++) {
                displayMatrix[column + y * columns] = original[(y - steps + rows) % rows ];
            }
        }

        private void rotateRow(int row, int steps) {
            steps = steps % columns;
            if (steps == 0)
                return;

            // cache first value and start shifting
            bool[] original = new bool[columns];
            for (int x = 0; x < columns; x++) {
                original[x] = displayMatrix[x + row * columns];
            }

            // shift to new position
            for (int x = 0; x < columns; x++) {
                displayMatrix[x + row * columns] = original[(x - steps + columns) % columns];
            }
        }

    }
}
