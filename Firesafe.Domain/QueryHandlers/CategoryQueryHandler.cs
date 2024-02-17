using Firesafe.Domain.Core.Query;
using Firesafe.Domain.Entities;
using Firesafe.Domain.Queries;
using Firesafe.Domain.UnitOfWork;

namespace Firesafe.Domain.QueryHandlers;

public class CategoryQueryHandler(IUnitOfWork uow)
    : QueryHandler(uow), IQueryHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    public Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<Category>>(Uow.CategoryRepository.GetAll());
    }
}