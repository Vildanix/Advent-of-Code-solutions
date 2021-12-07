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
    class Day3: DailySolution {
        public Day3(string baseInputFileName) : base(baseInputFileName) { }

        private struct PowerConsumptionDto
        {
            public string GamaRate { get; set; }
            public string EpsilonRate { get; set; }
            public int PowerConsumption { get; set; }
        }

        private struct LifeSupportDto
        {
            public string Oxygen { get; set; }
            public string CO2 { get; set; }
            public int LifeSupport { get; set; }
        }

        protected override void PrintFirstSolution(string inputFile)
        {
            /*  
             *  The submarine has been making some odd creaking noises, so you ask it to produce a diagnostic report just in case.
             *  The diagnostic report (your puzzle input) consists of a list of binary numbers which, when decoded properly, 
             *  can tell you many useful things about the conditions of the submarine. The first parameter to check is the power consumption.
             *  
             *  You need to use the binary numbers in the diagnostic report to generate two new binary numbers (called the gamma rate and the epsilon rate). 
             *  The power consumption can then be found by multiplying the gamma rate by the epsilon rate.
             *  
             *  Each bit in the gamma rate can be determined by finding the most common bit in the corresponding position of all numbers in the diagnostic report.
             *  
             *  The epsilon rate is calculated in a similar way; rather than use the most common bit, the least common bit from each position is used.
             *  
             *  Answer is decimal number
             */

            var solution = SolvePowerConsumption(inputFile);
            Console.WriteLine($"Gama rate: {solution.GamaRate}, Epsilon rate: {solution.EpsilonRate}, Power consumption: {solution.PowerConsumption}");
        }

        protected override void PrintSecondSolution(string inputFile)
        {
            /**
             * Next, you should verify the life support rating, which can be determined by multiplying the oxygen generator rating by the CO2 scrubber rating.
             * Both the oxygen generator rating and the CO2 scrubber rating are values that can be found in your diagnostic report - finding them is the tricky part.
             * Both values are located using a similar process that involves filtering out values until only one remains.
             * Before searching for either rating value, start with the full list of binary numbers from your diagnostic
             * report and consider just the first bit of those numbers. Then:
             *  - Keep only numbers selected by the bit criteria for the type of rating value for which you are searching. Discard numbers which do not match the bit criteria.
             *  - If you only have one number left, stop; this is the rating value for which you are searching.
             *  - Otherwise, repeat the process, considering the next bit to the right.
             *  
             *  The bit criteria depends on which type of rating value you want to find:
             *   - To find oxygen generator rating, determine the most common value (0 or 1) in the current bit position,
             *     and keep only numbers with that bit in that position. If 0 and 1 are equally common, 
             *     keep values with a 1 in the position being considered.
             *   - To find CO2 scrubber rating, determine the least common value (0 or 1) in the current bit position, 
             *     and keep only numbers with that bit in that position. If 0 and 1 are equally common,
             *     keep values with a 0 in the position being considered.
             */

            var solution = SolveLifeSupport(inputFile);
            Console.WriteLine($"Oxygen gen.: {solution.Oxygen}, CO2 Scrubber: {solution.CO2}, Life support: {solution.LifeSupport}");
        }

        private char GetMostCommonBit(List<int> readings, int position)
        {
            int mask = 1 << position;
            int counter = readings.Where(reading => (reading & mask) > 0).Count();

            return counter >= (double)readings.Count / 2 ? '1' : '0';
        }

        private PowerConsumptionDto SolvePowerConsumption(string input)
        {
            var binaryInputs = SeparateLines(input).Select(line => Convert.ToInt32(line, 2)).ToList();
            int binaryLength = MaxBinaryLength(binaryInputs);

            StringBuilder gamaRateSB = new StringBuilder(binaryLength);
            StringBuilder epsilonRateSB = new StringBuilder(binaryLength);

            for (int i = binaryLength - 1; i >= 0; i--)
            {
                if (GetMostCommonBit(binaryInputs, i) == '1')
                {
                    gamaRateSB.Append('1');
                    epsilonRateSB.Append('0');
                } else
                {
                    gamaRateSB.Append('0');
                    epsilonRateSB.Append('1');
                }
            }

            var consumption = new PowerConsumptionDto() {
                EpsilonRate = epsilonRateSB.ToString(),
                GamaRate = gamaRateSB.ToString(),
                PowerConsumption = 0
            };

            consumption.PowerConsumption = Convert.ToInt32(consumption.GamaRate, 2) * Convert.ToInt32(consumption.EpsilonRate, 2);

            return consumption;
        }

        private LifeSupportDto SolveLifeSupport(string input)
        {
            var oxygenGeneratorInputs = SeparateLines(input).Select(line => Convert.ToInt32(line, 2)).ToList();
            var co2Scrubber = new List<int>(oxygenGeneratorInputs); 
            int binaryLength = MaxBinaryLength(oxygenGeneratorInputs);

            // oxygen generator
            for (int i = binaryLength - 1; i >= 0; i--)
            {
                int mask = 1 << i;
                if (GetMostCommonBit(oxygenGeneratorInputs, i) == '1')
                {
                    oxygenGeneratorInputs = oxygenGeneratorInputs.Where(reading => (reading & mask) > 0).ToList();
                }
                else
                {
                    oxygenGeneratorInputs = oxygenGeneratorInputs.Where(reading => (reading & mask) == 0).ToList();
                }

                if (oxygenGeneratorInputs.Count == 1) break;
            }

            // CO2 Scrubber generator
            for (int i = binaryLength - 1; i >= 0; i--)
            {
                int mask = 1 << i;
                if (GetMostCommonBit(co2Scrubber, i) == '1')
                {
                    co2Scrubber = co2Scrubber.Where(reading => (reading & mask) == 0).ToList();
                }
                else
                {
                    co2Scrubber = co2Scrubber.Where(reading => (reading & mask) > 0).ToList();
                }

                if (co2Scrubber.Count == 1) break;
            }

            return new LifeSupportDto()
            {
                Oxygen = Convert.ToString(oxygenGeneratorInputs.First(), 2),
                CO2 = Convert.ToString(co2Scrubber.First(), 2),
                LifeSupport = oxygenGeneratorInputs.First() * co2Scrubber.First()
            };
        }

        private int MaxBinaryLength(IEnumerable<int> inputs)
        {
            return inputs.Select(binary => (int)Math.Log2(binary) + 1).Max();
        }
    }
}
