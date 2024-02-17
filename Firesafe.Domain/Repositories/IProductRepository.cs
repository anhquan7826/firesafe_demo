using Firesafe.Domain.Core.Repository;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    public IEnumerable<Product> GetByCategoryPaged(string category, int page, DateTime timestamp);

    public Product? GetProduct(Guid productId);

    public List<Product> GetBySupplierPaged(Guid supplierId, int page);
    
    public Supplier? GetByProductId(Guid productId);
}