using Listener.Infrastructure;
using Listener.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Listener
{
    public class ApiClient : IDisposable
    {
        private readonly HttpClient client;

        public ApiClient(string host, string secretKey)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(host);
            client.DefaultRequestHeaders.Add("secretKeyBase64", secretKey);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("PrivateToken", secretKey);
        }

        public async Task StartTestRun(string testRunId)
        {
            string url = $"api/v2/testRuns/{testRunId}/start";
            await client.PostAsync(url, new JsonContent(string.Empty));
        }

        public async Task CompleteTestRun(string testRunId)
        {
            string url = $"api/v2/testRuns/{testRunId}/complete";
            await client.PostAsync(url, new JsonContent(string.Empty));
        }

        public async Task<List<PublicTestRunModel>> GetActiveTestRunsByProject(Guid projectId)
        {
            string url = $"api/v2/projects/{projectId}/testRuns?NotStarted=true&InProgress=true";
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            string response = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<PublicTestRunModel>>(response);
        }

        public async Task SetAutoTestResult(PublicTestRunModel publicTestRunModel, IEnumerable<TestResult> testResults, Guid configId)
        {
            string url = $"/api/v2/testRuns/{publicTestRunModel.Id}/testResults";

            foreach (AutoTestModel autoTest in publicTestRunModel.TestResults.Select(a => a.AutoTest))
            {
                string autotestExternalId = autoTest.ExternalId;
                foreach (TestResult testResult in testResults)
                {
                    if (testResult.Name == autoTest.ExternalId)
                    {
                        var points = new List<TestResultV2GetModel>()
                        {
                           new TestResultV2GetModel()
                           {
                                ConfigurationId = configId,
                                Outcome = testResult.Result.ToString(),
                                Message = testResult.Messenge,
                                Traces = testResult.StackTrace,
                                AutoTestExternalId = autotestExternalId
                           }
                        };

                        await client.PostAsync(url, new JsonContent(points));
                        break;
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
