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
            RunDotNetCallCompiledNoTimeCompileTest(testItteration);
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

                TestCase testCase = GetTestCase(TestCaseType.AddInts);
                testCase.StartTimer(TestCaseGroup.DotNetCallingNeoLua);
                for (int i = 1; i < testItteration; i++)
                {
                    double result = g.LuaIntWorkMethods.Add(i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.DotNetCallingNeoLua);

                testCase = GetTestCase(TestCaseType.SubtractInts);
                testCase.StartTimer(TestCaseGroup.DotNetCallingNeoLua);
                for (int i = 1; i < testItteration; i++)
                {
                    double result = g.LuaIntWorkMethods.Subtract(i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.DotNetCallingNeoLua);

                testCase = GetTestCase(TestCaseType.StringFlip);
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

                TestCase testCase = GetTestCase(TestCaseType.AddInts);
                testCase.StartTimer(TestCaseGroup.PureNeoLua);
                g.dochunk("for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods.Add(i, i+1) end", "test.lua");
                testCase.EndTimer(TestCaseGroup.PureNeoLua);

                testCase = GetTestCase(TestCaseType.SubtractInts);
                testCase.StartTimer(TestCaseGroup.PureNeoLua);
                g.dochunk("for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods.Subtract(i, i+1) end", "test.lua");
                testCase.EndTimer(TestCaseGroup.PureNeoLua);

                testCase = GetTestCase(TestCaseType.StringFlip);
                testCase.StartTimer(TestCaseGroup.PureNeoLua);
                g.dochunk("local testString = 'testString' for i=1, GlobalItterationCount, 1 do testString = LuaStringWorkMethods.StringFlip(testString) end", "test.lua");
                testCase.EndTimer(TestCaseGroup.PureNeoLua);
            }
        }

        private void RunDotNetCallTest(int testItteration)
        {
            using (var l = new Lua())
            {
                dynamic script = l.CreateEnvironment<LuaGlobal>();
                script.GlobalItterationCount = testItteration;
                script.DotNetIntWorkMethods = new IntWorkMethods();
                script.DotNetFloatWorkMethods = new FloatWorkMethods();
                script.DotNetStringWorkMethods = new StringWorkMethods();
                script.DotNetCollectionWorkMethods = new CollectionWorkMethods();

                RunTestCaseScript(script, TestCaseType.AddInts, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Add(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.AddFloats, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Add(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.SubtractInts, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Subtract(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.SubtractFloats, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Subtract(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.MultiplyInts, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Multiply(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.MultiplyFloats, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Multiply(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.DivideInts, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Divide(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.DivideFloats, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Divide(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsAreEqual, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.IsEqual(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsAreEqual, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.IsEqual(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsAreNotEqual, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.NotEqual(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsAreNotEqual, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.NotEqual(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsGreaterThan, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Greater(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsGreaterThan, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Greater(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.GreaterOrEqual(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.GreaterOrEqual(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsLesserThan, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Lesser(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsLesserThan, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Lesser(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.LesserOrEqual(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.LesserOrEqual(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.Remainder, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Remainder(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.Exponent, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Exponent(i, i+1) end");

                RunTestCaseScript(script, TestCaseType.StringFlip, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = 'testString' result = DotNetStringWorkMethods.StringFlip(result) end");
                RunTestCaseScript(script, TestCaseType.CombineStrings, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local result = DotNetStringWorkMethods.CombineStrings(stringOne, stringTwo) end");
                RunTestCaseScript(script, TestCaseType.FormatStrings, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local result = DotNetStringWorkMethods.CombineStringsFormat(stringOne, stringTwo) end");
                RunTestCaseScript(script, TestCaseType.CombineManyStrings, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' local result = DotNetStringWorkMethods.CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
                RunTestCaseScript(script, TestCaseType.FormatManyStrings, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' local result = DotNetStringWorkMethods.CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");

                RunTestCaseScript(script, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('BestItem', i) else DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('Item' .. i, i) end end");
                RunTestCaseScript(script, TestCaseType.GetValueFromDictionary, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.GetValueFromDictionary('BestItem') end");
                RunTestCaseScript(script, TestCaseType.AddStringToList, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.AddStringToList('Item' .. i) end");
                RunTestCaseScript(script, TestCaseType.ItterateListAndGetCount, TestCaseGroup.NeoLuaCallingDotNet, "for i=1, GlobalItterationCount / 10000, 1 do local result = DotNetCollectionWorkMethods.ItterateThroughListAndGetCount() end");
            }
        }

        private void RunTestCaseScript(dynamic g, TestCaseType caseType, TestCaseGroup group, string runScript)
        {
            TestCase testCase = GetTestCase(caseType);
            testCase.StartTimer(group);
            g.dochunk(runScript);
            testCase.EndTimer(group);
        }

        private void RunTestCaseCompiledScript(dynamic g, Lua l, TestCaseType caseType, TestCaseGroup group, string runScript)
        {
            TestCase testCase = GetTestCase(caseType);
            testCase.StartTimer(group);
            var chunk = l.CompileChunk(runScript, "test.lua", new LuaCompileOptions());
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
            testCase.EndTimer(group);
        }

        private void RunTestCaseCompiledNoTimeCompileScript(dynamic g, Lua l, TestCaseType caseType, TestCaseGroup group, string runScript)
        {
            TestCase testCase = GetTestCase(caseType);            
            var chunk = l.CompileChunk(runScript, "test.lua", new LuaCompileOptions());
            testCase.StartTimer(group);
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
            testCase.EndTimer(group);
        }

        private void RunDotNetCallCompiledTest(int testItteration)
        {
            using (var l = new Lua())
            {
                dynamic script = l.CreateEnvironment<LuaGlobal>();
                script.GlobalItterationCount = testItteration;
                script.DotNetIntWorkMethods = new IntWorkMethods();
                script.DotNetFloatWorkMethods = new FloatWorkMethods();
                script.DotNetStringWorkMethods = new StringWorkMethods();
                script.DotNetCollectionWorkMethods = new CollectionWorkMethods();

                RunTestCaseCompiledScript(script, l, TestCaseType.AddInts, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Add(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.AddFloats, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Add(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.SubtractInts, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Subtract(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.SubtractFloats, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Subtract(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.MultiplyInts, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Multiply(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.MultiplyFloats, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Multiply(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.DivideInts, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Divide(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.DivideFloats, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Divide(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.IntsAreEqual, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.IsEqual(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FloatsAreEqual, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.IsEqual(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.IntsAreNotEqual, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.NotEqual(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FloatsAreNotEqual, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.NotEqual(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.IntsGreaterThan, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Greater(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FloatsGreaterThan, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Greater(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.GreaterOrEqual(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.GreaterOrEqual(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.IntsLesserThan, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Lesser(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FloatsLesserThan, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Lesser(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.LesserOrEqual(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.LesserOrEqual(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.Remainder, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Remainder(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.Exponent, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Exponent(i, i+1) end");

                RunTestCaseCompiledScript(script, l, TestCaseType.StringFlip, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = 'testString' result = DotNetStringWorkMethods.StringFlip(result) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.CombineStrings, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local result = DotNetStringWorkMethods.CombineStrings(stringOne, stringTwo) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FormatStrings, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local result = DotNetStringWorkMethods.CombineStringsFormat(stringOne, stringTwo) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.CombineManyStrings, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' local result = DotNetStringWorkMethods.CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FormatManyStrings, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' local result = DotNetStringWorkMethods.CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");

                RunTestCaseCompiledScript(script, l, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('BestItem', i) else DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('Item' .. i, i) end end");
                RunTestCaseCompiledScript(script, l, TestCaseType.GetValueFromDictionary, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.GetValueFromDictionary('BestItem') end");
                RunTestCaseCompiledScript(script, l, TestCaseType.AddStringToList, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.AddStringToList('Item' .. i) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.ItterateListAndGetCount, TestCaseGroup.NeoLuaCallingDotNetPreCompiled, "for i=1, GlobalItterationCount / 10000, 1 do local result = DotNetCollectionWorkMethods.ItterateThroughListAndGetCount() end");
            }
        }

        private void RunDotNetCallCompiledNoTimeCompileTest(int testItteration)
        {
            using (var l = new Lua())
            {
                dynamic script = l.CreateEnvironment<LuaGlobal>();
                script.GlobalItterationCount = testItteration;
                script.DotNetIntWorkMethods = new IntWorkMethods();
                script.DotNetFloatWorkMethods = new FloatWorkMethods();
                script.DotNetStringWorkMethods = new StringWorkMethods();
                script.DotNetCollectionWorkMethods = new CollectionWorkMethods();

                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.AddInts, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Add(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.AddFloats, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Add(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.SubtractInts, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Subtract(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.SubtractFloats, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Subtract(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.MultiplyInts, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Multiply(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.MultiplyFloats, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Multiply(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.DivideInts, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Divide(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.DivideFloats, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Divide(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.IntsAreEqual, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.IsEqual(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FloatsAreEqual, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.IsEqual(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.IntsAreNotEqual, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.NotEqual(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FloatsAreNotEqual, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.NotEqual(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.IntsGreaterThan, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Greater(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FloatsGreaterThan, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Greater(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.GreaterOrEqual(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.GreaterOrEqual(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.IntsLesserThan, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Lesser(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FloatsLesserThan, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Lesser(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.LesserOrEqual(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.LesserOrEqual(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.Remainder, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Remainder(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.Exponent, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Exponent(i, i+1) end");

                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.StringFlip, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = 'testString' result = DotNetStringWorkMethods.StringFlip(result) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.CombineStrings, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local result = DotNetStringWorkMethods.CombineStrings(stringOne, stringTwo) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FormatStrings, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local result = DotNetStringWorkMethods.CombineStringsFormat(stringOne, stringTwo) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.CombineManyStrings, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' local result = DotNetStringWorkMethods.CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FormatManyStrings, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' local result = DotNetStringWorkMethods.CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");

                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('BestItem', i) else DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('Item' .. i, i) end end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.GetValueFromDictionary, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.GetValueFromDictionary('BestItem') end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.AddStringToList, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.AddStringToList('Item' .. i) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.ItterateListAndGetCount, TestCaseGroup.NeoLuaCallingDotNetPreCompiledNoCompileTime, "for i=1, GlobalItterationCount / 10000, 1 do local result = DotNetCollectionWorkMethods.ItterateThroughListAndGetCount() end");
            }
        }
    }
}
