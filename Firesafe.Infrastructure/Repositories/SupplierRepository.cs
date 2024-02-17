using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SupplierRepository(DatabaseContext dbContext) : Repository<Supplier>(dbContext), ISupplierRepository
{
    public Supplier? GetByUserId(Guid userId)
    {
        return DbSet.FirstOrDefault(it => it.UserId == userId);
    }

    public void RegisterSupplier(Guid userId)
    {
        var supplier = new Supplier
        {
            UserId = userId
        };
        Add(supplier);
    }
}