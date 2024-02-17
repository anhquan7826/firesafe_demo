using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductImageRepository(DbContext dbContext) : Repository<ProductImage>(dbContext), IProductImageRepository
{
    public IEnumerable<ProductImage> GetProductImages(Guid productId)
    {
        return DbSet.Where(pi => pi.ProductId == productId);
    }
}