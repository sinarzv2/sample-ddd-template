using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Aggregates.Identity;
using Infrastructure.IRepository;
using Infrastructure.Models;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserClaimRepository : Repository<UserClaim>, IUserClaimRepository
    {
        public UserClaimRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ClaimModel>> GetClaimsByType(Guid userId, string type)
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
            var allClaim = await userClaims.Union(roleClaims).Select(s => new ClaimModel()
            {
                Type = s.Type,
                Value = s.Value
            }).ToListAsync();
            return allClaim;
        }
    }
}
