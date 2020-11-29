using System.Collections.Generic;
using System.IO;

namespace Listener.Models
{
    public class TestResult
    {
        public string Name { get; set; }
        public TestResultOutcomes Result { get; set; }
        public string Messenge { get; set; }
        public string StackTrace { get; set; }
        public List<FileInfo> Attachments { get; set; } = new List<FileInfo>();
    }
}
