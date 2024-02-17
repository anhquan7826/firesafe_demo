using Firesafe.Domain.Core.Repository;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Repositories;

public interface IProductCategoryRepository : IRepository<Category>
{
    public bool IsExist(string category);

    public Category? Get(string category);
}