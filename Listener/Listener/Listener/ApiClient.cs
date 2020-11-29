using Listener.Infrastructure;
using Listener.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            string url = $"api/Public/StartTestRun/{testRunId}";
            await client.PostAsync(url, new JsonContent(string.Empty));
        }

        public async Task CompleteTestRun(string testRunId)
        {
            string url = $"api/Public/CompleteTestRun/{testRunId}";
            await client.PostAsync(url, new JsonContent(string.Empty));
        }

        public async Task<Response<PublicTestRunModel[]>> GetAllActiveTestRuns()
        {
            string url = "api/Public/GetTestRuns";
            HttpResponseMessage response = await client.GetAsync(url);
            return await Response.CreateValueResponse<PublicTestRunModel[]>(response);
        }

        public async Task SetAutoTestResult(PublicTestRunModel publicTestRunModel, IEnumerable<TestResult> testResults, Guid configId)
        {
            string url = $"/api/v2/testRuns/{publicTestRunModel.TestRunId}/testResults";

            foreach (AutoTestModel autoTest in publicTestRunModel.AutoTests)
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

        public async Task UploadFileToResult(FileInfo fileInfo, string resultId, string contentType = "multipart/form-data")
        {
            var url = $"api/Public/testResults/{resultId}/attachments";
            var filePath = fileInfo.FullName;
            var fileName = fileInfo.Name;
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var content = new FileContent(fileName, contentType, stream);
                var response = await client.PostAsync(url, content);
            }
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
