using Firesafe.Domain.Core.Repository;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Repositories;

public interface IOriginRepository : IRepository<Origin>
{
    public IEnumerable<Origin> GetAllSorted();

    public Origin? Get(string id);
}