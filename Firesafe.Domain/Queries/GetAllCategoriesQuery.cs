using Firesafe.Domain.Core.Query;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Queries;

public class GetAllCategoriesQuery : Query<IEnumerable<Category>>
{
}