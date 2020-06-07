using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestIt.WebApi.Models;

namespace TestIT.Listener
{
    class Program
    {
        private const int DeleyInMilliseconds = 1000;

        public static readonly string Domain = "";
        public static readonly string SecretKey = "";
        public static readonly string PathToTests = @"D:\e2e_tests\";
        public static readonly List<string> Errors = new List<string>();

        static async Task Main()
        {
            // Клиент для отправки запросов
            PublicApiClient client = new PublicApiClient(Domain, SecretKey);
            // Экземпляр класса для запуска автотестов
            TestRunner runner = new TestRunner();
            // Экземпляр класса для получения результатов автотестов
            AutoTestResultsProvider resultsProvider = new AutoTestResultsProvider();
            // Бесконечный цикл для получения и обработки запроса на запуск автотестов
            while (true)
            {
                // Получение всех тестовых запусков
                IEnumerable<PublicTestRunModel> testRuns = await client.GetAllActiveTestRuns();
                // Получение первого необработанного тестового запуска  
                PublicTestRunModel run = testRuns.FirstOrDefault(testRun => testRun.Status == TestRunStates.NotStarted);
                // Если тестовый запуск был найден, обрабатываем его
                if (run != null)
                {
                    try
                    {
                        // Стартуем тестовый запуск, переводим тестовый запуск в статус In Progress
                        await client.StartTestRun(run.TestRunId.ToString());
                        // Выбираем название тестов (их идеинтификаторы)
                        List<string> testsName = run.AutoTests.Select(at => at.ExternalId).ToList();
                        // Запускам выбранные автотесты
                        runner.RunSelectedTests(PathToTests, testsName);
                        // Получаем результаты автотестов
                        IEnumerable<TestResult> testResults = resultsProvider.GetLastResults(PathToTests);
                        // Проставляем полученные результаты
                        await client.SetAutoTestResult(run, testResults);
                        // Завершаем запуск после проставления всех результатов
                        await client.CompleteTestRun(run.TestRunId.ToString());
                    }
                    catch (Exception ex)
                    {
                        Errors.Add(ex.Message);
                        // Завершаем запуск в случае ошибки
                        await client.CompleteTestRun(run.TestRunId.ToString());
                    }
                }
                // Задержка
                Thread.Sleep(DeleyInMilliseconds);
            }
        }
    }
}
