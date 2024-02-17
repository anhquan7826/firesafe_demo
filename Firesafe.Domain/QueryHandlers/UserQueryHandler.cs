using Firesafe.Domain.Core.Query;
using Firesafe.Domain.Entities;
using Firesafe.Domain.Queries;
using Firesafe.Domain.UnitOfWork;

namespace Firesafe.Domain.QueryHandlers;

public class UserQueryHandler(IUnitOfWork uow) : QueryHandler(uow), IQueryHandler<GetUserByFirebaseIdQuery, User?>
{
    public Task<User?> Handle(GetUserByFirebaseIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Uow.UserRepository.GetByFirebaseId(request.FirebaseId));
    }
}