using System;
using System.Collections.Generic;

namespace Listener.Models
{
    public class PublicTestRunModel
    {
        public long TestPlanGlobalId { get; set; }

        public string ProductName { get; set; }

        public string Build { get; set; }

        public string Status { get; set; }

        public DateTime? StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string StateName { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? TestPlanId { get; set; }
        public List<TestResultV2GetModel> TestResults { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
