using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2016

namespace AdventOfCode2016 {
    class Day7 {

        public void createSolution() {
            /*
             * An IP supports TLS if it has an Autonomous Bridge Bypass Annotation, or ABBA.
             * An ABBA is any four-character sequence which consists of a pair of two different characters
             * followed by the reverse of that pair, such as xyyx or abba. However, the IP also must not have an ABBA
             * within any hypernet sequences, which are contained by square brackets.
             * 
             * TLS (transport-layer snooping)
             * SSL (super-secret listening)
             */

            // test string
            //string inputAddr = "abba[mnop]qrst\nabcd[bddb]xyyx\naaaa[qwer]tyui\nioxxoj[asdfgh]zxcvbn";

            // load and prepare file containing input
            string inputAddr = File.ReadAllText("../../Resources/input-day7.txt");
            string[] ipv7Addr = inputAddr.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int tlsCount = 0;
            int sslCount = 0;
            foreach(string addr in ipv7Addr) {
                // verify address TLS and SSL
                tlsCount += isAddressTLS(addr) ? 1 : 0; // part 1
                sslCount += isAddressSSL(addr) ? 1 : 0; // part 2
            }

            Console.WriteLine("TLS supporting IP Count: {0}", tlsCount);
            Console.WriteLine("SSL supporting IP Count: {0}", sslCount);

            Console.ReadKey();
        }

        private bool isAddressTLS(string address) {
            bool isBracket = false;
            bool symetryFound = false;
            char[] addrChars = address.ToCharArray();
            for (int i = 3; i < addrChars.Length; i++) {
                // switch allowed symetry
                if (addrChars[i] == '[') {
                    isBracket = true;
                    i += 3;
                    continue;
                }

                if (addrChars[i] == ']') {
                    isBracket = false;
                    i += 3;
                    continue;
                }

                // check symetry of last four characters
                if (addrChars[i-3] == addrChars[i] && addrChars[i-2] == addrChars[i-1] && addrChars[i] != addrChars[i - 1]) {
                    // symetry in bracket is not allowed
                    if (isBracket) {
                        return false;
                    }

                    // at least one symetry found
                    symetryFound = true;
                }
            }

            return symetryFound;
        }

        private bool isAddressSSL(string address) {
            bool isBracket = false;
            char[] addrChars = address.ToCharArray();
            for (int i = 2; i < addrChars.Length; i++) {
                // switch allowed symetry
                if (addrChars[i] == '[') {
                    isBracket = true;
                    i += 2;
                    continue;
                }

                if (addrChars[i] == ']') {
                    isBracket = false;
                    i += 2;
                    continue;
                }

                // find sequence ABA outside brackets and then sequence BAB inside bracket
                if (!isBracket && addrChars[i - 2] == addrChars[i] && addrChars[i] != addrChars[i - 1]) {
                    Regex regex = new Regex(@"\[[a-z]*" + addrChars[i - 1] + addrChars[i] + addrChars[i - 1] + "[a-z]*\\]");
                    Match match = regex.Match(address);
                    if (match.Success) {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
