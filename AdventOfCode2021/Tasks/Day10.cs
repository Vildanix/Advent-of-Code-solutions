using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// Advent of Code
// Autor: Stanislav Tvrzník
// Year: 2021

namespace AdventOfCode2021.Tasks
{
    class Day10 : DailySolution {

        public Day10(string baseInputFileName) : base(baseInputFileName) { }

        protected readonly Dictionary<char, int> SCORE_SYNTAX_TABLE = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        protected readonly Dictionary<char, int> SCORE_INCOMPLETE_TABLE = new Dictionary<char, int>() { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };
        protected readonly List<(char, char)> CHUNK_PAIRS = new List<(char, char)>() { ('(', ')'), ('[', ']'), ('{', '}'), ('<', '>') };

        protected override void PrintFirstSolution(string inputFile)
        {
            /*
             * You ask the submarine to determine the best route out of the deep-sea cave, but it only replies:
             * 
             * Syntax error in navigation subsystem on line: all of them
             * 
             * All of them?! The damage is worse than you thought. You bring up a copy of the navigation subsystem (your puzzle input).
             * The navigation subsystem syntax is made of several lines containing chunks. There are one or more chunks on each line, 
             * and chunks contain zero or more other chunks. Adjacent chunks are not separated by any delimiter; if one chunk stops, 
             * the next chunk (if any) can immediately start. Every chunk must open and close with one of four legal pairs of matching characters:
             *      If a chunk opens with (, it must close with ).
             *      If a chunk opens with [, it must close with ].
             *      If a chunk opens with {, it must close with }.
             *      If a chunk opens with <, it must close with >.
             *      
             * So, () is a legal chunk that contains no other chunks, as is []. More complex but valid chunks include 
             * ([]), {()()()}, <([{}])>, [<>({}){}[([])<>]], and even (((((((((()))))))))).
             * Some lines are incomplete, but others are corrupted. Find and discard the corrupted lines first.
             * A corrupted line is one where a chunk closes with the wrong character - that is, where the characters 
             * it opens and closes with do not form one of the four legal pairs listed above.
             * 
             * Examples of corrupted chunks include (], {()()()>, (((()))}, and <([]){()}[{}]). Such a chunk can appear anywhere within a line,
             * and its presence causes the whole line to be considered corrupted.
             * 
             * Some of the lines aren't corrupted, just incomplete; you can ignore these lines for now. Stop at the first incorrect closing character on each corrupted line.
             * 
             * Did you know that syntax checkers actually have contests to see who can get the high score for syntax errors in a file?
             * It's true! To calculate the syntax error score for a line, take the first illegal character on the line and look it up in the following table:
             *      ): 3 points.
             *      ]: 57 points.
             *      }: 1197 points.
             *      >: 25137 points.
             * 
             * Find the first illegal character in each corrupted line of the navigation subsystem. What is the total syntax error score for those errors?
             */

            var solution = SolveSyntaxErrorScore(inputFile);
            Console.WriteLine($"Total syntax error score is {solution}");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /*
             * Now, discard the corrupted lines. The remaining lines are incomplete.
             * 
             * Incomplete lines don't have any incorrect characters - instead, they're missing some closing characters at the end of the line.
             * To repair the navigation subsystem, you just need to figure out the sequence of closing characters that complete all open chunks in the line.
             * 
             * You can only use closing characters (), ], }, or >), and you must add them in the correct order so that only legal pairs are formed and all chunks end up closed.
             * 
             * Did you know that autocomplete tools also have contests? It's true! The score is determined by considering the completion string character-by-character. 
             * Start with a total score of 0. Then, for each character, multiply the total score by 5 and then increase the total score by the point value given
             * for the character in the following table:
             *      ): 1 point.
             *      ]: 2 points.
             *      }: 3 points.
             *      >: 4 points.
             * 
             * Autocomplete tools are an odd bunch: the winner is found by sorting all of the scores and then taking the middle score.
             * (There will always be an odd number of scores to consider.)
             * Find the completion string for each incomplete line, score the completion strings, and sort the scores. What is the middle score?
             */
            // 27994957 malé
            var solution = SolveIncompleteScore(inputFile);
            Console.WriteLine($"Middle score for invomplete result is {solution}");
        }

        private int SolveSyntaxErrorScore(string fileInput)
        {
            return SeparateLines(fileInput).Select(command => CheckSyntaxError(command)).Select(result => result.Item1 ? SCORE_SYNTAX_TABLE[result.Item2] : 0).Sum();
        }

        private long SolveIncompleteScore(string fileInput)
        {
            var scores = SeparateLines(fileInput).Where(command => !CheckSyntaxError(command).Item1).Select(command => GetIncompleteScore(command)).ToList();
            scores.Sort();
            return scores[scores.Count / 2];
        }

        private (bool, char) CheckSyntaxError(string command)
        {
            var chunks = new Stack<char>();
            var chars = command.ToCharArray();

            foreach(var chunkChar in chars)
            {
                var chunk = CHUNK_PAIRS.Where(pair => pair.Item1 == chunkChar || pair.Item2 == chunkChar).First();
                
                if (chunkChar == chunk.Item1) {
                    chunks.Push(chunk.Item2);
                    continue;
                }

                var expectedSymbol = chunks.Pop();
                if (chunkChar != expectedSymbol)
                {
                    return (true, chunkChar);
                }
            }

            return (false, '0');
        }

        private long GetIncompleteScore(string command)
        {
            var chunks = new Stack<char>();
            var chars = command.ToCharArray();

            foreach(var chunkChar in chars)
            {
                var chunk = CHUNK_PAIRS.Where(pair => pair.Item1 == chunkChar || pair.Item2 == chunkChar).First();
                
                if (chunkChar == chunk.Item1) {
                    chunks.Push(chunk.Item2);
                } else
                {
                    chunks.Pop();
                }
            }

            long acc = 0;
            foreach (var chunk in chunks)
            {
                acc = acc * 5 + SCORE_INCOMPLETE_TABLE[chunk];
            }
            return acc;
        }
        
    }
}
