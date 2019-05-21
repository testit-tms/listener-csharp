using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace TestIt.WebApi.Models
{
    public class ConfigurationPutModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public JObject Capabilities { get; set; }
        public Guid ProjectId { get; set; }
        public long GlobalId { get; set; }
    }
}
