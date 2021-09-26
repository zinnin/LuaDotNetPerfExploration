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
            RuNeoLuaCalledFromDotNetTests(testItteration);
            RunDotNetCallCompiledTest(testItteration);
            RunDotNetCallCompiledNoTimeCompileTest(testItteration);
        }

        private void RuNeoLuaCalledFromDotNetTests(int itterationCount)
        {
            using (var l = new Lua())
            {
                dynamic g = l.CreateEnvironment<LuaGlobal>();
                IntWorkMethods workMethods = new IntWorkMethods();
                g.GlobalItterationCount = itterationCount;

                string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
                g.dochunk(File.ReadAllText(workMethodsScript), "WorkMethods.lua");

                TestCase testCase = GetTestCase(TestCaseType.AddInts);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    int result = g.LuaIntWorkMethods.Add(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.AddFloats);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    float result = g.LuaIntWorkMethods.Add(g.LuaIntWorkMethods, i, i + 1.5F);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.SubtractInts);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    int result = g.LuaIntWorkMethods.Subtract(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.SubtractFloats);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    float result = g.LuaIntWorkMethods.Subtract(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.MultiplyInts);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    int result = g.LuaIntWorkMethods.Multiply(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.MultiplyInts);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    float result = g.LuaIntWorkMethods.Multiply(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.DivideInts);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    int result = (int)g.LuaIntWorkMethods.Divide(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.DivideFloats);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    float result = (float)g.LuaIntWorkMethods.Divide(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.Remainder);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    int result = g.LuaIntWorkMethods.Remainder(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.Exponent);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    int result = (int)g.LuaIntWorkMethods.Exponent(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.IntsAreEqual);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    bool result = g.LuaIntWorkMethods.IsEqual(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.FloatsAreEqual);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    bool result = g.LuaIntWorkMethods.IsEqual(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.IntsAreNotEqual);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    bool result = g.LuaIntWorkMethods.NotEqual(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.FloatsAreNotEqual);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    bool result = g.LuaIntWorkMethods.NotEqual(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.IntsGreaterThan);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    bool result = g.LuaIntWorkMethods.Greater(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.FloatsGreaterThan);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    bool result = g.LuaIntWorkMethods.Greater(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.IntsGreaterOrEqualTo);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    bool result = g.LuaIntWorkMethods.GreaterOrEqual(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.FloatsGreaterOrEqualTo);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    bool result = g.LuaIntWorkMethods.GreaterOrEqual(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.IntsLesserThan);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    bool result = g.LuaIntWorkMethods.Lesser(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.FloatsLesserThan);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    bool result = g.LuaIntWorkMethods.Lesser(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.IntsLesserOrEqualTo);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    bool result = g.LuaIntWorkMethods.LesserOrEqual(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.FloatsLesserOrEqualTo);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    bool result = g.LuaIntWorkMethods.LesserOrEqual(g.LuaIntWorkMethods, i, i + 1);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.StringFlip);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                string testString = "testString";
                for (int i = 1; i < itterationCount; i++)
                {
                    testString = g.LuaStringWorkMethods.StringFlip(g.LuaStringWorkMethods, testString);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.CombineStrings);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                string stringOne = "stringOne";
                string stringTwo = "stringTwo";
                for (int i = 1; i < itterationCount; i++)
                {
                    string result = g.LuaStringWorkMethods.CombineStrings(g.LuaStringWorkMethods, stringOne, stringTwo);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.FormatStrings);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    string result = g.LuaStringWorkMethods.CombineStringsFormat(g.LuaStringWorkMethods, stringOne, stringTwo);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                string stringThree = "stringThree";
                string stringFour = "stringFour";
                string stringFive = "stringFive";
                string stringSix = "stringSix";
                string stringSeven = "stringSeven";
                string stringEight = "stringEight";
                string stringNine = "stringNine";
                string stringTen = "stringTen";

                testCase = GetTestCase(TestCaseType.CombineManyStrings);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    string result = g.LuaStringWorkMethods.CombineManyStrings(g.LuaStringWorkMethods, stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.FormatManyStrings);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    string result = g.LuaStringWorkMethods.CombineManyStringsFormat(g.LuaStringWorkMethods, stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen);
                }
                testCase.EndTimer(TestCaseGroup.NeoLuaCalled);

                testCase = GetTestCase(TestCaseType.AddOrUpdateValueInDictionary);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    if (i % 5 == 0)
                    {
                        g.LuaCollectionWorkMethods.AddOrUpdateValueInDictionary(g.LuaCollectionWorkMethods, "BestItem", i);
                    }
                    else
                    {
                        g.LuaCollectionWorkMethods.AddOrUpdateValueInDictionary(g.LuaCollectionWorkMethods, "Item" + i.ToString(), i);
                    }
                }
                testCase.EndTimer(TestCaseGroup.DotNetPure);

                testCase = GetTestCase(TestCaseType.GetValueFromDictionary);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    int item = g.LuaCollectionWorkMethods.GetValueFromDictionary(g.LuaCollectionWorkMethods, "BestItem");
                }
                testCase.EndTimer(TestCaseGroup.DotNetPure);

                testCase = GetTestCase(TestCaseType.AddStringToList);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount; i++)
                {
                    g.LuaCollectionWorkMethods.AddStringToList(g.LuaCollectionWorkMethods, "Item" + i.ToString());
                }
                testCase.EndTimer(TestCaseGroup.DotNetPure);

                testCase = GetTestCase(TestCaseType.ItterateListAndGetCount);
                testCase.StartTimer(TestCaseGroup.NeoLuaCalled);
                for (int i = 1; i < itterationCount / 10000; i++)
                {
                    int result = g.LuaCollectionWorkMethods.ItterateThroughListAndGetCount(g.LuaCollectionWorkMethods);
                }
                testCase.EndTimer(TestCaseGroup.DotNetPure);
            }
        }

        private void RunPureLuaTest(int testItteration)
        {
            using (var l = new Lua())
            {
                dynamic script = l.CreateEnvironment<LuaGlobal>();

                string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
                var chunk = l.CompileChunk(File.ReadAllText(workMethodsScript), "test.lua", new LuaCompileOptions());
                try
                {
                    script.dochunk(chunk); // execute the chunk
                }
                catch (Exception e)
                {
                    Console.WriteLine("Expception: {0}", e.Message);
                    var d = LuaExceptionData.GetData(e); // get stack trace
                    Console.WriteLine("StackTrace: {0}", d.FormatStackTrace(0, false));
                }
                script.GlobalItterationCount = testItteration;

                RunTestCaseScript(script, TestCaseType.AddInts, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Add(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.AddFloats, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Add(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.SubtractInts, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Subtract(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.SubtractFloats, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Subtract(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.MultiplyInts, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Multiply(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.MultiplyFloats, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Multiply(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.DivideInts, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Divide(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.DivideFloats, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Divide(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsAreEqual, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:IsEqual(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsAreEqual, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:IsEqual(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsAreNotEqual, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:NotEqual(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsAreNotEqual, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:NotEqual(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsGreaterThan, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Greater(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsGreaterThan, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Greater(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:GreaterOrEqual(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:GreaterOrEqual(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsLesserThan, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Lesser(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsLesserThan, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Lesser(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:LesserOrEqual(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:LesserOrEqual(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.Remainder, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Remainder(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.Exponent, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Exponent(i, i+1) end");

                RunTestCaseScript(script, TestCaseType.StringFlip, TestCaseGroup.NeoLuaPure, "local result = 'testString' for i=1, GlobalItterationCount, 1 do result = LuaStringWorkMethods:StringFlip(result) end");
                RunTestCaseScript(script, TestCaseType.CombineStrings, TestCaseGroup.NeoLuaPure, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = LuaStringWorkMethods:CombineStrings(stringOne, stringTwo) end");
                RunTestCaseScript(script, TestCaseType.FormatStrings, TestCaseGroup.NeoLuaPure, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = LuaStringWorkMethods:CombineStringsFormat(stringOne, stringTwo) end");
                RunTestCaseScript(script, TestCaseType.CombineManyStrings, TestCaseGroup.NeoLuaPure, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = LuaStringWorkMethods:CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
                RunTestCaseScript(script, TestCaseType.FormatManyStrings, TestCaseGroup.NeoLuaPure, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = LuaStringWorkMethods:CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");

                RunTestCaseScript(script, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then LuaCollectionWorkMethods:AddOrUpdateValueInDictionary('BestItem', i) else LuaCollectionWorkMethods:AddOrUpdateValueInDictionary('Item' .. i, i) end end");
                RunTestCaseScript(script, TestCaseType.GetValueFromDictionary, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaCollectionWorkMethods:GetValueFromDictionary('BestItem') end");
                RunTestCaseScript(script, TestCaseType.AddStringToList, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaCollectionWorkMethods:AddStringToList('Item' .. i) end");
                RunTestCaseScript(script, TestCaseType.ItterateListAndGetCount, TestCaseGroup.NeoLuaPure, "for i=1, GlobalItterationCount / 10000, 1 do local result = LuaCollectionWorkMethods:ItterateThroughListAndGetCount() end");
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

                RunTestCaseScript(script, TestCaseType.AddInts, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Add(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.AddFloats, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Add(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.SubtractInts, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Subtract(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.SubtractFloats, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Subtract(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.MultiplyInts, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Multiply(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.MultiplyFloats, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Multiply(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.DivideInts, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Divide(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.DivideFloats, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Divide(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsAreEqual, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.IsEqual(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsAreEqual, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.IsEqual(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsAreNotEqual, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.NotEqual(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsAreNotEqual, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.NotEqual(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsGreaterThan, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Greater(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsGreaterThan, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Greater(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.GreaterOrEqual(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.GreaterOrEqual(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsLesserThan, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Lesser(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsLesserThan, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Lesser(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.LesserOrEqual(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.LesserOrEqual(i, i+1.5) end");
                RunTestCaseScript(script, TestCaseType.Remainder, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Remainder(i, i+1) end");
                RunTestCaseScript(script, TestCaseType.Exponent, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Exponent(i, i+1) end");

                RunTestCaseScript(script, TestCaseType.StringFlip, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = 'testString' result = DotNetStringWorkMethods:StringFlip(result) end");
                RunTestCaseScript(script, TestCaseType.CombineStrings, TestCaseGroup.NeoLuaCalling, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineStrings(stringOne, stringTwo) end");
                RunTestCaseScript(script, TestCaseType.FormatStrings, TestCaseGroup.NeoLuaCalling, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineStringsFormat(stringOne, stringTwo) end");
                RunTestCaseScript(script, TestCaseType.CombineManyStrings, TestCaseGroup.NeoLuaCalling, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
                RunTestCaseScript(script, TestCaseType.FormatManyStrings, TestCaseGroup.NeoLuaCalling, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");

                RunTestCaseScript(script, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('BestItem', i) else DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('Item' .. i, i) end end");
                RunTestCaseScript(script, TestCaseType.GetValueFromDictionary, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.GetValueFromDictionary('BestItem') end");
                RunTestCaseScript(script, TestCaseType.AddStringToList, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.AddStringToList('Item' .. i) end");
                RunTestCaseScript(script, TestCaseType.ItterateListAndGetCount, TestCaseGroup.NeoLuaCalling, "for i=1, GlobalItterationCount / 10000, 1 do local result = DotNetCollectionWorkMethods.ItterateThroughListAndGetCount() end");
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

                RunTestCaseCompiledScript(script, l, TestCaseType.AddInts, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Add(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.AddFloats, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Add(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.SubtractInts, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Subtract(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.SubtractFloats, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Subtract(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.MultiplyInts, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Multiply(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.MultiplyFloats, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Multiply(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.DivideInts, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Divide(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.DivideFloats, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Divide(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.IntsAreEqual, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.IsEqual(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FloatsAreEqual, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.IsEqual(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.IntsAreNotEqual, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.NotEqual(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FloatsAreNotEqual, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.NotEqual(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.IntsGreaterThan, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Greater(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FloatsGreaterThan, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Greater(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.GreaterOrEqual(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.GreaterOrEqual(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.IntsLesserThan, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Lesser(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FloatsLesserThan, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Lesser(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.LesserOrEqual(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.LesserOrEqual(i, i+1.5) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.Remainder, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Remainder(i, i+1) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.Exponent, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Exponent(i, i+1) end");

                RunTestCaseCompiledScript(script, l, TestCaseType.StringFlip, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = 'testString' result = DotNetStringWorkMethods:StringFlip(result) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.CombineStrings, TestCaseGroup.NeoLuaCallingPreCompiled, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineStrings(stringOne, stringTwo) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FormatStrings, TestCaseGroup.NeoLuaCallingPreCompiled, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineStringsFormat(stringOne, stringTwo) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.CombineManyStrings, TestCaseGroup.NeoLuaCallingPreCompiled, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.FormatManyStrings, TestCaseGroup.NeoLuaCallingPreCompiled, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");

                RunTestCaseCompiledScript(script, l, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('BestItem', i) else DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('Item' .. i, i) end end");
                RunTestCaseCompiledScript(script, l, TestCaseType.GetValueFromDictionary, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.GetValueFromDictionary('BestItem') end");
                RunTestCaseCompiledScript(script, l, TestCaseType.AddStringToList, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.AddStringToList('Item' .. i) end");
                RunTestCaseCompiledScript(script, l, TestCaseType.ItterateListAndGetCount, TestCaseGroup.NeoLuaCallingPreCompiled, "for i=1, GlobalItterationCount / 10000, 1 do local result = DotNetCollectionWorkMethods.ItterateThroughListAndGetCount() end");
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

                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.AddInts, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Add(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.AddFloats, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Add(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.SubtractInts, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Subtract(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.SubtractFloats, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Subtract(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.MultiplyInts, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Multiply(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.MultiplyFloats, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Multiply(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.DivideInts, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Divide(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.DivideFloats, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Divide(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.IntsAreEqual, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.IsEqual(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FloatsAreEqual, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.IsEqual(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.IntsAreNotEqual, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.NotEqual(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FloatsAreNotEqual, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.NotEqual(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.IntsGreaterThan, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Greater(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FloatsGreaterThan, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Greater(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.GreaterOrEqual(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.GreaterOrEqual(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.IntsLesserThan, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Lesser(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FloatsLesserThan, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Lesser(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.LesserOrEqual(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.LesserOrEqual(i, i+1.5) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.Remainder, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Remainder(i, i+1) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.Exponent, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Exponent(i, i+1) end");

                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.StringFlip, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = 'testString' result = DotNetStringWorkMethods:StringFlip(result) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.CombineStrings, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineStrings(stringOne, stringTwo) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FormatStrings, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineStringsFormat(stringOne, stringTwo) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.CombineManyStrings, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.FormatManyStrings, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");

                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('BestItem', i) else DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('Item' .. i, i) end end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.GetValueFromDictionary, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.GetValueFromDictionary('BestItem') end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.AddStringToList, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.AddStringToList('Item' .. i) end");
                RunTestCaseCompiledNoTimeCompileScript(script, l, TestCaseType.ItterateListAndGetCount, TestCaseGroup.NeoLuaCallingPreCompiledNoCompileTime, "for i=1, GlobalItterationCount / 10000, 1 do local result = DotNetCollectionWorkMethods.ItterateThroughListAndGetCount() end");
            }
        }
    }
}
