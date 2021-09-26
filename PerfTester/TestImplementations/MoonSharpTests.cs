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
            RuMoonSharpCalledFromDotNetTests(testItteration);
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

            RunTestCaseScript(script, TestCaseType.AddInts, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Add(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.AddFloats, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Add(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.SubtractInts, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Subtract(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.SubtractFloats, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Subtract(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.MultiplyInts, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Multiply(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.MultiplyFloats, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Multiply(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.DivideInts, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Divide(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.DivideFloats, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Divide(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsAreEqual, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.IsEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsAreEqual, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.IsEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsAreNotEqual, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.NotEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsAreNotEqual, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.NotEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsGreaterThan, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Greater(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsGreaterThan, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Greater(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.GreaterOrEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.GreaterOrEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsLesserThan, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Lesser(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsLesserThan, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.Lesser(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.LesserOrEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetFloatWorkMethods.LesserOrEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.Remainder, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Remainder(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.Exponent, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetIntWorkMethods.Exponent(i, i+1) end");

            RunTestCaseScript(script, TestCaseType.StringFlip, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = 'testString' result = DotNetStringWorkMethods:StringFlip(result) end");
            RunTestCaseScript(script, TestCaseType.CombineStrings, TestCaseGroup.MoonSharpCalling, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineStrings(stringOne, stringTwo) end");
            RunTestCaseScript(script, TestCaseType.FormatStrings, TestCaseGroup.MoonSharpCalling, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineStringsFormat(stringOne, stringTwo) end");
            RunTestCaseScript(script, TestCaseType.CombineManyStrings, TestCaseGroup.MoonSharpCalling, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
            RunTestCaseScript(script, TestCaseType.FormatManyStrings, TestCaseGroup.MoonSharpCalling, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = DotNetStringWorkMethods:CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");

            RunTestCaseScript(script, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('BestItem', i) else DotNetCollectionWorkMethods.AddOrUpdateValueInDictionary('Item' .. i, i) end end");
            RunTestCaseScript(script, TestCaseType.GetValueFromDictionary, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.GetValueFromDictionary('BestItem') end");
            RunTestCaseScript(script, TestCaseType.AddStringToList, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount, 1 do local result = DotNetCollectionWorkMethods.AddStringToList('Item' .. i) end");
            RunTestCaseScript(script, TestCaseType.ItterateListAndGetCount, TestCaseGroup.MoonSharpCalling, "for i=1, GlobalItterationCount / 10000, 1 do local result = DotNetCollectionWorkMethods.ItterateThroughListAndGetCount() end");
        }

        public void RunPureLuaTest(int testItteration)
        {
            string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
            Script script = new Script();
            script.DoString(File.ReadAllText(workMethodsScript));

            DynValue itterationCount = DynValue.NewNumber(testItteration);
            script.Globals.Set("GlobalItterationCount", itterationCount);

            RunTestCaseScript(script, TestCaseType.AddInts, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Add(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.AddFloats, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Add(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.SubtractInts, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Subtract(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.SubtractFloats, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Subtract(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.MultiplyInts, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Multiply(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.MultiplyFloats, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Multiply(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.DivideInts, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Divide(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.DivideFloats, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Divide(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsAreEqual, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:IsEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsAreEqual, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:IsEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsAreNotEqual, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:NotEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsAreNotEqual, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:NotEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsGreaterThan, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Greater(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsGreaterThan, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Greater(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsGreaterOrEqualTo, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:GreaterOrEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsGreaterOrEqualTo, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:GreaterOrEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsLesserThan, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Lesser(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsLesserThan, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Lesser(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.IntsLesserOrEqualTo, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:LesserOrEqual(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.FloatsLesserOrEqualTo, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:LesserOrEqual(i, i+1.5) end");
            RunTestCaseScript(script, TestCaseType.Remainder, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Remainder(i, i+1) end");
            RunTestCaseScript(script, TestCaseType.Exponent, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaIntWorkMethods:Exponent(i, i+1) end");

            RunTestCaseScript(script, TestCaseType.StringFlip, TestCaseGroup.MoonSharpPure, "local result = 'testString' for i=1, GlobalItterationCount, 1 do result = LuaStringWorkMethods:StringFlip(result) end");
            RunTestCaseScript(script, TestCaseType.CombineStrings, TestCaseGroup.MoonSharpPure, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = LuaStringWorkMethods:CombineStrings(stringOne, stringTwo) end");
            RunTestCaseScript(script, TestCaseType.FormatStrings, TestCaseGroup.MoonSharpPure, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' for i=1, GlobalItterationCount, 1 do local result = LuaStringWorkMethods:CombineStringsFormat(stringOne, stringTwo) end");
            RunTestCaseScript(script, TestCaseType.CombineManyStrings, TestCaseGroup.MoonSharpPure, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = LuaStringWorkMethods:CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");
            RunTestCaseScript(script, TestCaseType.FormatManyStrings, TestCaseGroup.MoonSharpPure, "local stringOne = 'stringOne' local stringTwo = 'stringTwo' local stringThree = 'stringThree' local stringFour = 'stringFour' local stringFive = 'stringFive' local stringSix = 'stringSix' local stringSeven = 'stringSeven' local stringEight = 'stringEight' local stringNine = 'stringNine' local stringTen = 'stringTen' for i=1, GlobalItterationCount, 1 do local result = LuaStringWorkMethods:CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen) end");

            RunTestCaseScript(script, TestCaseType.AddOrUpdateValueInDictionary, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do if (i % 5 == 0) then LuaCollectionWorkMethods:AddOrUpdateValueInDictionary('BestItem', i) else LuaCollectionWorkMethods:AddOrUpdateValueInDictionary('Item' .. i, i) end end");
            RunTestCaseScript(script, TestCaseType.GetValueFromDictionary, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaCollectionWorkMethods:GetValueFromDictionary('BestItem') end");
            RunTestCaseScript(script, TestCaseType.AddStringToList, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount, 1 do local result = LuaCollectionWorkMethods:AddStringToList('Item' .. i) end");
            RunTestCaseScript(script, TestCaseType.ItterateListAndGetCount, TestCaseGroup.MoonSharpPure, "for i=1, GlobalItterationCount / 10000, 1 do local result = LuaCollectionWorkMethods:ItterateThroughListAndGetCount() end");
        }

        public void RuMoonSharpCalledFromDotNetTests(int itterationCount)
        {
            string workMethodsScript = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\WorkMethods.lua";
            Script script = new Script();
            script.DoString(File.ReadAllText(workMethodsScript));
            DynValue intScripts = script.Globals.Get("LuaIntWorkMethods");
            DynValue stringScripts = script.Globals.Get("LuaStringWorkMethods");
            DynValue collectionScripts = script.Globals.Get("LuaCollectionWorkMethods");

            TestCase testCase = GetTestCase(TestCaseType.AddInts);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = (int)script.Call(intScripts.Table.Get("Add"), intScripts, i, i + 1).Number;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.AddFloats);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                float result = (float)script.Call(intScripts.Table.Get("Add"), intScripts, i, i + 1.5F).Number;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.SubtractInts);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = (int)script.Call(intScripts.Table.Get("Subtract"), intScripts, i, i + 1).Number;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.SubtractFloats);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                float result = (float)script.Call(intScripts.Table.Get("Subtract"), intScripts, i, i + 1.5F).Number;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.MultiplyInts);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = (int)script.Call(intScripts.Table.Get("Multiply"), intScripts, i, i + 1).Number;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.MultiplyFloats);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                float result = (float)script.Call(intScripts.Table.Get("Multiply"), intScripts, i, i + 1.5F).Number;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.DivideInts);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = (int)script.Call(intScripts.Table.Get("Divide"), intScripts, i, i + 1).Number;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.DivideFloats);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                float result = (float)script.Call(intScripts.Table.Get("Divide"), intScripts, i, i + 1.5F).Number;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.Remainder);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = (int)script.Call(intScripts.Table.Get("Remainder"), intScripts, i, i + 1).Number;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.Exponent);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = (int)script.Call(intScripts.Table.Get("Exponent"), intScripts, i, i + 1.5F).Number;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.IntsAreEqual);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = script.Call(intScripts.Table.Get("IsEqual"), intScripts, i, i + 1).Boolean;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.FloatsAreEqual);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = script.Call(intScripts.Table.Get("IsEqual"), intScripts, i, i + 1.5F).Boolean;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.IntsAreNotEqual);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = script.Call(intScripts.Table.Get("NotEqual"), intScripts, i, i + 1).Boolean;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.FloatsAreNotEqual);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = script.Call(intScripts.Table.Get("NotEqual"), intScripts, i, i + 1.5F).Boolean;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.IntsGreaterThan);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = script.Call(intScripts.Table.Get("Greater"), intScripts, i, i + 1).Boolean;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.FloatsGreaterThan);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = script.Call(intScripts.Table.Get("Greater"), intScripts, i, i + 1.5F).Boolean;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.IntsGreaterOrEqualTo);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = script.Call(intScripts.Table.Get("GreaterOrEqual"), intScripts, i, i + 1).Boolean;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.FloatsGreaterOrEqualTo);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = script.Call(intScripts.Table.Get("GreaterOrEqual"), intScripts, i, i + 1.5F).Boolean;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.IntsLesserThan);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = script.Call(intScripts.Table.Get("Lesser"), intScripts, i, i + 1).Boolean;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.FloatsLesserThan);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = script.Call(intScripts.Table.Get("Lesser"), intScripts, i, i + 1.5F).Boolean;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.IntsLesserOrEqualTo);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = script.Call(intScripts.Table.Get("LesserOrEqual"), intScripts, i, i + 1).Boolean;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.FloatsLesserOrEqualTo);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = script.Call(intScripts.Table.Get("LesserOrEqual"), intScripts, i, i + 1.5F).Boolean;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.StringFlip);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            string testString = "testString";
            for (int i = 1; i < itterationCount; i++)
            {
                testString = script.Call(stringScripts.Table.Get("StringFlip"), stringScripts, testString).String;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.CombineStrings);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            string stringOne = "stringOne";
            string stringTwo = "stringTwo";
            for (int i = 1; i < itterationCount; i++)
            {
                string result = script.Call(stringScripts.Table.Get("CombineStrings"), stringScripts, stringOne, stringTwo).String;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.FormatStrings);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                string result = script.Call(stringScripts.Table.Get("CombineStringsFormat"), stringScripts, stringOne, stringTwo).String;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            string stringThree = "stringThree";
            string stringFour = "stringFour";
            string stringFive = "stringFive";
            string stringSix = "stringSix";
            string stringSeven = "stringSeven";
            string stringEight = "stringEight";
            string stringNine = "stringNine";
            string stringTen = "stringTen";

            testCase = GetTestCase(TestCaseType.CombineManyStrings);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                string result = script.Call(stringScripts.Table.Get("CombineManyStrings"), stringScripts, stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen).String;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.FormatManyStrings);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                string result = script.Call(stringScripts.Table.Get("CombineManyStringsFormat"), stringScripts, stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen).String;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.AddOrUpdateValueInDictionary);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                if (i % 5 == 0)
                {
                    script.Call(collectionScripts.Table.Get("AddOrUpdateValueInDictionary"), collectionScripts, "BestItem", i);
                }
                else
                {
                    script.Call(collectionScripts.Table.Get("AddOrUpdateValueInDictionary"), collectionScripts, "Item" + i.ToString(), i);
                }
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.GetValueFromDictionary);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                string item = script.Call(collectionScripts.Table.Get("GetValueFromDictionary"), collectionScripts, "BestItem").String;

            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.AddStringToList);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount; i++)
            {
                script.Call(collectionScripts.Table.Get("AddStringToList"), collectionScripts, "Item" + i.ToString());
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);

            testCase = GetTestCase(TestCaseType.ItterateListAndGetCount);
            testCase.StartTimer(TestCaseGroup.MoonSharpCalled);
            for (int i = 1; i < itterationCount / 10000; i++)
            {
                int result = (int)script.Call(collectionScripts.Table.Get("ItterateThroughListAndGetCount"), collectionScripts).Number;
            }
            testCase.EndTimer(TestCaseGroup.MoonSharpCalled);
        }
    }
}
