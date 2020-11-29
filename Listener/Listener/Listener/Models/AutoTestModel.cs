using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Listener.Models
{
    public class AutoTestModel
    {
        public Guid? Id { get; set; } = null;
        public bool IsDeleted { get; set; }
        public string ExternalId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
        public List<JObject> Links { get; set; } = new List<JObject>();
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Classname { get; set; }
        public List<JObject> Steps { get; set; } = new List<JObject>();
        public List<JObject> Setup { get; set; } = new List<JObject>();
        public List<JObject> Teardown { get; set; } = new List<JObject>();
        public long GlobalId { get; set; }
        public List<JObject> Labels { get; set; } = new List<JObject>();
    }
}
