using AdventOfCode2022.Interfaces;
using AdventOfCode2022.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2022
{
    class Program
    {
        // Solutions for https://adventofcode.com/2022
        static void Main(string[] args)
        {
            var existingSolutions = GetExistingDailySoulutions();
            existingSolutions.Sort();

            var newestSolution = existingSolutions.Last();

            try
            {
                IDailySoultion daySolution = ConstructDailySolution(newestSolution);
                daySolution.CreateSolution();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static IDailySoultion ConstructDailySolution(string dailySolution)
        {
            var typeName = $"{typeof(DailySolution).Namespace}.{dailySolution}";

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
            return constructor.Invoke(new object[] { (string)$"input_{dailySolution.ToLower()}" }) as IDailySoultion;
        }

        private static List<string> GetExistingDailySoulutions()
        {
            return typeof(IDailySoultion).Assembly.GetTypes()
                                         .Where(t => t.IsSubclassOf(typeof(DailySolution)) && t.GetInterface(nameof(IDailySoultion)) != null)
                                         .Select(t => t.Name)
                                         .ToList();
        }
    }
}
