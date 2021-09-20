using static PerfTester.PerfCollector;

namespace PerfTester.LibraryImplementations
{
    class DotNetTests
    {
        public DotNetTests(int itterationCount)
        {
            IntWorkMethods workMethods = new IntWorkMethods();

            TestCase testCase = GetTestCase("AddInts");
            testCase.StartTimer(TestCaseGroup.PureDotNet);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = workMethods.Add(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.PureDotNet);

            testCase = GetTestCase("SubtractInts");
            testCase.StartTimer(TestCaseGroup.PureDotNet);
            for (int i = 1; i < itterationCount; i++)
            {
                int result = workMethods.Subtract(i, i + 1);
            }
            testCase.EndTimer(TestCaseGroup.PureDotNet);

            StringWorkMethods stringWorkMethods = new StringWorkMethods();
            testCase = GetTestCase("StringFlip");
            testCase.StartTimer(TestCaseGroup.PureDotNet);
            string testString = "testString";
            for (int i = 1; i < itterationCount; i++)
            {
                testString = stringWorkMethods.StringFlip(testString);
            }
            testCase.EndTimer(TestCaseGroup.PureDotNet);
        }
    }
}
