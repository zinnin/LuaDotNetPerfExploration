using PerfTester.LibraryImplementations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static PerfTester.PerfCollector;

namespace PerfTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int testCount = 500000;            
            new DotNetTests(testCount);
            new MoonSharpTests(testCount);
            new NLuaTests(testCount);
            new NeoLuaTests(testCount);

            string csvData = "TestCase, ";

            foreach (TestCaseGroup luaSolution in (Enum.GetValues(typeof(TestCaseGroup))))
            {
                csvData += $"{luaSolution}, ";
            }

            foreach (KeyValuePair<TestCaseType, TestCase> testCase in TestCases)
            {
                csvData += $"\n{testCase.Key}, ";

                foreach (KeyValuePair<TestCaseGroup, Stopwatch> testCaseData in testCase.Value.PerfData)
                {
                    csvData += $"{testCaseData.Value.ElapsedMilliseconds}, ";
                }
            }

            File.WriteAllText("Results.csv", csvData);
        }
    }
}
