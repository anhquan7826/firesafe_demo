using Firesafe.Domain.UnitOfWork;

namespace Firesafe.Domain.Core.Event;

public abstract class EventHandler(IUnitOfWork uow)
{
    protected IUnitOfWork Uow { get; } = uow;
}