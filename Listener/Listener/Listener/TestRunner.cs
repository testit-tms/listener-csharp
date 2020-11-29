using Listener.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Listener
{
    public class TestRunner
    {
        public void RunSelectedTests(string path, string pathToReport, List<AutoTestModel> autoTests)
        {
            string filter;

            if (autoTests.Count == 1)
            {
                filter = autoTests[0].ExternalId;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("\"");
                foreach (AutoTestModel autoTest in autoTests)
                {
                    stringBuilder.Append(autoTest.ExternalId).Append("|");
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append("\"");

                filter = stringBuilder.ToString();
            }

            string arguments = $"test {path} --filter {filter} --logger:trx -r {path}{pathToReport}";

            Process process = Process.Start($"dotnet", arguments);

            process.WaitForExit();
        }
    }
}
