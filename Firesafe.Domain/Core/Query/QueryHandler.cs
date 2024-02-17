using Firesafe.Domain.UnitOfWork;

namespace Firesafe.Domain.Core.Query;

public abstract class QueryHandler(IUnitOfWork uow)
{
    protected IUnitOfWork Uow { get; } = uow;
}