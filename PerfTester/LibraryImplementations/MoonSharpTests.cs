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

        public void RunDotNetCallTest(int testItteration)
        {
            IntWorkMethods workMethods = new IntWorkMethods();            
            UserData.RegisterType<IntWorkMethods>();
            UserData.RegisterType<StringWorkMethods>();

            Script script = new Script();
            DynValue itterationCount = DynValue.NewNumber(testItteration);
            script.Globals.Set("GlobalItterationCount", itterationCount);

            script.Globals.Set("DotNetIntWorkMethods", UserData.Create(new IntWorkMethods()));
            script.Globals.Set("DotNetStringWorkMethods", UserData.Create(new StringWorkMethods()));

            TestCase testCase = GetTestCase("AddInts");
            testCase.StartTimer(TestCaseGroup.MoonSharpCallingDotNet);
            script.DoString("for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Add(i, i+1) end");
            testCase.EndTimer(TestCaseGroup.MoonSharpCallingDotNet);

            testCase = GetTestCase("SubtractInts");
            testCase.StartTimer(TestCaseGroup.MoonSharpCallingDotNet);
            script.DoString("for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Subtract(i, i+1) end");
            testCase.EndTimer(TestCaseGroup.MoonSharpCallingDotNet);

            testCase = GetTestCase("StringFlip");
            testCase.StartTimer(TestCaseGroup.MoonSharpCallingDotNet);
            script.DoString("local testString = 'testString' for i=1, GlobalItterationCount, 1 do testString = DotNetStringWorkMethods.StringFlip(testString) end");
            testCase.EndTimer(TestCaseGroup.MoonSharpCallingDotNet);
        }

        public void RunPureLuaTest(int testItteration)
        {
            string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
            Script script = new Script();
            script.DoString(File.ReadAllText(workMethodsScript));

            DynValue itterationCount = DynValue.NewNumber(testItteration);
            script.Globals.Set("GlobalItterationCount", itterationCount);

            TestCase testCase = GetTestCase("AddInts");
            testCase.StartTimer(TestCaseGroup.PureMoonSharp);
            script.DoString("for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods.Add(i, i+1) end");
            testCase.EndTimer(TestCaseGroup.PureMoonSharp);

            testCase = GetTestCase("SubtractInts");
            testCase.StartTimer(TestCaseGroup.PureMoonSharp);
            script.DoString("for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods.Subtract(i, i+1) end");
            testCase.EndTimer(TestCaseGroup.PureMoonSharp);

            testCase = GetTestCase("StringFlip");
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

            TestCase testCase = GetTestCase("AddInts");
            testCase.StartTimer(TestCaseGroup.DotNetCallingMoonSharp);
            for (int i = 1; i < itterationCount; i++)
            {
                double result = script.Call(intScripts.Table.Get("AddInts"), i, i + 1).Number;
            }
            testCase.EndTimer(TestCaseGroup.DotNetCallingMoonSharp);

            testCase = GetTestCase("SubtractInts");
            testCase.StartTimer(TestCaseGroup.DotNetCallingMoonSharp);
            for (int i = 1; i < itterationCount; i++)
            {
                double result = script.Call(intScripts.Table.Get("SubtractInts"), i, i + 1).Number;
            }
            testCase.EndTimer(TestCaseGroup.DotNetCallingMoonSharp);

            testCase = GetTestCase("StringFlip");
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
