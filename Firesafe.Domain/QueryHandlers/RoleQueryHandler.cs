using Firesafe.Domain.Core.Query;
using Firesafe.Domain.Entities;
using Firesafe.Domain.Queries;
using Firesafe.Domain.UnitOfWork;

namespace Firesafe.Domain.QueryHandlers;

public class RoleQueryHandler(IUnitOfWork uow)
    : QueryHandler(uow), IQueryHandler<GetAllRolesQuery, IEnumerable<UserRole>>
{
    public Task<IEnumerable<UserRole>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Uow.RoleRepository.GetRoles());
    }
}