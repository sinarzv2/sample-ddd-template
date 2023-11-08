using Application.UnitOfWork;
using Domain.IRepository;
using Infrastructure.Persistance;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private UserRepository? _userRepository;
    private RoleRepository? _roleRepository;
    private UserClaimRepository? _userClaimRepository;
    public UnitOfWork(ApplicationDbContext databaseContext)
    {
        _context = databaseContext;
    }


    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

    public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_context);

    public IUserClaimRepository UserClaimRepository => _userClaimRepository ??= new UserClaimRepository(_context);

    public async Task CommitChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }
    public bool IsDisposed { get; protected set; }
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }
        if (disposing)
        {
            _context?.Dispose();
        }

        IsDisposed = true;
    }
}