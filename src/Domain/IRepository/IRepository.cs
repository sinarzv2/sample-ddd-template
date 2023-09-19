using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Domain.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        void Attach(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        void DeleteById(object id);
        Task DeleteByIdAsync(object id, CancellationToken cancellationToken);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        void Detach(TEntity entity);
        TEntity GetById(params object[] ids);
        Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
        void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class;
        Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;


        Task SaveChangesAsync(CancellationToken cancellationToken);
        void SaveChanges();
    }
}