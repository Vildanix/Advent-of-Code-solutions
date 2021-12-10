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

            try
            {
                IDailySoultion daySolution = ConstructDailySolution(workingDay);
                daySolution.CreateSolution();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        private static IDailySoultion ConstructDailySolution(int solutionNumber)
        {
            var typeName = $"{typeof(DailySolution).Namespace}.Day{solutionNumber}";

            Type type = Type.GetType(typeName);
            if (type == null)
            {
                throw new InvalidOperationException($"Type {typeName} does not exists");
            }

            ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(string) });
            if (constructor == null)
            {
                throw new InvalidOperationException($"Type {typeName} missing string constructor");
            }
            return constructor.Invoke(new object[] { (string)$"input_day{solutionNumber}" }) as IDailySoultion;
        }
    }
}
