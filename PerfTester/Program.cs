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
            int testCount = 1000000;
            new NeoLuaTests(testCount);
            new DotNetTests(testCount);
            new MoonSharpTests(testCount);
            new NLuaTests(testCount);

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

    /* Test Cases
     * Compare All Above Test Cases In the following way: 
     * What is the speed to of basic operations in just C#
     * What is the speed to of basic operations in c# using properally multi-threaded code
     * What is the speed to of basic operations where the operations are written using async methods 
     * What is the speed to of basic operations in just each lua implementation
     * What is the speed to of basic operations where the operations are written in c# called from lua
     * What is the speed to of basic operations where the operations are written in lua, and called from c# * 
     * 
     * What is the speed of convering a Lua Object into a C# Object
     * What is the speed of contering a C# Object into a Lua Object
     * What is the speed of basic operations on properties of objects that are delivered to Lua from C#
     * What is the speed of basic operations on properties of objects (keys in the array) of and object in C# that was delivered from Lua
     * 
     * Basic Operations:
     * All basic int and float operations: + - * / % ^
     * Compare operations on int, float, string == != > < <= >= 
     * Flip a string between two values / Add one character to a string x times / add 2 strings together, string format, add 12 strings together
     * Find an object in a dictionary by key, Find an object in a dictionary by key, increase the key it is looking for by 1 each loop
     * Iterate through a list doing the first two actions to each object
    */
}
