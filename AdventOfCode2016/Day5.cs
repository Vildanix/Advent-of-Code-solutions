using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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
            int passwordLength = 8;

            MD5 hashMD5 = MD5.Create();
            byte[] hashInputBuffer, hashOutput;
            uint passwordSize = 0, index = 0;

            // find all characters for door password
            Console.WriteLine("Creating first door key");
            while (passwordSize < passwordLength) {
                hashInputBuffer = System.Text.Encoding.ASCII.GetBytes(puzzleInput + index);

                // create hash and test if first 5 hexadecimal values are zeros (using mask for last hexadecimal value)
                hashOutput = hashMD5.ComputeHash(hashInputBuffer);
                if (hashOutput[0] == 0x00 && hashOutput[1] == 0x00 && (hashOutput[2] & 0xf0) == 0x00) {
                    Console.Write("{0:X}", hashOutput[2] & 0x0f);
                    passwordSize++;
                }

                index++;
            }
            Console.WriteLine("\nPress enter to enter first door code");
            Console.ReadKey();

            // part 2
            byte[] password = new byte[passwordLength];
            byte[] foundPositions = new byte[passwordLength];
            byte[] cinematic = new byte[passwordLength];
            passwordSize = 0;
            index = 0;

            Console.WriteLine("\nSecond door");
            Random random = new Random();           // only for cinematic effect
            while (passwordSize < passwordLength) {
                hashInputBuffer = System.Text.Encoding.ASCII.GetBytes(puzzleInput + index);

                // create hash and test if first 5 hexadecimal values are zeros (using mask for last hexadecimal value)
                hashOutput = hashMD5.ComputeHash(hashInputBuffer);
                if (hashOutput[0] == 0x00 && hashOutput[1] == 0x00 && (hashOutput[2] & 0xf0) == 0x00) {
                    // check if 6 hexa value is valid password position
                    byte positionIndex = (byte)(hashOutput[2] & 0x0f);
                    if (positionIndex < passwordLength) {

                        // check if position is already filled and skip if value exists
                        if (foundPositions[positionIndex] == 0xff) {
                            index++;
                            continue;
                        }

                        // save password symbol
                        password[positionIndex] = (byte)(hashOutput[3] & 0xf0);
                        foundPositions[positionIndex] = 0xff;

                        // recalculate found positions
                        passwordSize = 0;
                        for (uint byteIndex = 0; byteIndex < foundPositions.Length; byteIndex++) {
                            if (foundPositions[byteIndex] == 0xFF) {
                                passwordSize++;
                            }
                        }
                        
                    }
                }

                // cinematic effect refresh once every 20000 hashes
                if (index % 20000 == 0) {
                    random.NextBytes(cinematic);
                    for (int i = 0; i < passwordLength; i++) {
                        cinematic[i] = (byte)(password[i] | (cinematic[i] & ~foundPositions[i]));
                    }

                    Console.Write("\r{0}", getPasswordAsString(cinematic));
                }

                index++;
            }
            Console.WriteLine("\rPassword: {0}", getPasswordAsString(password));
            Console.ReadKey();
        }

        private string getPasswordAsString(byte[] bytes) {
            StringBuilder byteString = new StringBuilder(bytes.Length);
            for(uint byteIndex = 0; byteIndex < bytes.Length; byteIndex++) {
                byteString.AppendFormat("{0:X}", (bytes[byteIndex] & 0xf0) >> 4);
            }
                
            return byteString.ToString();
        }

    }
}
