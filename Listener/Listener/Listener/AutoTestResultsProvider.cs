using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using Listener.Models;

namespace Listener
{
    public class AutoTestResultsProvider
    {
        public IEnumerable<TestResult> GetLastResults(string pathToResult)
        {
            DirectoryInfo directory = new DirectoryInfo(pathToResult);

            FileInfo file = directory.GetFiles()
                                    .OrderByDescending(f => f.LastWriteTime)
                                    .First();

            using (Stream stream = file.OpenRead())
            {
                return TestResultConverter.ConvertFromXml(stream);
            }
        }
    }
}
