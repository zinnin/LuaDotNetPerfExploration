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
            UserData.RegisterType<CollectionWorkMethods>();

            Script script = new Script();
            DynValue itterationCount = DynValue.NewNumber(testItteration);
            script.Globals.Set("GlobalItterationCount", itterationCount);

            script.Globals.Set("DotNetIntWorkMethods", UserData.Create(new IntWorkMethods()));
            script.Globals.Set("DotNetFloatWorkMethods", UserData.Create(new FloatWorkMethods()));
            script.Globals.Set("DotNetStringWorkMethods", UserData.Create(new StringWorkMethods()));
            script.Globals.Set("DotNetCollectionWorkMethods", UserData.Create(new CollectionWorkMethods()));

            RunTestCaseScript(script, TestCaseType.AddInts, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Add(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.AddFloats, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Add(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.SubtractInts, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Subtract(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.SubtractFloats, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Subtract(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.MultiplyInts, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Multiply(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.MultiplyFloats, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Multiply(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.DivideInts, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Divide(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.DivideFloats, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Divide(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsAreEqual, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.IsEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsAreEqual, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.IsEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsAreNotEqual, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.NotEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsAreNotEqual, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.NotEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsGreaterThan, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Greater(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsGreaterThan, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Greater(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.GreaterOrEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.GreaterOrEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsLesserThan, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Lesser(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsLesserThan, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Lesser(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.LesserOrEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.LesserOrEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.Remainder, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Remainder(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.Exponent, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Exponent(i, i+1) end");

            RunTestCaseScript(script, TestCaseType.StringFlip, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = 'testString' result = DotNetStringWorkMethods.StringFlip(result) end");
            RunTestCaseScript(script, TestCaseType.CombineStrings, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local result = DotNetStringWorkMethods.CombineStrings(stringOne, stringTwo) end");
            RunTestCaseScript(script, TestCaseType.FormatStrings, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local result = DotNetStringWorkMethods.CombineStringsFormat(stringOne, stringTwo) end");
            RunTestCaseScript(script, TestCaseType.CombineManyStrings, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' local result = DotNetStringWorkMethods.CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
            RunTestCaseScript(script, TestCaseType.FormatManyStrings, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' local result = DotNetStringWorkMethods.CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
            
            RunTestCaseScript(script, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('BestItem', i) else DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('Item' .. i, i) end end");
            RunTestCaseScript(script, TestCaseType.GetValueFromDictionary, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.GetValueFromDictionary('BestItem') end");
            RunTestCaseScript(script, TestCaseType.AddStringToList, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.AddStringToList('Item' .. i) end");
            RunTestCaseScript(script, TestCaseType.ItterateListAndGetCount, TestCaseGroup.MoonSharpCallingDotNet, "for i=1, GlobalItterationCount / 10000, 1 do local result = DotNetCollectionWorkMethods.ItterateThroughListAndGetCount() end");
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
