using Domain.Aggregates.Identity;
using Domain.Entities.IdentityModel;
using Infrastructure.Persistance.Configuration.IdentityConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime>("CreatedDate")
                    .IsRequired().HasDefaultValueSql("GETDATE()");
                modelBuilder.Entity(entityType.ClrType)
                    .Property<DateTime?>("UpdatedDate")
                    .IsRequired(false);
                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>("CreatedBy")
                    .HasMaxLength(100)
                    .IsRequired(false);
                modelBuilder.Entity(entityType.ClrType)
                    .Property<string>("UpdatedBy")
                    .IsRequired(false)
                    .HasMaxLength(100);

            }
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            ChangeTracker.DetectChanges();
            var entries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                entityEntry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                entityEntry.Property("UpdatedBy").CurrentValue = _httpContextAccessor.HttpContext?.User.Identity?.Name;

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedBy").CurrentValue = _httpContextAccessor.HttpContext?.User.Identity?.Name;
                    entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
