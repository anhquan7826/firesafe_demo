using Firesafe.Domain.Core.Repository;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Repositories;

public interface ISupplierRepository : IRepository<Supplier>
{
    public Supplier? GetByUserId(Guid userId);

    public void RegisterSupplier(Guid userId);
}