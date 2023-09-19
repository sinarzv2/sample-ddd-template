using Domain.Aggregates.Identity;
using Domain.IRepository;
using Infrastructure.Core;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserClaimRepository : Repository<UserClaim>, IUserClaimRepository
    {
        public UserClaimRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<UserClaim>> GetClaimsByType(Guid userId, string type)
        {
            var userClaims = DbContext.UserClaims.Where(d => d.UserId == userId && d.ClaimType == type).Select(s => new
            {
                Type = s.ClaimType,
                Value = s.ClaimValue
            });

            var roleClaims = from userRole in DbContext.UserRoles
                             join roleClaim in DbContext.RoleClaims on userRole.RoleId equals roleClaim.RoleId
                             where userRole.UserId == userId && roleClaim.ClaimType == type
                             select new
                             {
                                 Type = roleClaim.ClaimType,
                                 Value = roleClaim.ClaimValue
                             };
            var allClaim = await userClaims.Union(roleClaims).Select(s => new UserClaim()
            {
                ClaimType = s.Type,
                ClaimValue = s.Value
            }).ToListAsync();
            return allClaim;
        }
    }
}
