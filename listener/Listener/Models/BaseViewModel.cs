using System;

namespace TestIt.WebApi.Models
{
    public abstract class BaseViewModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
