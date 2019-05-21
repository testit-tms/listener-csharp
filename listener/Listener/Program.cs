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

        public static readonly string Domain = "http://192.168.88.51";
        public static readonly string SecretKey = "QzM3ejdzUUZ2NTdNc0RTRA==";
        public static readonly string UserName = "auto";
        public static readonly string PathToTests = @"D:\autotests\testit";

        static async Task Main(string[] args)
        {
            // Клиент для отправки запросов
            PublicApiClient client = new PublicApiClient(Domain, SecretKey, UserName);
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
                    // Стартуем тестовый запуск, переводим тестовый запуск в статус  In Progress
                    await client.StartTestRun(run.TestRunId.ToString());
                    // Выбираем название тестов (их идеинтификаторы)
                    List<string> testsName = run.AutoTests.Select(at => at.AutotestExternalId).ToList();
                    // Запускам выбранные автотесты
                    runner.RunSelectedTests(PathToTests, testsName);
                    // Получаем результаты автотестов
                    IEnumerable<TestResult> testResults = resultsProvider.GetLastResults(PathToTests);
                    // Проставляем полученные результаты
                    await client.SetAutoTestResult(run, testResults);
                }
                // Задержка
                Thread.Sleep(DeleyInMilliseconds);
            }
        }
    }
}
