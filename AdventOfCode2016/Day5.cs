using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2016

namespace AdventOfCode2016 {
    class Day5 {

        public void createSolution() {
            // part 1
            /*
             * The eight-character password for the door is generated one character at a time by finding the MD5 hash
             * of some Door ID (your puzzle input) and an increasing integer index (starting with 0).
             *
             * A hash indicates the next character in the password if its hexadecimal representation starts with five zeroes.
             * If it does, the sixth character in the hash is the next character of the password.
             */


            //string puzzleInput = "abc";       // test string
            string puzzleInput = "abbhdwsy";

            MD5 hashMD5 = MD5.Create();
            byte[] hashInputBuffer, hashOutput;
            uint passwordSize = 0, index = 0;

            // find all characters for door password
            Console.WriteLine("Creating door key");
            while (passwordSize < 8) {
                hashInputBuffer = System.Text.Encoding.ASCII.GetBytes(puzzleInput + index);

                // create hash and test if first 5 hexadecimal values are zeros (using mask for last hexadecimal value)
                hashOutput = hashMD5.ComputeHash(hashInputBuffer);
                if (hashOutput[0] == 0x00 && hashOutput[1] == 0x00 && (hashOutput[2] & 0xf0) == 0x00) {
                    Console.Write("{0:X}", hashOutput[2] & 0x0f);
                    passwordSize++;
                }

                index++;
            }
            Console.ReadKey();
        }

    }
}
