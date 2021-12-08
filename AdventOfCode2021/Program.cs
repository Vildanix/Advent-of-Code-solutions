using AdventOfCode2021.Interfaces;
using AdventOfCode2021.Tasks;
using System;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            int workingDay = 8;
            IDailySoultion daySolution;
            switch (workingDay)
            {
                default:
                case 1:
                    daySolution = new Day1("input_day1");
                    break;
                case 2:
                    daySolution = new Day2("input_day2");
                    break;
                case 3:
                    daySolution = new Day3("input_day3");
                    break;
                case 4:
                    daySolution = new Day4("input_day4");
                    break;
                case 5:
                    daySolution = new Day5("input_day5");
                    break;
                case 6:
                    daySolution = new Day6("input_day6");
                    break;
                case 7:
                    daySolution = new Day7("input_day7");
                    break;
                case 8:
                    daySolution = new Day8("input_day8");
                    break;
            }
            

            daySolution.CreateSolution();
        }
    }
}
