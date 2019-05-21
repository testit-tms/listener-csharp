using System;
using System.Collections.Generic;

namespace TestIt.WebApi.Models
{
    public class PublicTestRunModel
    {
        public Guid TestRunId { get; set; }        
        public long TestPlanGlobalId { get; set; }
        public string ProductName { get; set; }
        public string Build { get; set; }
        public ICollection<ConfigurationModel> Configurations { get; set; } = new List<ConfigurationModel>();
        public ICollection<AutoTestModel> AutoTests { get; set; } = new List<AutoTestModel>();
        public ICollection<PublicTestPointModel> TestPoints { get; set; } = new List<PublicTestPointModel>();
        public string Status { get; set; }
    }
}
