using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2016

namespace AdventOfCode2016 {
    class Day6 {

        public void createSolution() {
            // part 1
            /*
             * Day 6: Signals and Noise
             * Figure out most common character for each column
             */

            // contain words easter and advent (if sorted by least common character)
            string test = "eedadn\ndrvtee\neandsr\nraavrd\natevrs\ntsrnev\nsdttsa\nrasrtv\nnssdts\nntnada\nsvetve\ntesnvt\nvntsnd\nvrdear\ndvrsen\nenarar";
            
            // load and prepare file containing input
            String inputFile = File.ReadAllText("../../Resources/input-day6.txt");
            //string[] messages = test.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] messages = inputFile.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // add columns
            int columnCount = messages[0].Length;
            Dictionary<char, int>[] occurrences = new Dictionary<char, int>[columnCount];
            for (int i = 0; i < columnCount; i++) {
                occurrences[i] = new Dictionary<char, int>();
            }

            // count letters in each column
            foreach (string message in messages) {
                char[] letters = message.ToCharArray();
                for(int i = 0; i < columnCount; i++) {
                    if (occurrences[i].ContainsKey(letters[i])) {
                        occurrences[i][letters[i]]++;
                    } else {
                        occurrences[i].Add(letters[i], 1);
                    }
                }
            }

            // sort by most common letter and print it
            Console.WriteLine("First message: ");
            for (int i = 0; i < columnCount; i++) {
                Console.Write(occurrences[i].OrderByDescending(o => o.Value).First().Key);
            }
            Console.WriteLine("\n\nSecond message:");
            // part2: sort by least common letter and print it
            for (int i = 0; i < columnCount; i++) {
                Console.Write(occurrences[i].OrderBy(o => o.Value).First().Key);
            }
            Console.ReadKey();
        }

    }
}
