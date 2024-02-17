using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(DbContext dbContext) : Repository<Product>(dbContext), IProductRepository
{
    private readonly DbContext _dbContext = dbContext;

    public IEnumerable<Product> GetByCategoryPaged(string category, int page, DateTime timestamp)
    {
        return DbSet.Where(p => p.Categories.Select(c => c.CategoryId).Contains(category))
            .Where(p => p.CreatedAt < timestamp)
            .OrderBy(p => p.CreatedAt)
            .Skip(10 * page).Take(10);
    }

    public Product? GetProduct(Guid productId)
    {
        return DbSet
            .Include(p =>
                p.ProductImages.OrderBy(pi => pi.Order)
            )
            .Include(p => p.ProductCertificates)
            .Include(p => p.Categories)
            .FirstOrDefault(p => p.ProductId == productId);
    }

    public List<Product> GetBySupplierPaged(Guid supplierId, int page)
    {
        return DbSet
            .Where(p => p.SupplierId == supplierId)
            .Include(p => p.Categories)
            .Include(p => p.Supplier)
            .OrderBy(p => p.CreatedAt).Skip(10 * page).Take(10)
            .ToList();
    }

    public Supplier? GetByProductId(Guid productId)
    {
        var product = DbSet.Include(p => p.Supplier).FirstOrDefault(p => p.ProductId == productId);
        return product?.Supplier;
    }
}