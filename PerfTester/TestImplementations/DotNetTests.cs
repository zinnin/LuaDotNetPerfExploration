using static PerfTester.PerfCollector;

namespace PerfTester.LibraryImplementations
{
    class DotNetTests
    {
        public DotNetTests(int itterationCount)
        {
            IntWorkMethods workMethods = new IntWorkMethods();
            FloatWorkMethods floatMethods = new FloatWorkMethods();
            StringWorkMethods stringWorkMethods = new StringWorkMethods();
            CollectionWorkMethods collectionMethods = new CollectionWorkMethods();

            TestCase testCase = GetTestCase(TestCaseType.AddInts);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = workMethods.Add(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.AddFloats);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                float result = floatMethods.Add(i, i + 1.5f);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.SubtractInts);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = workMethods.Subtract(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.SubtractFloats);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                float result = floatMethods.Subtract(i, i + 1.5f);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.MultiplyInts);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = workMethods.Multiply(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.MultiplyFloats);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                float result = floatMethods.Multiply(i, i + 1.5f);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.DivideInts);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = workMethods.Divide(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.DivideFloats);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                float result = floatMethods.Divide(i, i + 1.5f);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.IntsAreEqual);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = workMethods.IsEqual(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.FloatsAreEqual);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = floatMethods.IsEqual(i, i + 1.5f);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.IntsAreNotEqual);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = workMethods.NotEqual(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.FloatsAreNotEqual);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = floatMethods.NotEqual(i, i + 1.5f);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.IntsGreaterThan);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = workMethods.Greater(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.FloatsGreaterThan);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = floatMethods.Greater(i, i + 1.5f);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.IntsGreaterOrEqualTo);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = workMethods.GreaterOrEqual(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.FloatsGreaterOrEqualTo);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = floatMethods.GreaterOrEqual(i, i + 1.5f);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.IntsLesserThan);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = workMethods.Lesser(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.FloatsLesserThan);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = floatMethods.Lesser(i, i + 1.5f);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.IntsLesserOrEqualTo);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = workMethods.LesserOrEqual(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.FloatsLesserOrEqualTo);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                bool result = floatMethods.LesserOrEqual(i, i + 1.5f);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.Remainder);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = workMethods.Remainder(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.Exponent);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = workMethods.Exponent(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);
            
            testCase = GetTestCase(TestCaseType.StringFlip);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            string testString = "testString";
            for (int i = 1; i < itterationCount; i++)
            {
                testString = stringWorkMethods.StringFlip(testString);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.CombineStrings);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            string stringOne = "stringOne";
            string stringTwo = "stringTwo";
            for (int i = 1; i < itterationCount; i++)
            {
                string result = stringWorkMethods.CombineStrings(stringOne, stringTwo);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.FormatStrings);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                string result = stringWorkMethods.CombineStringsFormat(stringOne, stringTwo);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            string stringThree = "stringThree";
            string stringFour = "stringFour";
            string stringFive = "stringFive";
            string stringSix = "stringSix";
            string stringSeven = "stringSeven";
            string stringEight = "stringEight";
            string stringNine = "stringNine";
            string stringTen = "stringTen";

            testCase = GetTestCase(TestCaseType.CombineManyStrings);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                string result = stringWorkMethods.CombineManyStrings(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.FormatManyStrings);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                string result = stringWorkMethods.CombineManyStringsFormat(stringOne, stringTwo, stringThree, stringFour, stringFive, stringSix, stringSeven, stringEight, stringNine, stringTen);
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.AddOrUpdateValueInDictionary);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                if (i % 5 == 0)
                {
                    collectionMethods.AddOrUpdateValueInDictionary("BestItem", i);
                }
                else
                {
                    collectionMethods.AddOrUpdateValueInDictionary("Item" + i.ToString(), i);
                }
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.GetValueFromDictionary);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                var item = collectionMethods.GetValueFromDictionary("BestItem");
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.AddStringToList);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount; i++)
            {
                collectionMethods.AddStringToList("Item" + i.ToString());
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);

            testCase = GetTestCase(TestCaseType.ItterateListAndGetCount);
            testCase.StartTimer(TestCaseGroup.DotNetPure);
            for (int i = 1; i < itterationCount / 10000; i++)
            {
                int count = collectionMethods.ItterateThroughListAndGetCount();
            }
            testCase.EndTimer(TestCaseGroup.DotNetPure);
        }
    }
}
