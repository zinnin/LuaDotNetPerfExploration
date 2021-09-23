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
            script["DotNetFloatWorkMethods"] = new FloatWorkMethods();
            script["DotNetStringWorkMethods"] = new StringWorkMethods();
            script["DotNetCollectionWorkMethods"] = new CollectionWorkMethods();

            RunTestCaseScript(script, TestCaseType.AddInts, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Add(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.AddFloats, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:Add(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.SubtractInts, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Subtract(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.SubtractFloats, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:Subtract(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.MultiplyInts, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Multiply(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.MultiplyFloats, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:Multiply(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.DivideInts, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Divide(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.DivideFloats, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:Divide(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsAreEqual, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:IsEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsAreEqual, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:IsEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsAreNotEqual, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:NotEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsAreNotEqual, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:NotEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsGreaterThan, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Greater(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsGreaterThan, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:Greater(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:GreaterOrEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:GreaterOrEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsLesserThan, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Lesser(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsLesserThan, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:Lesser(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:LesserOrEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods:LesserOrEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.Remainder, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Remainder(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.Exponent, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods:Exponent(i, i+1) end");

            RunTestCaseScript(script, TestCaseType.StringFlip, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = 'testString' result = DotNetStringWorkMethods:StringFlip(result) end");
            RunTestCaseScript(script, TestCaseType.CombineStrings, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local result = DotNetStringWorkMethods:CombineStrings(stringOne, stringTwo) end");
            RunTestCaseScript(script, TestCaseType.FormatStrings, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local result = DotNetStringWorkMethods:CombineStringsFormat(stringOne, stringTwo) end");
            RunTestCaseScript(script, TestCaseType.CombineManyStrings, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' local result = DotNetStringWorkMethods:CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
            RunTestCaseScript(script, TestCaseType.FormatManyStrings, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' local result = DotNetStringWorkMethods:CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");

            RunTestCaseScript(script, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then DotNetCollectionWorkMethods:AddOrUpdateValueInDictionary('BestItem', i) else DotNetCollectionWorkMethods:AddOrUpdateValueInDictionary('Item' .. i, i) end end");
            RunTestCaseScript(script, TestCaseType.GetValueFromDictionary, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods:GetValueFromDictionary('BestItem') end");
            RunTestCaseScript(script, TestCaseType.AddStringToList, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods:AddStringToList('Item' .. i) end");
            RunTestCaseScript(script, TestCaseType.ItterateListAndGetCount, TestCaseGroup.NLuacallingDotNet, "for i=1, GlobalItterationCount / 10000, 1 do local result = DotNetCollectionWorkMethods:ItterateThroughListAndGetCount() end");
        }

        private void RunTestCaseScript(Lua script, TestCaseType caseType, TestCaseGroup group, string runScript)
        {
            TestCase testCase = GetTestCase(caseType);
            testCase.StartTimer(group);
            script.DoString(runScript);
            testCase.EndTimer(group);
        }

        public void RunPureLuaTest(int testItteration)
        {
            string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
            Lua script = new Lua();
            script["GlobalItterationCount"] = testItteration;
            script.DoString(File.ReadAllText(workMethodsScript));

            TestCase testCase = GetTestCase(TestCaseType.AddInts);
            testCase.StartTimer(TestCaseGroup.PureNLua);
            script.DoString("for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods.Add(i, i+1) end");
            testCase.EndTimer(TestCaseGroup.PureNLua);

            testCase = GetTestCase(TestCaseType.SubtractInts);
            testCase.StartTimer(TestCaseGroup.PureNLua);
            script.DoString("for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods.Subtract(i, i+1) end");
            testCase.EndTimer(TestCaseGroup.PureNLua);

            testCase = GetTestCase(TestCaseType.StringFlip);
            testCase.StartTimer(TestCaseGroup.PureNLua);
            script.DoString("local testString = 'testString' for i=1, GlobalItterationCount, 1 do testString = LuaStringWorkMethods.StringFlip(testString) end");
            testCase.EndTimer(TestCaseGroup.PureNLua);
        }

        public void RunLuaCalledFromDotNetTests(int itterationCount)
        {
            string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
            Lua script = new Lua();
            script.DoString(File.ReadAllText(workMethodsScript));

            TestCase testCase = GetTestCase(TestCaseType.AddInts);
            testCase.StartTimer(TestCaseGroup.DotNetCallingNLua);
            for (int i = 1; i < itterationCount; i++)
            {
                var scriptFunc = script["LuaIntWorkMethods.Add"] as LuaFunction;
                var res = (System.Int64)scriptFunc.Call(i, i + 1).First();
            }
            testCase.EndTimer(TestCaseGroup.DotNetCallingNLua);

            testCase = GetTestCase(TestCaseType.SubtractInts);
            testCase.StartTimer(TestCaseGroup.DotNetCallingNLua);
            for (int i = 1; i < itterationCount; i++)
            {
                var scriptFunc = script["LuaIntWorkMethods.Subtract"] as LuaFunction;
                var res = (System.Int64)scriptFunc.Call(i, i + 1).First();
            }
            testCase.EndTimer(TestCaseGroup.DotNetCallingNLua);

            testCase = GetTestCase(TestCaseType.StringFlip);
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
