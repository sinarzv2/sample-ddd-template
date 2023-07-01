using System;

namespace Infrastructure.Models
{
    public class ClaimModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
