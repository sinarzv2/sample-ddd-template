using Domain.Aggregates.Identity;
using Domain.Entities.IdentityModel;
using Infrastructure.IRepository;
using Infrastructure.Persistance;

namespace Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
}
