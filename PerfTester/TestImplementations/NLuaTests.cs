using NLua;
using System;
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
            script["DotNetFloatWorkMethods"] = new FloatWorkMethods();
            script["DotNetStringWorkMethods"] = new StringWorkMethods();
            script["DotNetCollectionWorkMethods"] = new CollectionWorkMethods();

            RunTestCaseScript(script, TestCaseType.AddInts, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Add(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.AddFloats, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:Add(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.SubtractInts, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Subtract(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.SubtractFloats, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:Subtract(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.MultiplyInts, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Multiply(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.MultiplyFloats, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:Multiply(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.DivideInts, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Divide(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.DivideFloats, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:Divide(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsAreEqual, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:IsEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsAreEqual, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:IsEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsAreNotEqual, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:NotEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsAreNotEqual, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:NotEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsGreaterThan, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Greater(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsGreaterThan, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:Greater(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:GreaterOrEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:GreaterOrEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsLesserThan, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Lesser(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsLesserThan, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:Lesser(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:LesserOrEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:LesserOrEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.Remainder, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Remainder(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.Exponent, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Exponent(i, i+1) end");

            RunTestCaseScript(script, TestCaseType.StringFlip, TestCaseGroup.NLuaCalling, "local result = 'testString' for i=1, GlobalItterationCount, 1 do result = DotNetStringWorkMethods:StringFlip(result) end");
            RunTestCaseScript(script, TestCaseType.CombineStrings, TestCaseGroup.NLuaCalling, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineStrings(stringOne, stringTwo) end");
            RunTestCaseScript(script, TestCaseType.FormatStrings, TestCaseGroup.NLuaCalling, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineStringsFormat(stringOne, stringTwo) end");
            RunTestCaseScript(script, TestCaseType.CombineManyStrings, TestCaseGroup.NLuaCalling, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
            RunTestCaseScript(script, TestCaseType.FormatManyStrings, TestCaseGroup.NLuaCalling, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");

            RunTestCaseScript(script, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then DotNetCollectionWorkMethods:AddOrUpdateValueInDictionary('BestItem', i) else DotNetCollectionWorkMethods:AddOrUpdateValueInDictionary('Item' .. i, i) end end");
            RunTestCaseScript(script, TestCaseType.GetValueFromDictionary, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods:GetValueFromDictionary('BestItem') end");
            RunTestCaseScript(script, TestCaseType.AddStringToList, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods:AddStringToList('Item' .. i) end");
            RunTestCaseScript(script, TestCaseType.ItterateListAndGetCount, TestCaseGroup.NLuaCalling, "for i=1, GlobalItterationCount / 10000, 1 do local result = DotNetCollectionWorkMethods:ItterateThroughListAndGetCount() end");
        }

        public void RunPureLuaTest(int testItteration)
        {
            Lua script = new Lua();
            string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
            script.DoString(File.ReadAllText(workMethodsScript));
            script["GlobalItterationCount"] = testItteration;

            RunTestCaseScript(script, TestCaseType.AddInts, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Add(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.AddFloats, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Add(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.SubtractInts, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Subtract(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.SubtractFloats, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Subtract(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.MultiplyInts, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Multiply(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.MultiplyFloats, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Multiply(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.DivideInts, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Divide(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.DivideFloats, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Divide(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsAreEqual, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:IsEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsAreEqual, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:IsEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsAreNotEqual, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:NotEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsAreNotEqual, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:NotEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsGreaterThan, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Greater(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsGreaterThan, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Greater(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:GreaterOrEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:GreaterOrEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsLesserThan, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Lesser(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsLesserThan, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Lesser(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:LesserOrEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:LesserOrEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.Remainder, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Remainder(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.Exponent, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Exponent(i, i+1) end");

            RunTestCaseScript(script, TestCaseType.StringFlip, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = 'testString' result = LuaStringWorkMethods:StringFlip(result) end");
            RunTestCaseScript(script, TestCaseType.CombineStrings, TestCaseGroup.NLuaPure, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = LuaStringWorkMethods:CombineStrings(stringOne, stringTwo) end");
            RunTestCaseScript(script, TestCaseType.FormatStrings, TestCaseGroup.NLuaPure, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = LuaStringWorkMethods:CombineStringsFormat(stringOne, stringTwo) end");
            RunTestCaseScript(script, TestCaseType.CombineManyStrings, TestCaseGroup.NLuaPure, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = LuaStringWorkMethods:CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
            RunTestCaseScript(script, TestCaseType.FormatManyStrings, TestCaseGroup.NLuaPure, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = LuaStringWorkMethods:CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");

            RunTestCaseScript(script, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then LuaCollectionWorkMethods:AddOrUpdateValueInDictionary('BestItem', i) else LuaCollectionWorkMethods:AddOrUpdateValueInDictionary('Item' .. i, i) end end");
            RunTestCaseScript(script, TestCaseType.GetValueFromDictionary, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaCollectionWorkMethods:GetValueFromDictionary('BestItem') end");
            RunTestCaseScript(script, TestCaseType.AddStringToList, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount, 1 do local result = LuaCollectionWorkMethods:AddStringToList('Item' .. i) end");
            RunTestCaseScript(script, TestCaseType.ItterateListAndGetCount, TestCaseGroup.NLuaPure, "for i=1, GlobalItterationCount / 10000, 1 do local result = LuaCollectionWorkMethods:ItterateThroughListAndGetCount() end");
        }

        private void RunTestCaseScript(Lua script, TestCaseType caseType, TestCaseGroup group, string runScript)
        {
            TestCase testCase = GetTestCase(caseType);
            testCase.StartTimer(group);
            script.DoString(runScript);
            testCase.EndTimer(group);
        }

        public void RunLuaCalledFromDotNetTests(int itterationCount)
        {
            string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
            Lua script = new Lua();
            script.DoString(File.ReadAllText(workMethodsScript));

            TestCase testCase = GetTestCase(TestCaseType.AddInts);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                int.TryParse(((LuaFunction)script["LuaIntWorkMethods.Add"]).Call(script["LuaIntWorkMethods"], i, i + 1).First().ToString(), out int result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.AddFloats);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                float.TryParse(((LuaFunction)script["LuaIntWorkMethods.Add"]).Call(script["LuaIntWorkMethods"], i, i + 1.5F).First().ToString(), out float result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.SubtractInts);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                int.TryParse(((LuaFunction)script["LuaIntWorkMethods.Subtract"]).Call(script["LuaIntWorkMethods"], i, i + 1).First().ToString(), out int result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.SubtractFloats);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                float.TryParse(((LuaFunction)script["LuaIntWorkMethods.Subtract"]).Call(script["LuaIntWorkMethods"], i, i + 1.5F).First().ToString(), out float result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.MultiplyInts);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                int.TryParse(((LuaFunction)script["LuaIntWorkMethods.Multiply"]).Call(script["LuaIntWorkMethods"], i, i + 1).First().ToString(), out int result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.MultiplyInts);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                float.TryParse(((LuaFunction)script["LuaIntWorkMethods.Multiply"]).Call(script["LuaIntWorkMethods"], i, i + 1.5F).First().ToString(), out float result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.DivideInts);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                int.TryParse(((LuaFunction)script["LuaIntWorkMethods.Divide"]).Call(script["LuaIntWorkMethods"], i, i + 1).First().ToString(), out int result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.DivideFloats);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                float.TryParse(((LuaFunction)script["LuaIntWorkMethods.Divide"]).Call(script["LuaIntWorkMethods"], i, i + 1.5F).First().ToString(), out float result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.Remainder);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                int.TryParse(((LuaFunction)script["LuaIntWorkMethods.Remainder"]).Call(script["LuaIntWorkMethods"], i, i + 1).First().ToString(), out int result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.Exponent);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                int.TryParse(((LuaFunction)script["LuaIntWorkMethods.Exponent"]).Call(script["LuaIntWorkMethods"], i, i + 1.5F).First().ToString(), out int result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.IntsAreEqual);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool.TryParse(((LuaFunction)script["LuaIntWorkMethods.IsEqual"]).Call(script["LuaIntWorkMethods"], i, i + 1).First().ToString(), out bool result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.FloatsAreEqual);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool.TryParse(((LuaFunction)script["LuaIntWorkMethods.IsEqual"]).Call(script["LuaIntWorkMethods"], i, i + 1.5F).First().ToString(), out bool result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.IntsAreNotEqual);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool.TryParse(((LuaFunction)script["LuaIntWorkMethods.NotEqual"]).Call(script["LuaIntWorkMethods"], i, i + 1).First().ToString(), out bool result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.FloatsAreNotEqual);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool.TryParse(((LuaFunction)script["LuaIntWorkMethods.NotEqual"]).Call(script["LuaIntWorkMethods"], i, i + 1.5F).First().ToString(), out bool result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.IntsGreaterThan);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool.TryParse(((LuaFunction)script["LuaIntWorkMethods.Greater"]).Call(script["LuaIntWorkMethods"], i, i + 1).First().ToString(), out bool result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.FloatsGreaterThan);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool.TryParse(((LuaFunction)script["LuaIntWorkMethods.Greater"]).Call(script["LuaIntWorkMethods"], i, i + 1.5F).First().ToString(), out bool result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.IntsGreaterOrEqualTo);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool.TryParse(((LuaFunction)script["LuaIntWorkMethods.GreaterOrEqual"]).Call(script["LuaIntWorkMethods"], i, i + 1).First().ToString(), out bool result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.FloatsGreaterOrEqualTo);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool.TryParse(((LuaFunction)script["LuaIntWorkMethods.GreaterOrEqual"]).Call(script["LuaIntWorkMethods"], i, i + 1.5F).First().ToString(), out bool result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.IntsLesserThan);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool.TryParse(((LuaFunction)script["LuaIntWorkMethods.Lesser"]).Call(script["LuaIntWorkMethods"], i, i + 1).First().ToString(), out bool result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.FloatsLesserThan);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool.TryParse(((LuaFunction)script["LuaIntWorkMethods.Lesser"]).Call(script["LuaIntWorkMethods"], i, i + 1.5F).First().ToString(), out bool result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.IntsLesserOrEqualTo);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool.TryParse(((LuaFunction)script["LuaIntWorkMethods.LesserOrEqual"]).Call(script["LuaIntWorkMethods"], i, i + 1).First().ToString(), out bool result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.FloatsLesserOrEqualTo);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool.TryParse(((LuaFunction)script["LuaIntWorkMethods.LesserOrEqual"]).Call(script["LuaIntWorkMethods"], i, i + 1.5F).First().ToString(), out bool result);
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.StringFlip);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            string testString = "testString";
            for (int i = 1; i < itterationCount; i++)
            {
                testString = ((LuaFunction)script["LuaStringWorkMethods.StringFlip"]).Call(script["LuaStringWorkMethods"], testString).First().ToString();
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.CombineStrings);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            string stringOne = "stringOne";
            string stringTwo = "stringTwo";
            for (int i = 1; i < itterationCount; i++)
            {
                string result = ((LuaFunction)script["LuaStringWorkMethods.CombineStrings"]).Call(script["LuaStringWorkMethods"], stringOne, stringTwo).First().ToString();
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.FormatStrings);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                string result = ((LuaFunction)script["LuaStringWorkMethods.CombineStringsFormat"]).Call(script["LuaStringWorkMethods"], stringOne, stringTwo).First().ToString();
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            string stringThree = "stringThree";
            string stringFour = "stringFour";
            string stringFive = "stringFive";
            string stringSix = "stringSix";
            string stringSeven = "stringSeven";
            string stringEight = "stringEight";
            string stringNine = "stringNine";
            string stringTen = "stringTen";

            testCase = GetTestCase(TestCaseType.CombineManyStrings);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                string result = ((LuaFunction)script["LuaStringWorkMethods.CombineManyStrings"]).Call(script["LuaStringWorkMethods"], stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen).First().ToString();
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.FormatManyStrings);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                string result = ((LuaFunction)script["LuaStringWorkMethods.CombineManyStringsFormat"]).Call(script["LuaStringWorkMethods"], stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen).First().ToString();
            }
            testCase.EndTimer(TestCaseGroup.NLuaCalled);

            testCase = GetTestCase(TestCaseType.AddOrUpdateValueInDictionary);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                if (i % 5 == 0)
                {
                    ((LuaFunction)script["LuaCollectionWorkMethods.AddOrUpdateValueInDictionary"]).Call(script["LuaCollectionWorkMethods"], "BestItem", i);
                }
                else
                {
                    ((LuaFunction)script["LuaCollectionWorkMethods.AddOrUpdateValueInDictionary"]).Call(script["LuaCollectionWorkMethods"], "Item" + i.ToString(), i);
                }
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.GetValueFromDictionary);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                string item = ((LuaFunction)script["LuaCollectionWorkMethods.GetValueFromDictionary"]).Call(script["LuaCollectionWorkMethods"], "BestItem").First().ToString();
                
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.AddStringToList);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                ((LuaFunction)script["LuaCollectionWorkMethods.AddStringToList"]).Call(script["LuaCollectionWorkMethods"], "Item" + i.ToString());
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.ItterateListAndGetCount);
            testCase.StartTimer(TestCaseGroup.NLuaCalled);
            for (int i = 1; i < itterationCount / 10000; i++)
            {
                int.TryParse(((LuaFunction)script["LuaCollectionWorkMethods.ItterateThroughListAndGetCount"]).Call(script["LuaCollectionWorkMethods"]).First().ToString(), out int result);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);
        }
    }
}
