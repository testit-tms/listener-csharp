using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TestIt.WebApi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TestIT.Listener
{
    public class PublicApiClient : IDisposable
    {
        private readonly HttpClient client;

        public PublicApiClient(string host, string secretKey)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("secretKeyBase64", secretKey);
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("PrivateToken", secretKey);
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
            string url = "/api/v2/testResults";
            foreach (ConfigurationModel config in publicTestRunModel.Configurations)
            {
                foreach (AutoTestModel autoTest in publicTestRunModel.AutoTests)
                {
                    string autotestExternalId = autoTest.ExternalId;
                    foreach (TestResult testResult in testResults)
                    {
                        if (testResult.Name.Contains(autoTest.ExternalId))
                        {
                            PublicTestPointPostModel point = new PublicTestPointPostModel()
                            {
                                TestRunId = publicTestRunModel.TestRunId,
                                TestPlanGlobalId = publicTestRunModel.TestPlanGlobalId,
                                Outcome = testResult.Result.ToString(),
                                Message = testResult.Messenge,
                                StackTrace = testResult.StackTrace,
                                AutoTestExternalId = autotestExternalId,
                                ConfigurationGlobalId = config.GlobalId,

                            };

                            string jsonContent = JsonConvert.SerializeObject(point);
                            StringContent content = new StringContent(jsonContent, Encoding.UTF8, Application.Json);
                            HttpResponseMessage responseMessage = await client.PostAsync(url, content);
                        }
                    }
                }

            }
        }
        public async Task CompleteTestRun(string testRunId)
        {
            string url = $"api/Public/CompleteTestRun/{testRunId}";
            HttpResponseMessage responseMessage = await client.PostAsync(url, new JsonContent(string.Empty));
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
