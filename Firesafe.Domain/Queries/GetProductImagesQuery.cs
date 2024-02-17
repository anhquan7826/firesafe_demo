using Firesafe.Domain.Core.Query;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Queries;

public class GetProductImagesQuery(Guid productId) : Query<IEnumerable<ProductImage>>
{
    public readonly Guid ProductId = productId;
}