using Domain.Aggregates.Identity;
using Infrastructure.Models;

namespace Infrastructure.IRepository
{
    public interface IUserClaimRepository : IRepository<UserClaim>
    {
        Task<List<ClaimModel>> GetClaimsByType(Guid userId, string type);
    }
}
