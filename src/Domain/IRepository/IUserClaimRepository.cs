using Domain.Aggregates.Identity;

namespace Domain.IRepository;

public interface IUserClaimRepository : IRepository<UserClaim>
{
    Task<List<UserClaim>> GetClaimsByType(Guid userId, string type);
}