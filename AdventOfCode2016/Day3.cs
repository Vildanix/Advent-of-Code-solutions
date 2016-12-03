using System;
using System.Collections.Generic;
using System.IO;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2016

namespace AdventOfCode2016 {
    class Day3 {

        public void createSolution() {
            // part 1
            /*
             * Calculate impossible triangles from 3 number sequence given in input string
             * In your puzzle input, how many of the listed triangles are possible?
             */

            // test string
            string testString = "5  10  25";

            // load and prepare file containing input
            StreamReader inputFileStream = new StreamReader("../../Resources/input-day3.txt");
            String inputFile = inputFileStream.ReadToEnd();

            string[] coridorTriangles = inputFile.Split('\n');
            //string[] coridorTriangles = testString.Split(new char[] { '.' });

            // verify each triangle sequence
            string[] values;
            int[] valuesInt = new int[3];
            int validTriangleCount = 0;
            foreach (string triangle in coridorTriangles) {
                values = triangle.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // each triangle must have 3 sides
                if (values.Length < 3)
                    continue;

                valuesInt[0] = int.Parse(values[0]);
                valuesInt[1] = int.Parse(values[1]);
                valuesInt[2] = int.Parse(values[2]);

                if (isTriangle(valuesInt)) {
                    validTriangleCount++;
                }
            }

            Console.WriteLine("Valid triangle count: {0}", validTriangleCount);
            Console.ReadKey();

            // part 2

        }

        private bool isTriangle(int[] values) {
            Array.Sort(values);
            if (values[0] + values[1] > values[2]) {
                return true;
            }
            return false;
        }
    }
}
