using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Entities.IdentityModel
{
    public class Role : IdentityRole<Guid>
    {

        public string Description { get; set; } = string.Empty;
    
    }
}
