using Firesafe.Domain.Entities.EventStore;
using Firesafe.Domain.Repositories.EventStore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.EventStore;

public class ExceptionStoreRepository(DbContext dbContext)
    : Repository<ExceptionStore>(dbContext), IExceptionStoreRepository
{
    public ExceptionStore GetLatestException()
    {
        return DbSet.OrderByDescending(x => x.Timestamp).FirstOrDefault() ?? new ExceptionStore
        {
            Id = Guid.Empty,
            Message = "null",
            StackTrace = "null",
            Timestamp = DateTime.UnixEpoch
        };
    }

    public List<ExceptionStore> GetFromTimestamp(DateTime timestamp)
    {
        return DbSet
            .Where(x => x.Timestamp >= timestamp)
            .OrderByDescending(x => x.Timestamp)
            .ToList();
    }
}