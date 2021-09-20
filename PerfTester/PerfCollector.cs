using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PerfTester
{
    static class PerfCollector
    {
        public enum TestCaseGroup
        {
            PureDotNet,
            PureMoonSharp,
            MoonSharpCallingDotNet,
            DotNetCallingMoonSharp,
            PureNLua,
            NLuacallingDotNet,
            DotNetCallingNLua,
            PureNeoLua,
            NeoLuaCallingDotNet,
            NeoLuaCallingDotNetPreCompiled,
            DotNetCallingNeoLua,
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

        public static Dictionary<string, TestCase> TestCases { get; set; } = new Dictionary<string, TestCase>();

        public static TestCase GetTestCase(string name)
        {
            if (!TestCases.ContainsKey(name))
            {
                TestCases.Add(name, new TestCase());
            }

            return TestCases[name];
        }
    }
}
