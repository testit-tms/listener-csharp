using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TestIT.Listener
{
    public class TestRunner
    {
        public void RunSelectedTests(string path, List<string> testsName)
        {
            string filter = string.Empty;

            if (testsName.Count == 1)
            {
                filter = testsName[0];
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("\"");
                foreach (var testName in testsName)
                {
                    stringBuilder.Append($"{testName}|");
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append("\"");

                filter = stringBuilder.ToString();
            }

            string arguments = $"test {path} --filter {filter} --logger:trx -r ./build/reports/";

            Process process = Process.Start($"dotnet", arguments);

            process.WaitForExit();
        }
    }
}
