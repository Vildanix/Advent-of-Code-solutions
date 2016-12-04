using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2016

namespace AdventOfCode2016 {
    class Day4 {

        public void createSolution() {
            // part 1
            /*
             * Each room consists of an encrypted name (lowercase letters separated by dashes) followed by a dash, a sector ID, and a checksum in square brackets.

             * A room is real (not a decoy) if the checksum is the five most common letters in the encrypted name, in order, with ties broken by alphabetization. For example:
             * 
             * aaaaa-bbb-z-y-x-123[abxyz] is a real room because the most common letters are a (5), b (3), and then a tie between x, y, and z, which are listed alphabetically.
             * a-b-c-d-e-f-g-h-987[abcde] is a real room because although the letters are all tied (1 of each), the first five are listed alphabetically.
             * not-a-real-room-404[oarel] is a real room.
             * totally-real-room-200[decoy] is not.
             *
             * Of the real rooms from the list above, the sum of their sector IDs is 1514.
             * What is the sum of the sector IDs of the real rooms?
             */

            // test string
            string testString = "not-a-real-room-404[oarel]";

            // load and prepare file containing input
            StreamReader inputFileStream = new StreamReader("../../Resources/input-day4.txt");
            String inputFile = inputFileStream.ReadToEnd();

            string[] roomNames = inputFile.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //string[] roomNames = testString.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string roomLetters, roomChecksum;
            int roomNumber, sectorSum = 0;
            foreach (string room in roomNames) {
                // get room name parts
                Regex regex = new Regex(@"([a-z-]+)(\d+)\[([a-z)]+)\]");
                Match match = regex.Match(room);
                if (match.Success) {
                    roomLetters = match.Groups[1].Value.Replace("-","");
                    int.TryParse(match.Groups[2].Value, out roomNumber);
                    roomChecksum = match.Groups[3].Value;

                    // count real rooms
                    if (isRealRoom(roomLetters, roomChecksum)) {
                        sectorSum += roomNumber;
                    }
                }
            }

            Console.WriteLine("Sum of the sector IDs: {0}", sectorSum);
            Console.ReadKey();
        }

        private bool isRealRoom(string roomLetters, string roomChecksum) {
            Dictionary<char, int> charCount = new Dictionary<char, int>();

            // count characters in room name
            foreach (char roomChar in roomLetters.ToCharArray()) {
                if (charCount.ContainsKey(roomChar)) {
                    charCount[roomChar]++;
                } else {
                    charCount.Add(roomChar, 1);
                }
            }

            // sort by occurrence and alphabet
            var sortedOccurrence = charCount.OrderByDescending(o => o.Value).ThenBy(o => o.Key);
            var charCountList = sortedOccurrence.ToList<KeyValuePair<char, int>>();

            // checksum check
            char[] checksumChars = roomChecksum.ToCharArray();
            for (int checksumIndex = 0; checksumIndex < checksumChars.Length; checksumIndex++) {
                if (charCountList[checksumIndex].Key != checksumChars[checksumIndex]) {
                    return false;
                }
            }

            return true;
        }

    }
}
