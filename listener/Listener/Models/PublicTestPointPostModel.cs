using System;

namespace TestIt.WebApi.Models
{
    public class PublicTestPointPostModel
    {
        public Guid TestRunId { get; set; }
        public long TestPlanGlobalId { get; set; }
        public string AutoTestExternalId { get; set; }
        public long ConfigurationGlobalId { get; set; }
        public string Status { get; set; }
        public string Outcome { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Name { get; set; }
    }
}
