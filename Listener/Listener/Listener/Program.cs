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
        public static readonly Guid projectId = new Guid("5236eb3f-7c05-46f9-a609-dc0278896464");
        public static readonly ApiClient client = new ApiClient(domain, privateToken);
        public static readonly AutoTestResultsProvider resultsProvider = new AutoTestResultsProvider();
        public static readonly TestRunner runner = new TestRunner();  

        public static readonly string PathToTests =
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName + "\\TestAutomationProject";
        private static readonly string PartialPathToResult = @"\reports\";

        static async Task Main()
        {
            while (true)
            {
                List<PublicTestRunModel> testRuns = (await client.GetActiveTestRunsByProject(projectId));
                PublicTestRunModel run = testRuns.FirstOrDefault();

                if (run != null)
                {
                    try
                    {
                        Console.WriteLine("Тест-ран в работе. " + run.Id);
                        await client.StartTestRun(run.Id.ToString());

                        List<AutoTestModel> autotests = run.TestResults.Select(a => a.AutoTest).ToList();
                        runner.RunSelectedTests(PathToTests, PartialPathToResult, autotests);
                        IEnumerable<TestResult> testResults = resultsProvider.GetLastResults(PathToTests + PartialPathToResult);
                        await client.SetAutoTestResult(run, testResults, configurationId);

                        await client.CompleteTestRun(run.Id.ToString());
                        Console.WriteLine("Тест-ран завершён. " + run.Id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Возникла ошибка: " + ex.Message);
                        await client.CompleteTestRun(run.Id.ToString());
                    }
                }
                else
                    Console.WriteLine("Ожидается тест-ран");
                Thread.Sleep(DelayInMilliseconds);
            }
        }
    }
}
