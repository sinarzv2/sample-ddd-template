using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.IRepository;

namespace Infrastructure.Core;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class

{
    protected ApplicationDbContext DbContext { get; }

    public DbSet<TEntity> Entities { get; }
    public virtual IQueryable<TEntity> Table => Entities;
    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
        Entities = DbContext.Set<TEntity>();
    }

    #region Async Method
    public virtual async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return await Entities.FindAsync(ids, cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        if (entities != null)
        {
            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        }
    }
    public async Task UpdateAsync(TEntity entity)
    {

        await Task.Run(() => Entities.Update(entity));
    }
    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {

        if (entities != null)
        {
            await Task.Run(() => Entities.UpdateRange(entities));
        }
    }
    public async Task DeleteAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        await Task.Run(() => { Entities.Remove(entity); });
    }
    public async Task DeleteByIdAsync(object id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(cancellationToken, id);
        await DeleteAsync(entity);
    }
    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        await Task.Run(() => Entities.RemoveRange(entities));
    }

    #endregion

    #region Sync Methods
    public virtual TEntity GetById(params object[] ids)
    {
        return Entities.Find(ids);
    }

    public virtual void Add(TEntity entity)
    {
        Entities.Add(entity);

    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        Entities.AddRange(entities);

    }

    public virtual void Update(TEntity entity)
    {

        Entities.Update(entity);
    }



    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {

        if (entities != null)
        {
            Entities.UpdateRange(entities);
        }
    }




    public virtual void Delete(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        Entities.Remove(entity);
    }



    public void DeleteById(object id)
    {
        var entity = GetById(id);
        Delete(entity);
    }



    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        Entities.RemoveRange(entities);
    }


    #endregion

    #region Attach & Detach
    public virtual void Detach(TEntity entity)
    {
        var entry = DbContext.Entry(entity);
        if (entry != null)
            entry.State = EntityState.Detached;
    }

    public virtual void Attach(TEntity entity)
    {
        if (DbContext.Entry(entity).State == EntityState.Detached)
            Entities.Attach(entity);
    }
    #endregion

    #region Explicit Loading
    public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
        where TProperty : class
    {
        Attach(entity);

        var collection = DbContext.Entry(entity).Collection(collectionProperty);
        if (!collection.IsLoaded)
            await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
        where TProperty : class
    {
        Attach(entity);
        var collection = DbContext.Entry(entity).Collection(collectionProperty);
        if (!collection.IsLoaded)
            collection.Load();
    }

    public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken)
        where TProperty : class
    {
        Attach(entity);
        var reference = DbContext.Entry(entity).Reference(referenceProperty);
        if (!reference.IsLoaded)
            await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
        where TProperty : class
    {
        Attach(entity);
        var reference = DbContext.Entry(entity).Reference(referenceProperty);
        if (!reference.IsLoaded)
            reference.Load();
    }
    #endregion

    #region SaveChange

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public void SaveChanges()
    {
        DbContext.SaveChanges();
    }
    #endregion
}