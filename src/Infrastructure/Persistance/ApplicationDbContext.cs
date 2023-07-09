using Domain.Aggregates.Cities;
using Domain.Aggregates.DiscountCoupons;
using Domain.Aggregates.Identity;
using Domain.Aggregates.Provinces;
using Domain.SeedWork;
using Infrastructure.Persistance.Configuration.IdentityConfiguration;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IMediator _mediator;


    public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor, IMediator mediator)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _mediator = mediator;
    }

    public DbSet<City> Cities { get; set; }

    public DbSet<Province> Provinces { get; set; }

    public DbSet<DiscountCoupon> DiscountCoupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IEntity).IsAssignableFrom(p));
        var entityTypes =
            modelBuilder.Model.GetEntityTypes().Where(d => types.Contains(d.ClrType));

        foreach (var entityType in entityTypes )
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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        ChangeTracker.DetectChanges();

        var entries = ChangeTracker
            .Entries()
            .Where(e => 
                e.State == EntityState.Added
                || e.State == EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            if (entityEntry.Entity is Enumeration)
                entityEntry.State = EntityState.Unchanged;

            if (entityEntry.Entity is IEntity)
            {


                entityEntry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                entityEntry.Property("UpdatedBy").CurrentValue =
                    _httpContextAccessor.HttpContext?.User.Identity?.Name;

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedBy").CurrentValue =
                        _httpContextAccessor.HttpContext?.User.Identity?.Name;
                    entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }
            }
        }

        var affectedRows = await base.SaveChangesAsync(cancellationToken);
        if (affectedRows > 0)
        {
            var aggregateRoots =
                    ChangeTracker.Entries()
                        .Where(current => current.Entity is IAggregateRoot)
                        .Select(current => current.Entity as IAggregateRoot)
                        .ToList();

            foreach (var aggregateRoot in aggregateRoots)
            {
                if (aggregateRoot == null) continue;
                foreach (var domainEvent in aggregateRoot.DomainEvents)
                    await _mediator.Publish(domainEvent, cancellationToken);
                    
                aggregateRoot.ClearDomainEvents();
            }
        }

        return affectedRows;

    }
}