using System;

namespace GgstResource.Models
{
    public abstract class BaseResource
    {
        public string Reference { get; set; }
        public long Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}