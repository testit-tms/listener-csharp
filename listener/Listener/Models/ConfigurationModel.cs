using System;

namespace TestIt.WebApi.Models
{
    public class ConfigurationModel : ConfigurationPutModel
    {
        public string EntityTypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
    }
}
