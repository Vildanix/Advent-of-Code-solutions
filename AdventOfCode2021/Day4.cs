using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2021

namespace AdventOfCode2021 {
    class Day4 {
        private const int BOARD_SIZE = 5;

        private class Board
        {
            private int number;
            private int size;
            private int[] cells; // cell values
            private int[] marks; // mark picked numbers. 0 means picked, 1 means still avalilable
            private bool isWinner;

            public Board(int[] cellValues, int number, int size)
            {
                cells = cellValues;
                marks = new int[size * size];
                for (int i = 0; i < marks.Length; i++)
                {
                    marks[i] = 1;
                }

                this.size = size;
                this.number = number;
                isWinner = false;
            }

            // has fully marked row or collumn
            public bool IsWinner()
            {
                int row, col;

                for (int i = 0; i < size; i++)
                {
                    row = 0;
                    col = 0;
                    for (int j = 0; j < size; j++)
                    {
                        if (marks[i * size + j] == 0) row++; else row = 0;
                        if (marks[j * size + i] == 0) col++; else col = 0;

                        if (row == size || col == size)
                        {
                            isWinner = true;
                            return true;
                        }
                    }
                }

                return false;
            }

            public bool IsNewWinner()
            {
                if (isWinner) return false;

                return IsWinner();
            }

            public void MarkNumber(int draw)
            {
                for (int i = 0; i< cells.Length; i++)
                {
                    if (cells[i] == draw)
                    {
                        marks[i] = 0;
                        break;
                    }
                }
            }

            public int GetValue(int winningNumber)
            {
                int boardValue = 0;
                for (int i = 0; i < cells.Length; i++)
                {
                    boardValue += cells[i] * marks[i];
                }

                return boardValue * winningNumber;
            }

            public int GetNumber()
            {
                return number;
            }
        }

        public void createSolution() {
            /*  
             *  You're already almost 1.5km (almost a mile) below the surface of the ocean, already so deep that you can't see any sunlight. 
             *  What you can see, however, is a giant squid that has attached itself to the outside of your submarine.
             *  Maybe it wants to play bingo?
             *  
             *  Bingo is played on a set of boards each consisting of a 5x5 grid of numbers.
             *  Numbers are chosen at random, and the chosen number is marked on all boards on which it appears.
             *  (Numbers may not appear on all boards.) If all numbers in any row or any column of a board are marked, that board wins. (Diagonals don't count.)
             *  
             *  The submarine has a bingo subsystem to help passengers (currently, you and the giant squid) pass the time. 
             *  It automatically generates a random order in which to draw numbers and a random set of boards (your puzzle input).
             *  
             *  The score of the winning board can now be calculated. Start by finding the sum of all unmarked numbers on that board.
             *  Then, multiply that sum by the number that was just called when the board won, to get the final score.
             *  
             *  To guarantee victory against the giant squid, figure out which board will win first. What will your final score be if you choose that board?
             */

            String testInput = File.ReadAllText("./Inputs/input_day4_test.txt");

            // load and prepare file containing input
            String inputFile = File.ReadAllText("./Inputs/input_day4.txt");

            Console.Write("Test first solution: ");
            var testSolution = SolveWinningBoard(testInput);
            Console.WriteLine($"Board number: {testSolution.Item1}, Winning value: {testSolution.Item2}");

            Console.Write("First solution: ");
            var firstSolution = SolveWinningBoard(inputFile);
            Console.WriteLine($"Board number: {firstSolution.Item1}, Winning value: {firstSolution.Item2}");

            /**
             * On the other hand, it might be wise to try a different strategy: let the giant squid win.
             * You aren't sure how many bingo boards a giant squid could play at once, so rather than waste time counting its arms, 
             * the safe thing to do is to figure out which board will win last and choose that one. That way, no matter which boards it picks, it will win for sure.
             * 
             * Figure out which board will win last. Once it wins, what would its final score be?
             */

            Console.Write("Test second solution: ");
            var testSecondSolution = SolveLastWinningBoard(testInput);
            Console.WriteLine($"Board number: {testSecondSolution.Item1}, Winning value: {testSecondSolution.Item2}");

            Console.Write("Second solution: ");
            var secondSolution = SolveLastWinningBoard(inputFile);
            Console.WriteLine($"Board number: {secondSolution.Item1}, Winning value: {secondSolution.Item2}");

            Console.ReadKey();

        }


        private Tuple<int, int> SolveWinningBoard(string input)
        {
            var inputLines = SeparateLines(input).ToList();

            var draws = inputLines.First().Split(',').Select(draw => Convert.ToInt32(draw)).ToList();

            var boards = ReadBoards(inputLines.Skip(1).ToList(), BOARD_SIZE);

            foreach (var draw in draws)
            {
                foreach (var board in boards)
                {
                    board.MarkNumber(draw);
                    if (board.IsWinner())
                    {
                        return new Tuple<int, int>(board.GetNumber(), board.GetValue(draw));
                    }
                }
            }

            return new Tuple<int, int>(0, 0);
        }

        private Tuple<int, int> SolveLastWinningBoard(string input)
        {
            var inputLines = SeparateLines(input).ToList();

            var draws = inputLines.First().Split(',').Select(draw => Convert.ToInt32(draw)).ToList();

            var boards = ReadBoards(inputLines.Skip(1).ToList(), BOARD_SIZE);
            Tuple<int, int> lastWinningBoard = new Tuple<int, int>(0, 0);
            foreach (var draw in draws)
            {
                foreach (var board in boards)
                {
                    board.MarkNumber(draw);
                    if (board.IsNewWinner())
                    {
                        lastWinningBoard = new Tuple<int, int>(board.GetNumber(), board.GetValue(draw));
                    }
                }
            }

            return lastWinningBoard;
        }

        private List<Board> ReadBoards(List<string> boardInputRows, int boardSize)
        {
            List<Board> boards = new List<Board>();

            int[] boardValues = new int[boardSize * boardSize];
            int boardRowReaded = 0;
            int boardReaded = 0;
            foreach (var line in boardInputRows)
            {
                if (line.Length < 1) continue;
                var cells = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(cell => Convert.ToInt32(cell)).ToArray();
                for (int i = 0; i < boardSize; i++)
                {
                    boardValues[i + boardRowReaded * boardSize] = cells[i];
                }
                boardRowReaded++;
                if (boardRowReaded == boardSize)
                {
                    boardRowReaded = 0;
                    boards.Add(new Board(boardValues, boardReaded++, boardSize));
                    boardValues = new int[boardSize * boardSize];
                }
            }

            return boards;
        }

        private IEnumerable<string> SeparateLines(string input)
        {
            return input.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
