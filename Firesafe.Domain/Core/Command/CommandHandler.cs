using Firesafe.Domain.Core.Mediator;
using Firesafe.Domain.UnitOfWork;

namespace Firesafe.Domain.Core.Command;

public abstract class CommandHandler(IUnitOfWork uow, IMediatorHandler mediatorHandler)
{
    protected IUnitOfWork Uow { get; } = uow;
    protected IMediatorHandler MediatorHandler { get; } = mediatorHandler;
}