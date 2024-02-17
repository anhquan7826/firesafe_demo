using Firesafe.Domain.Core.Repository;
using Firesafe.Domain.Entities.EventStore;

namespace Firesafe.Domain.Repositories.EventStore;

public interface IExceptionStoreRepository : IRepository<ExceptionStore>
{
    public ExceptionStore GetLatestException();

    public List<ExceptionStore> GetFromTimestamp(DateTime timestamp);
}