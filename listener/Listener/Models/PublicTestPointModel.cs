using System.Collections.Generic;

namespace TestIt.WebApi.Models
{
    public class PublicTestPointModel 
    {
        public long ConfigurationGlobalId { get; set; }
        public IEnumerable<string> AutoTestIds { get; set; }
    }
}
