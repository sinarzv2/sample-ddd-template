using System;
using Domain.Entities.IdentityModel;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IUserClaimRepository : IRepository<UserClaim>
    {
        Task<List<ClaimModel>> GetClaimsByType(Guid userId, string type);
    }
}
