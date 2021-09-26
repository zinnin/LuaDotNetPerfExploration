using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PerfTester
{
    static class PerfCollector
    {
        public enum TestCaseGroup
        {
            DotNetPure,
            MoonSharpPure,
            NLuaPure,
            NeoLuaPure,

            MoonSharpCalled,
            NLuaCalled,
            NeoLuaCalled,

            MoonSharpCalling,
            NLuaCalling,
            NeoLuaCalling,
            NeoLuaCallingPreCompiled,
            NeoLuaCallingPreCompiledNoCompileTime,
        }

        public enum TestCaseType
        {
            AddInts,
            AddFloats,
            SubtractInts,
            SubtractFloats,
            MultiplyInts,
            MultiplyFloats,
            DivideInts,
            DivideFloats,
            IntsAreEqual,
            FloatsAreEqual,
            IntsAreNotEqual,
            FloatsAreNotEqual,
            IntsGreaterThan,
            FloatsGreaterThan,
            IntsGreaterOrEqualTo,
            FloatsGreaterOrEqualTo,
            IntsLesserThan,
            FloatsLesserThan,
            IntsLesserOrEqualTo,
            FloatsLesserOrEqualTo,
            Remainder,
            Exponent,
            StringFlip,
            CombineStrings,
            FormatStrings,
            CombineManyStrings,
            FormatManyStrings,
            AddOrUpdateValueInDictionary,
            GetValueFromDictionary,
            AddStringToList,
            ItterateListAndGetCount,
        }

        public class TestCase
        {
            public Dictionary<TestCaseGroup, Stopwatch> PerfData { get; set; } = new Dictionary<TestCaseGroup, Stopwatch>();

            public TestCase()
            {
                foreach(TestCaseGroup luaSolution in (Enum.GetValues(typeof(TestCaseGroup))))
                {
                    PerfData.Add(luaSolution, new Stopwatch());
                }
            }

            public void StartTimer(TestCaseGroup luaSolution)
            {
                PerfData[luaSolution].Start();
            }

            public void EndTimer(TestCaseGroup luaSolution)
            {
                PerfData[luaSolution].Stop();
            }
        }

        public static Dictionary<TestCaseType, TestCase> TestCases { get; set; } = new Dictionary<TestCaseType, TestCase>();

        public static TestCase GetTestCase(TestCaseType name)
        {
            if (!TestCases.ContainsKey(name))
            {
                TestCases.Add(name, new TestCase());
            }

            return TestCases[name];
        }
    }
}
