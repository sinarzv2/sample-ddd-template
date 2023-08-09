using Domain.Aggregates.Identity;
using Infrastructure.Core;
using Infrastructure.Models.Account;

namespace Infrastructure.IRepository
{
    public interface IUserClaimRepository : IRepository<UserClaim>
    {
        Task<List<ClaimModel>> GetClaimsByType(Guid userId, string type);
    }
}
