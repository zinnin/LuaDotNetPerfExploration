using Neo.IronLua;
using System;
using System.IO;
using System.Reflection;
using static PerfTester.PerfCollector;

namespace PerfTester.LibraryImplementations
{
    class NeoLuaTests
    {
        public NeoLuaTests(int testItteration)
        {
            RunDotNetCallTest(testItteration);
            RunPureLuaTest(testItteration);
            RunLuaCalledFromDotNetTests(testItteration);
            RunDotNetCallCompiledTest(testItteration);
        }

        private void RunLuaCalledFromDotNetTests(int testItteration)
        {
            using (var l = new Lua())
            {
                dynamic g = l.CreateEnvironment<LuaGlobal>();
                IntWorkMethods workMethods = new IntWorkMethods();
                g.GlobalItterationCount = testItteration;

                string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
                g.dochunk(File.ReadAllText(workMethodsScript), "WorkMethods.lua");

                TestCase testCase = GetTestCase("AddInts");
                testCase.StartTimer(TestCaseGroup.DotNetCallingNeoLua);
                for (int i = 1; i < testItteration; i++)
                {
                    double result = g.LuaIntWorkMethods.Add(i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.DotNetCallingNeoLua);

                testCase = GetTestCase("SubtractInts");
                testCase.StartTimer(TestCaseGroup.DotNetCallingNeoLua);
                for (int i = 1; i < testItteration; i++)
                {
                    double result = g.LuaIntWorkMethods.Subtract(i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.DotNetCallingNeoLua);

                testCase = GetTestCase("StringFlip");
                testCase.StartTimer(TestCaseGroup.DotNetCallingNeoLua);
                string testString = "testString";
                for (int i = 1; i < testItteration; i++)
                {
                    testString = g.LuaStringWorkMethods.StringFlip(testString);
                }
                testCase.EndTimer(TestCaseGroup.DotNetCallingNeoLua);
            }
        }

        private void RunPureLuaTest(int testItteration)
        {
            using (var l = new Lua())
            {
                dynamic g = l.CreateEnvironment<LuaGlobal>();
                IntWorkMethods workMethods = new IntWorkMethods();
                g.GlobalItterationCount = testItteration;
                g.DotNetIntWorkMethods = new LuaTable();

                string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
                g.dochunk(File.ReadAllText(workMethodsScript), "WorkMethods.lua");

                TestCase testCase = GetTestCase("AddInts");
                testCase.StartTimer(TestCaseGroup.PureNeoLua);
                g.dochunk("for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods.Add(i, i+1) end", "test.lua");
                testCase.EndTimer(TestCaseGroup.PureNeoLua);

                testCase = GetTestCase("SubtractInts");
                testCase.StartTimer(TestCaseGroup.PureNeoLua);
                g.dochunk("for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods.Subtract(i, i+1) end", "test.lua");
                testCase.EndTimer(TestCaseGroup.PureNeoLua);

                testCase = GetTestCase("StringFlip");
                testCase.StartTimer(TestCaseGroup.PureNeoLua);
                g.dochunk("local testString = 'testString' for i=1, GlobalItterationCount, 1 do testString = LuaStringWorkMethods.StringFlip(testString) end", "test.lua");
                testCase.EndTimer(TestCaseGroup.PureNeoLua);
            }
        }

        private void RunDotNetCallTest(int testItteration)
        {
            using (var l = new Lua())
            {
                dynamic g = l.CreateEnvironment<LuaGlobal>();
                g.GlobalItterationCount = testItteration;
                g.DotNetIntWorkMethods = new IntWorkMethods();
                g.DotNetStringWorkMethods = new StringWorkMethods();

                TestCase testCase = GetTestCase("AddInts");
                testCase.StartTimer(TestCaseGroup.NeoLuaCallingDotNet);
                g.dochunk("for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Add(i, i+1) end", "test.lua");
                testCase.EndTimer(TestCaseGroup.NeoLuaCallingDotNet);

                testCase = GetTestCase("SubtractInts");
                testCase.StartTimer(TestCaseGroup.NeoLuaCallingDotNet);
                g.dochunk("for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Subtract(i, i+1) end", "test.lua");
                testCase.EndTimer(TestCaseGroup.NeoLuaCallingDotNet);

                testCase = GetTestCase("StringFlip");
                testCase.StartTimer(TestCaseGroup.NeoLuaCallingDotNet);
                g.dochunk("local testString = 'testString' for i=1, GlobalItterationCount, 1 do testString = DotNetStringWorkMethods.StringFlip(testString) end", "test.lua");
                testCase.EndTimer(TestCaseGroup.NeoLuaCallingDotNet);
            }
        }

        private void RunDotNetCallCompiledTest(int testItteration)
        {
            using (var l = new Lua())
            {
                dynamic g = l.CreateEnvironment<LuaGlobal>();
                g.GlobalItterationCount = testItteration;
                g.DotNetIntWorkMethods = new IntWorkMethods();
                g.DotNetStringWorkMethods = new StringWorkMethods();

                TestCase testCase = GetTestCase("AddInts");
                testCase.StartTimer(TestCaseGroup.NeoLuaCallingDotNetPreCompiled);
                var chunk = l.CompileChunk("for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Add(i, i+1) end", "test.lua", new LuaCompileOptions());
                try
                {
                    g.dochunk(chunk); // execute the chunk
                }
                catch (Exception e)
                {
                    Console.WriteLine("Expception: {0}", e.Message);
                    var d = LuaExceptionData.GetData(e); // get stack trace
                    Console.WriteLine("StackTrace: {0}", d.FormatStackTrace(0, false));
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCallingDotNetPreCompiled);

                testCase = GetTestCase("SubtractInts");
                testCase.StartTimer(TestCaseGroup.NeoLuaCallingDotNetPreCompiled);
                chunk = l.CompileChunk("for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Subtract(i, i+1) end", "test.lua", new LuaCompileOptions());
                try
                {
                    g.dochunk(chunk); // execute the chunk
                }
                catch (Exception e)
                {
                    Console.WriteLine("Expception: {0}", e.Message);
                    var d = LuaExceptionData.GetData(e); // get stack trace
                    Console.WriteLine("StackTrace: {0}", d.FormatStackTrace(0, false));
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCallingDotNetPreCompiled);

                testCase = GetTestCase("StringFlip");
                testCase.StartTimer(TestCaseGroup.NeoLuaCallingDotNetPreCompiled);
                chunk = l.CompileChunk("local testString = 'testString' for i=1, GlobalItterationCount, 1 do testString = DotNetStringWorkMethods.StringFlip(testString) end", "test.lua", new LuaCompileOptions());
                try
                {
                    g.dochunk(chunk); // execute the chunk
                }
                catch (Exception e)
                {
                    Console.WriteLine("Expception: {0}", e.Message);
                    var d = LuaExceptionData.GetData(e); // get stack trace
                    Console.WriteLine("StackTrace: {0}", d.FormatStackTrace(0, false));
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCallingDotNetPreCompiled);
            }
        }
    }
}
