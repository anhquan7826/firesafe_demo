using Firesafe.Domain.Core.Query;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Queries;

public class GetProductsByPageQuery(int page) : Query<IEnumerable<Product>>
{
    public readonly int Page = page;
}