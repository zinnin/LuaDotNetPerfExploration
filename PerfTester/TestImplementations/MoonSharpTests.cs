using MoonSharp.Interpreter;
using System.IO;
using System.Reflection;
using static PerfTester.PerfCollector;

namespace PerfTester
{
    class MoonSharpTests
    {
        public Script script = new Script();
        public MoonSharpTests(int testItteration)
        {
            Script.WarmUp();

            RunDotNetCallTest(testItteration);
            RunPureLuaTest(testItteration);
            RunLuaCalledFromDotNetTests(testItteration);
        }

        private void RunTestCaseScript(Script script, TestCaseType caseType, TestCaseGroup group, string runScript)
        {
            TestCase testCase = GetTestCase(caseType);
            testCase.StartTimer(group);
            script.DoString(runScript);
            testCase.EndTimer(group);
        }

        public void RunDotNetCallTest(int testItteration)
        {
            IntWorkMethods workMethods = new IntWorkMethods();            
            UserData.RegisterType<IntWorkMethods>();
            UserData.RegisterType<FloatWorkMethods>();
            UserData.RegisterType<StringWorkMethods>();

            Script script = new Script();
            DynValue itterationCount = DynValue.NewNumber(testItteration);
            script.Globals.Set("GlobalItterationCount", itterationCount);

            script.Globals.Set("DotNetIntWorkMethods", UserData.Create(new IntWorkMethods()));
            script.Globals.Set("DotNetFloatWorkMethods", UserData.Create(new FloatWorkMethods()));
            script.Globals.Set("DotNetStringWorkMethods", UserData.Create(new StringWorkMethods()));

            RunTestCaseScript(script, TestCaseType.AddInts, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Add(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.AddFloats, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Add(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.SubtractInts, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Subtract(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.SubtractFloats, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Subtract(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.MultiplyInts, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Multiply(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.MultiplyFloats, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Multiply(i, i+1.5) end");
        }

        public void RunPureLuaTest(int testItteration)
        {
            string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
            Script script = new Script();
            script.DoString(File.ReadAllText(workMethodsScript));

            DynValue itterationCount = DynValue.NewNumber(testItteration);
            script.Globals.Set("GlobalItterationCount", itterationCount);

            TestCase testCase = GetTestCase(TestCaseType.AddInts);
            testCase.StartTimer(TestCaseGroup.PureMoonSharp);
            script.DoString("for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods.Add(i, i+1) end");
            testCase.EndTimer(TestCaseGroup.PureMoonSharp);

            testCase = GetTestCase(TestCaseType.SubtractInts);
            testCase.StartTimer(TestCaseGroup.PureMoonSharp);
            script.DoString("for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods.Subtract(i, i+1) end");
            testCase.EndTimer(TestCaseGroup.PureMoonSharp);

            testCase = GetTestCase(TestCaseType.StringFlip);
            testCase.StartTimer(TestCaseGroup.PureMoonSharp);
            script.DoString("local testString = 'testString' for i=1, GlobalItterationCount, 1 do testString = LuaStringWorkMethods.StringFlip(testString) end");
            testCase.EndTimer(TestCaseGroup.PureMoonSharp);
        }

        public void RunLuaCalledFromDotNetTests(int itterationCount)
        {
            string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
            Script script = new Script();
            script.DoString(File.ReadAllText(workMethodsScript));
            DynValue intScripts = script.Globals.Get("LuaIntWorkMethods");
            DynValue stringScripts = script.Globals.Get("LuaStringWorkMethods");

            TestCase testCase = GetTestCase(TestCaseType.AddInts);
            testCase.StartTimer(TestCaseGroup.DotNetCallingMoonSharp);
            for (int i = 1; i < itterationCount; i++)
            {
                double result = script.Call(intScripts.Table.Get("Add"), i, i + 1).Number;
            }
            testCase.EndTimer(TestCaseGroup.DotNetCallingMoonSharp);

            testCase = GetTestCase(TestCaseType.SubtractInts);
            testCase.StartTimer(TestCaseGroup.DotNetCallingMoonSharp);
            for (int i = 1; i < itterationCount; i++)
            {
                double result = script.Call(intScripts.Table.Get("Subtract"), i, i + 1).Number;
            }
            testCase.EndTimer(TestCaseGroup.DotNetCallingMoonSharp);

            testCase = GetTestCase(TestCaseType.StringFlip);
            testCase.StartTimer(TestCaseGroup.DotNetCallingMoonSharp);
            string testString = "testString";
            for (int i = 1; i < itterationCount; i++)
            {
                testString = script.Call(stringScripts.Table.Get("StringFlip"), testString).String;
            }
            testCase.EndTimer(TestCaseGroup.DotNetCallingMoonSharp);
        }
    }
}
