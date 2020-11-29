using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.IO;
using Listener.Models;

namespace Listener
{
    class Program
    {
        private const int DelayInMilliseconds = 5000;

        public static readonly string domain = "https://demo.testit.software/";
        public static readonly string privateToken = "OGl6MnVTNzNXQXEyQm9RTUNo";
        public static readonly Guid configurationId = new Guid("15dbb164-c1aa-4cbf-830c-8c01ae14f4fb");
        public static readonly ApiClient client = new ApiClient(domain, privateToken);
        public static readonly AutoTestResultsProvider resultsProvider = new AutoTestResultsProvider();

        public static readonly string PathToTests =
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName + "\\TestAutomationProject";
        private static readonly string PartialPathToResult = @"\reports\";

        static async Task Main()
        {
            while (true)
            {
                PublicTestRunModel[] testRuns = (await client.GetAllActiveTestRuns()).Value;
                PublicTestRunModel run = testRuns.FirstOrDefault();

                if (run != null)
                {
                    try
                    {
                        Console.WriteLine("Тест-ран в работе. " + run.TestRunId);
                        await client.StartTestRun(run.TestRunId.ToString());

                        List<PublicTestPointModel> testPoints = run.TestPoints.ToList();
                        List<AutoTestModel> testsNames = GetTestNamesForTestPointsOfNeededConfig(testPoints, run);

                        new TestRunner().RunSelectedTests(PathToTests, PartialPathToResult, testsNames);
                        IEnumerable<TestResult> testResults = resultsProvider.GetLastResults(PathToTests + PartialPathToResult);
                        await client.SetAutoTestResult(run, testResults, configurationId);

                        await client.CompleteTestRun(run.TestRunId.ToString());
                        Console.WriteLine("Тест-ран завершён. " + run.TestRunId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Возникла ошибка: " + ex.Message);
                        await client.CompleteTestRun(run.TestRunId.ToString());
                    }
                }
                else
                    Console.WriteLine("Ожидается тест-ран");
                Thread.Sleep(DelayInMilliseconds);
            }
        }

        private static List<AutoTestModel> GetTestNamesForTestPointsOfNeededConfig(List<PublicTestPointModel> testPoints,
            PublicTestRunModel currentTestRun)
        {
            var autoTestIds = new HashSet<Guid>();
            foreach (PublicTestPointModel point in testPoints)
                foreach (var testId in point.AutoTestIds)
                    autoTestIds.Add(testId);

            return currentTestRun.AutoTests.Where(t => autoTestIds.Contains((Guid)t.Id)).ToList();
        }
    }
}
