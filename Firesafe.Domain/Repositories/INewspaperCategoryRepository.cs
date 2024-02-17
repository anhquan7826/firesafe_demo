using Firesafe.Domain.Core.Repository;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Repositories;

public interface INewspaperCategoryRepository : IRepository<NewspaperCategory>
{
    public bool IsExist(string category);

    public NewspaperCategory? Get(string category);
}