using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestIT.Listener
{
    public class AutoTestResultsProvider
    {
        private readonly string pathToResult = @"\TestIT.Tests\build\reports\";

        public IEnumerable<TestResult> GetLastResults(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path + pathToResult);

            FileInfo file = directory.GetFiles()
                                    .OrderByDescending(f => f.LastWriteTime)
                                    .FirstOrDefault();

            using (Stream stream = file.OpenRead())
            {
                return TestResultConverter.ConvertFromXml(stream);
            }
        }
    }
}
