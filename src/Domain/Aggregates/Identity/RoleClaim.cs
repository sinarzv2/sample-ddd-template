using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Entities.IdentityModel
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public new Guid Id { get; set; }
    }
  
}
