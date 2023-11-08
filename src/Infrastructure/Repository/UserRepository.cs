using Domain.Aggregates.Identity;
using Domain.IRepository;
using Infrastructure.Core;
using Infrastructure.Persistance;

namespace Infrastructure.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}