using NLua;
using System.IO;
using System.Linq;
using System.Reflection;
using static PerfTester.PerfCollector;

namespace PerfTester
{
    class NLuaTests
    {
        public NLuaTests(int testItteration)
        {
            RunDotNetCallTest(testItteration);
            RunPureLuaTest(testItteration);
            RunLuaCalledFromDotNetTests(testItteration);
        }

        public void RunDotNetCallTest(int testItteration)
        {
            Lua script = new Lua();
            script["GlobalItterationCount"] = testItteration;
            script["DotNetIntWorkMethods"] = new IntWorkMethods();
            script["DotNetStringWorkMethods"] = new StringWorkMethods();

            TestCase testCase = GetTestCase("AddInts");
            testCase.StartTimer(TestCaseGroup.NLuacallingDotNet);
            script.DoString("for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Add(i, i+1) end");
            testCase.EndTimer(TestCaseGroup.NLuacallingDotNet);

            testCase = GetTestCase("SubtractInts");
            testCase.StartTimer(TestCaseGroup.NLuacallingDotNet);
            script.DoString("for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Subtract(i, i+1) end");
            testCase.EndTimer(TestCaseGroup.NLuacallingDotNet);

            testCase = GetTestCase("StringFlip");
            testCase.StartTimer(TestCaseGroup.NLuacallingDotNet);
            script.DoString("local testString = 'testString' for i=1, GlobalItterationCount, 1 do testString = DotNetStringWorkMethods:StringFlip(testString) end");
            testCase.EndTimer(TestCaseGroup.NLuacallingDotNet);
        }

        public void RunPureLuaTest(int testItteration)
        {
            string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
            Lua script = new Lua();
            script["GlobalItterationCount"] = testItteration;
            script.DoString(File.ReadAllText(workMethodsScript));

            TestCase testCase = GetTestCase("AddInts");
            testCase.StartTimer(TestCaseGroup.PureNLua);
            script.DoString("for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods.Add(i, i+1) end");
            testCase.EndTimer(TestCaseGroup.PureNLua);

            testCase = GetTestCase("SubtractInts");
            testCase.StartTimer(TestCaseGroup.PureNLua);
            script.DoString("for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods.Subtract(i, i+1) end");
            testCase.EndTimer(TestCaseGroup.PureNLua);

            testCase = GetTestCase("StringFlip");
            testCase.StartTimer(TestCaseGroup.PureNLua);
            script.DoString("local testString = 'testString' for i=1, GlobalItterationCount, 1 do testString = LuaStringWorkMethods.StringFlip(testString) end");
            testCase.EndTimer(TestCaseGroup.PureNLua);
        }

        public void RunLuaCalledFromDotNetTests(int itterationCount)
        {
            string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
            Lua script = new Lua();
            script.DoString(File.ReadAllText(workMethodsScript));

            TestCase testCase = GetTestCase("AddInts");
            testCase.StartTimer(TestCaseGroup.DotNetCallingNLua);
            for (int i = 1; i < itterationCount; i++)
            {
                var scriptFunc = script["LuaIntWorkMethods.Add"] as LuaFunction;
                var res = (System.Int64)scriptFunc.Call(i, i + 1).First();
            }
            testCase.EndTimer(TestCaseGroup.DotNetCallingNLua);

            testCase = GetTestCase("SubtractInts");
            testCase.StartTimer(TestCaseGroup.DotNetCallingNLua);
            for (int i = 1; i < itterationCount; i++)
            {
                var scriptFunc = script["LuaIntWorkMethods.Subtract"] as LuaFunction;
                var res = (System.Int64)scriptFunc.Call(i, i + 1).First();
            }
            testCase.EndTimer(TestCaseGroup.DotNetCallingNLua);

            testCase = GetTestCase("StringFlip");
            testCase.StartTimer(TestCaseGroup.DotNetCallingNLua);
            string testString = "testString";
            for (int i = 1; i < itterationCount; i++)
            {
                var scriptFunc = script["LuaStringWorkMethods.StringFlip"] as LuaFunction;
                testString = (string)scriptFunc.Call(testString).First();
            }
            testCase.EndTimer(TestCaseGroup.DotNetCallingNLua);
        }
    }
}
