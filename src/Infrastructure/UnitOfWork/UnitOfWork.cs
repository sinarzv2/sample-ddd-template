using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.IRepository;
using Infrastructure.Persistance;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext databaseContext)
        {
            _context = databaseContext;
            UserRepository = new UserRepository(_context);
            RoleRepository = new RoleRepository(_context);
            UserClaimRepository = new UserClaimRepository(_context);
        }


        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserClaimRepository UserClaimRepository { get; }

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
}
