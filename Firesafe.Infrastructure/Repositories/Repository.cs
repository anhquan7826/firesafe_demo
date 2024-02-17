using Firesafe.Domain.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class Repository<TEntity>(DbContext dbContext) : IRepository<TEntity>
    where TEntity : class
{
    protected readonly DbSet<TEntity> DbSet = dbContext.Set<TEntity>();
    
    public void Dispose()
    {
        dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await dbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public void Add(TEntity obj)
    {
        DbSet.Add(obj);
    }

    public TEntity? GetById(Guid id)
    {
        return DbSet.Find(id);
    }

    public IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    public void Update(TEntity obj)
    {
        DbSet.Update(obj);
    }

    public void Remove(Guid id)
    {
        var o = DbSet.Find(id);
        if (o != null) DbSet.Remove(o);
    }

    public int SaveChanges()
    {
        return dbContext.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }
}