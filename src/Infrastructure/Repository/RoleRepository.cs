using Domain.Aggregates.Identity;
using Domain.IRepository;
using Infrastructure.Core;
using Infrastructure.Persistance;

namespace Infrastructure.Repository;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}