using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestIt.WebApi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TestIT.Listener
{
    public class PublicApiClient : IDisposable
    {
        private readonly HttpClient client;

        public PublicApiClient(string host, string secretKey, string userName)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("secretKeyBase64", secretKey);
            client.DefaultRequestHeaders.Add("userName", userName);
            client.BaseAddress = new Uri(host);
        }

        public async Task AddAutoTestToTestCase(int globalId, string testName)
        {
            string url = "api/Public/AddAutoTestToTestCase/" + globalId;
            StringContent content = new JsonContent(new PublicAutoTestModel { AutoTestExternalId = testName });

            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
        }

        public async Task StartTestRun(string testRunId)
        {
            string url = $"api/Public/StartTestRun/{testRunId}";

            HttpResponseMessage responseMessage = await client.PostAsync(url, new JsonContent(string.Empty));
        }

        public async Task<IEnumerable<PublicTestRunModel>> GetAllActiveTestRuns()
        {
            string url = "api/Public/GetTestRuns";
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            string response = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<PublicTestRunModel>>(response);
        }

        public async Task<IEnumerable<PublicTestRunModel>> GetActiveTestRunsByProject(int globalId)
        {
            string url = "api/Public/GetTestRuns/" + globalId;
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            string response = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<PublicTestRunModel>>(response);
        }

        public async Task SetAutoTestResult(PublicTestRunModel publicTestRunModel, IEnumerable<TestResult> testResults)
        {
            string url = "api/Public/SetAutoTestResult";

            foreach (ConfigurationModel config in publicTestRunModel.Configurations)
            {
                foreach (AutoTestModel autoTest in publicTestRunModel.AutoTests)
                {
                    foreach (TestResult testResult in testResults)
                    {
                        if (autoTest.AutotestExternalId == testResult.Name)
                        {
                            PublicTestPointPostModel point = new PublicTestPointPostModel()
                            {
                                TestRunId = publicTestRunModel.TestRunId,
                                ConfigurationGlobalId = config.GlobalId,
                                TestPlanGlobalId = publicTestRunModel.TestPlanGlobalId,
                                Status = "Ready",
                                Outcome = testResult.IsSuccess ? TestResultOutcomes.Passed : TestResultOutcomes.NotPassed,
                                Message = testResult.Messenge,
                                StackTrace = testResult.StackTrace,
                                AutoTestExternalId = testResult.Name,
                                Name = testResult.Name
                            };

                            string jsonContent = JsonConvert.SerializeObject(point);
                            StringContent content = new StringContent(jsonContent, Encoding.UTF8, Application.Json);
                            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
