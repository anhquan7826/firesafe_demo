using Firesafe.Domain.Core.Repository;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Repositories;

public interface IProductImageRepository : IRepository<ProductImage>
{
    public IEnumerable<ProductImage> GetProductImages(Guid productId);
}