using System;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Aggregates.Identity;

namespace Infrastructure.IRepository
{
    public interface IUserClaimRepository : IRepository<UserClaim>
    {
        Task<List<ClaimModel>> GetClaimsByType(Guid userId, string type);
    }
}
