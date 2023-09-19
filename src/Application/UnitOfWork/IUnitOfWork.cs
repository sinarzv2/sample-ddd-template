using Common.DependencyLifeTime;
using Domain.IRepository;

namespace Application.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IScopedService
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserClaimRepository UserClaimRepository { get; }
        Task CommitChangesAsync(CancellationToken cancellationToken = default);
    }
}
