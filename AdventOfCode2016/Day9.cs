using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2016

namespace AdventOfCode2016 {
    class Day9 {

        public void createSolution() {
            /*
             * Comporessed input contains instructions for decompressing datablock following
             * instructions are in (AxB) format, where A is length of following datablock and B is
             * number of repeat this datablock should be placed to output.
             */

            // test string
            string test = "X(8x2)(3x3)ABCY";
            
            // load original file and remove white space charaters
            string fileData = File.ReadAllText("../../Resources/input-day9.txt");
            fileData = Regex.Replace(fileData, @"\s+", "");

            // decode file input
            string decodedFile = decodeFile(fileData);

            // get file length
            //Console.WriteLine("{0}", decodedFile);
            Console.WriteLine("Decoded file length {0}", decodedFile.Length);

            Console.ReadKey();
        }

        private string decodeFile(string fileText) {
            StringBuilder output = new StringBuilder();
            string instruction = "";
            Regex regex = new Regex(@"(\d+)x(\d+)");
            bool readInstruction = false;
            int dataBlockLength = 0, repeat = 0;
            char[] fileSymbols = fileText.ToCharArray();

            for (int i = 0; i < fileSymbols.Length; i++) {
                // switching between instruction and datablock
                if (!readInstruction && fileSymbols[i] == '(') {
                    readInstruction = true;

                // if current istruction is completed, stop reading instruction,
                // decode it and move datablock for this instruction to output
                } else if (readInstruction && fileSymbols[i] == ')') {
                    Match match = regex.Match(instruction);
                    int.TryParse(match.Groups[1].Value, out dataBlockLength);
                    int.TryParse(match.Groups[2].Value, out repeat);

                    // prepare datablock for this instruction
                    string dataBlock = new string(fileSymbols.Skip(i + 1).Take(dataBlockLength).ToArray());

                    // append datablock to new output
                    for (int cycle = 0; cycle < repeat; cycle++) {
                        output.Append(dataBlock);
                    }

                    // reset instruction
                    instruction = "";
                    readInstruction = false;

                    // move current decoding position after datablock
                    i += dataBlockLength;
                    continue;
                }

                // read new symbol into instruction or output
                if (readInstruction) {
                    instruction += fileSymbols[i];
                } else {
                    output.Append(fileSymbols[i]);
                }
            }

            return output.ToString();
        }

    }
}
