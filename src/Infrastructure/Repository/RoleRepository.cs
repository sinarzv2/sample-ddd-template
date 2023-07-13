using Domain.Aggregates.Identity;
using Infrastructure.IRepository;
using Infrastructure.Persistance;

namespace Infrastructure.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
}
