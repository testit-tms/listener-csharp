using Listener.Models.Enums;

namespace TestIT.Listener
{
    public class TestResult
    {
        public string Name { get; set; }
        public TestResultOutcomes Result { get; set; }
        public string Messenge { get; set; }
        public string StackTrace { get; set; }
    }
}
