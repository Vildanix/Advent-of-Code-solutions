using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2016

namespace AdventOfCode2016 {
    class Day2 {
        private int keypadX;
        private int keypadY;

        public void createSolution() {
            // part 1
            // 
            // keypad
            // 1 2 3
            // 4 5 6
            // 7 8 9

            // instructions:

            // You start at "5" and move up(to "2"), left(to "1"), and left (you can't, and stay on "1"), so the first button is 1.
            // Starting from the previous button("1"), you move right twice(to "3") and then down three times(stopping at "9" after two moves and ignoring the third), ending up with 9.
            // Continuing from "9", you move left, up, right, down, and left, ending with 8.
            // Finally, you move up four times(stopping at "2"), then down once, ending with 5.

            // test input
            /*
            string[] input = {"ULL",
                              "RRDDD",
                              "LURDL",
                              "UUUUD"};
            */
            string[] input = {"RDLULDLDDRLLLRLRULDRLDDRRRRURLRLDLULDLDLDRULDDLLDRDRUDLLDDRDULLLULLDULRRLDURULDRUULLLUUDURURRDDLDLDRRDDLRURLLDRRRDULDRULURURURURLLRRLUDULDRULLDURRRLLDURDRRUUURDRLLDRURULRUDULRRRRRDLRLLDRRRDLDUUDDDUDLDRUURRLLUDUDDRRLRRDRUUDUUULDUUDLRDLDLLDLLLLRRURDLDUURRLLDLDLLRLLRULDDRLDLUDLDDLRDRRDLULRLLLRUDDURLDLLULRDUUDRRLDUDUDLUURDURRDDLLDRRRLUDULDULDDLLULDDDRRLLDURURURUUURRURRUUDUUURULDLRULRURDLDRDDULDDULLURDDUDDRDRRULRUURRDDRLLUURDRDDRUDLUUDURRRLLRR",
                            "RDRRLURDDDDLDUDLDRURRLDLLLDDLURLLRULLULUUURLDURURULDLURRLRULDDUULULLLRLLRDRRUUDLUUDDUDDDRDURLUDDRULRULDDDLULRDDURRUURLRRLRULLURRDURRRURLDULULURULRRLRLUURRRUDDLURRDDUUDRDLLDRLRURUDLDLLLLDLRURDLLRDDUDDLDLDRRDLRDRDLRRRRUDUUDDRDLULUDLUURLDUDRRRRRLUUUDRRDLULLRRLRLDDDLLDLLRDDUUUUDDULUDDDUULDDUUDURRDLURLLRUUUUDUDRLDDDURDRLDRLRDRULRRDDDRDRRRLRDULUUULDLDDDUURRURLDLDLLDLUDDLDLRUDRLRLDURUDDURLDRDDLLDDLDRURRULLURULUUUUDLRLUUUDLDRUDURLRULLRLLUUULURLLLDULLUDLLRULRRLURRRRLRDRRLLULLLDURDLLDLUDLDUDURLURDLUURRRLRLLDRLDLDRLRUUUDRLRUDUUUR",
                            "LLLLULRDUUDUUDRDUUURDLLRRLUDDDRLDUUDDURLDUDULDRRRDDLLLRDDUDDLLLRRLURDULRUUDDRRDLRLRUUULDDULDUUUDDLLDDDDDURLDRLDDDDRRDURRDRRRUUDUUDRLRRRUURUDURLRLDURDDDUDDUDDDUUDRUDULDDRDLULRURDUUDLRRDDRRDLRDLRDLULRLLRLRLDLRULDDDDRLDUURLUUDLLRRLLLUUULURUUDULRRRULURUURLDLLRURUUDUDLLUDLDRLLRRUUDDRLUDUDRDDRRDDDURDRUDLLDLUUDRURDLLULLLLUDLRRRUULLRRDDUDDDUDDRDRRULURRUUDLUDLDRLLLLDLUULLULLDDUDLULRDRLDRDLUDUDRRRRLRDLLLDURLULUDDRURRDRUDLLDRURRUUDDDRDUUULDURRULDLLDLDLRDUDURRRRDLDRRLUDURLUDRRLUDDLLDUULLDURRLRDRLURURLUUURRLUDRRLLULUULUDRUDRDLUL",
                            "LRUULRRUDUDDLRRDURRUURDURURLULRDUUDUDLDRRULURUDURURDRLDDLRUURLLRDLURRULRRRUDULRRULDLUULDULLULLDUDLLUUULDLRDRRLUURURLLUUUDDLLURDUDURULRDLDUULDDRULLUUUURDDRUURDDDRUUUDRUULDLLULDLURLRRLRULRLDLDURLRLDLRRRUURLUUDULLLRRURRRLRULLRLUUDULDULRDDRDRRURDDRRLULRDURDDDDDLLRRDLLUUURUULUDLLDDULDUDUUDDRURDDURDDRLURUDRDRRULLLURLUULRLUDUDDUUULDRRRRDLRLDLLDRRDUDUUURLRURDDDRURRUDRUURUUDLRDDDLUDLRUURULRRLDDULRULDRLRLLDRLURRUUDRRRLRDDRLDDLLURLLUDL",
                            "ULURLRDLRUDLLDUDDRUUULULUDDDDDRRDRULUDRRUDLRRRLUDLRUULRDDRRLRUDLUDULRULLUURLLRLLLLDRDUURDUUULLRULUUUDRDRDRUULURDULDLRRULUURURDULULDRRURDLRUDLULULULUDLLUURULDLLLRDUDDRRLULUDDRLLLRURDDLDLRLLLRDLDRRUUULRLRDDDDRUDRUULDDRRULLDRRLDDRRUDRLLDUDRRUDDRDLRUDDRDDDRLLRDUULRDRLDUDRLDDLLDDDUUDDRULLDLLDRDRRUDDUUURLLUURDLULUDRUUUDURURLRRDULLDRDDRLRDULRDRURRUDLDDRRRLUDRLRRRRLLDDLLRLDUDUDDRRRUULDRURDLLDLUULDLDLDUUDDULUDUDRRDRLDRDURDUULDURDRRDRRLLRLDLU"
                            };
            

            // start keypad position
            keypadX = 1;
            keypadY = 1;

            StringBuilder bathroomNumber = new StringBuilder();

            // build bathroom number
            foreach (string inputNumber in input) {

                // move on keypad folowing instruction set from found paper
                foreach (char instruction in inputNumber.ToCharArray()) {
                    switch (instruction) {
                        case 'U':
                            moveUp();
                            break;
                        case 'D':
                            moveDown();
                            break;
                        case 'L':
                            moveLeft();
                            break;
                        case 'R':
                            moveRight();
                            break;
                    }
                }

                // get current keypad number after finishing line of instructions
                bathroomNumber.Append(getKeypadNumber());
            }

            // print bathroom number into console
            Console.WriteLine(bathroomNumber.ToString());
            Console.ReadKey();


            // part 2
            // start star keypad position
            keypadX = 0;
            keypadY = 2;

            bathroomNumber.Clear();

            // build bathroom number
            foreach (string inputNumber in input) {

                // move on star keypad folowing instruction set from found paper
                foreach (char instruction in inputNumber.ToCharArray()) {
                    switch (instruction) {
                        case 'U':
                            moveStarUp();
                            break;
                        case 'D':
                            moveStarDown();
                            break;
                        case 'L':
                            moveStarLeft();
                            break;
                        case 'R':
                            moveStarRight();
                            break;
                    }
                }

                // get current star keypad number after finishing line of instructions
                bathroomNumber.Append(getStarKeypadNumber());
            }

            // print new bathroom number into console
            Console.WriteLine(bathroomNumber.ToString());
            Console.ReadKey();
        }

        private void moveUp() {
            if (keypadY > 0) {
                keypadY--;
            }
        }

        private void moveDown() {
            if (keypadY < 2) {
                keypadY++;
            }
        }

        private void moveLeft() {
            if (keypadX > 0) {
                keypadX--;
            }
        }

        private void moveRight() {
            if (keypadX < 2) {
                keypadX++;
            }
        }

        private int getKeypadNumber() {
            return (1 + keypadX) + keypadY * 3;
        }

        // for second solution
        private void moveStarUp() {
            if (keypadY - 1 >= Math.Abs(keypadX - 2)) {
                keypadY--;
            }
        }

        private void moveStarDown() {
            if (keypadY + 1 <= 4 - Math.Abs(keypadX - 2)) {
                keypadY++;
            }
        }

        private void moveStarLeft() {
            if (keypadX - 1 >= Math.Abs(keypadY - 2)) {
                keypadX--;
            }
        }

        private void moveStarRight() {
            if (keypadX + 1 <= 4 - Math.Abs(keypadY - 2)) {
                keypadX++;
            }
        }

        private string getStarKeypadNumber() {
            //    1
            //  2 3 4
            //5 6 7 8 9
            //  A B C
            //    D
            string[,] keypad = new string[5,5];
            keypad[2, 0] = "1";

            keypad[1, 1] = "2";
            keypad[2, 1] = "3";
            keypad[3, 1] = "4";

            keypad[0, 2] = "5";
            keypad[1, 2] = "6";
            keypad[2, 2] = "7";
            keypad[3, 2] = "8";
            keypad[4, 2] = "9";

            keypad[1, 3] = "A";
            keypad[2, 3] = "B";
            keypad[3, 3] = "C";

            keypad[2, 4] = "D";
            return keypad[keypadX, keypadY];
        }
    }
}
