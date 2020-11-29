using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Listener.Models
{
    public class TestResultV2GetModel
    {
        public ConfigurationModel Configuration { get; set; }
        public AutoTestModel AutoTest { get; set; }
        
        public Guid Id { get; set; }
        public Guid ConfigurationId { get; set; }
        public Guid WorkItemVersionId { get; set; }
        public Guid? AutoTestId { get; set; }
        public string Message { get; set; }
        public string Traces { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public Guid? RunByUserId { get; set; }
        public Guid? StoppedByUserId { get; set; }
        public Guid? TestPointId { get; set; }
        public JObject TestPoint { get; set; } = new JObject();
        public Guid TestRunId { get; set; }
        public string Outcome { get; set; }
        public string Comment { get; set; }
        public List<JObject> Links { get; set; } = new List<JObject>();
        public List<JObject> Attachments { get; set; } = new List<JObject>();
        public IDictionary<string, string> Parameters { get; set; }
        public IDictionary<string, string> Properties { get; set; }
        public string AutoTestExternalId { get; internal set; }
    }
}
