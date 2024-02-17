using Firesafe.Domain.Core.Query;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Queries;

public class GetProductQuery(Guid productId) : Query<Product?>
{
    public readonly Guid ProductId = productId;
}