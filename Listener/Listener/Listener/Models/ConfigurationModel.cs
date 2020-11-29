using Newtonsoft.Json.Linq;
using System;

namespace Listener.Models
{
    public class ConfigurationModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public JObject Capabilities { get; set; }
        public Guid ProjectId { get; set; }
        public long GlobalId { get; set; }
        public string EntityTypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
    }
}
