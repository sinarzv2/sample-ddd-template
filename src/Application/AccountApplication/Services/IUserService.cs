using Common.DependencyLifeTime;

namespace Application.AccountApplication.Services
{
    public interface IUserService : IScopedService
    {
        Task<bool> ExistUserByUsername(string username);
        
    }
}
