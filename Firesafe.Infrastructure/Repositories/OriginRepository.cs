using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OriginRepository(DbContext dbContext) : Repository<Origin>(dbContext), IOriginRepository
{
    public IEnumerable<Origin> GetAllSorted()
    {
        return DbSet.OrderBy(o => o.Name);
    }

    public Origin? Get(string id)
    {
        return DbSet.FirstOrDefault(o => o.OriginId == id);
    }
}