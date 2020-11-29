using System;
using System.Collections.Generic;

namespace Listener.Models
{
    public class PublicTestPointModel 
    {
        public Guid ConfigurationId { get; set; }
        public IEnumerable<Guid> AutoTestIds { get; set; }
    }
}
