using AdventOfCode2021.Interfaces;
using AdventOfCode2021.Tasks;
using System;
using System.Reflection;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            int workingDay = 10;

            IDailySoultion daySolution = ConstructDailySolution(workingDay);
            daySolution.CreateSolution();
        }

        private static IDailySoultion ConstructDailySolution(int solutionNumber)
        {
            var taskNameSpace = typeof(DailySolution).Namespace;

            Type type = Type.GetType($"{taskNameSpace}.Day{solutionNumber}");
            if (type == null)
            {
                throw new InvalidOperationException("Type " + type.Name + " does not exists");
            }

            ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(string) });
            if (constructor == null)
            {
                throw new InvalidOperationException("Type " + type.Name + " missing string constructor");
            }
            return constructor.Invoke(new object[] { (string)$"input_day{solutionNumber}" }) as IDailySoultion;
        }
    }
}
