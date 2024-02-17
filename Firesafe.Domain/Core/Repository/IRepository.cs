namespace Firesafe.Domain.Core.Repository;

public interface IRepository<TEntity> : IDisposable, IAsyncDisposable
    where TEntity : class
{
    void Add(TEntity obj);

    TEntity? GetById(Guid id);

    IQueryable<TEntity> GetAll();

    void Update(TEntity obj);

    void Remove(Guid id);

    int SaveChanges();
}