using Firesafe.Domain.Core.Event;
using Firesafe.Domain.Entities.EventStore;
using Firesafe.Domain.Events.EventStore;
using Firesafe.Domain.UnitOfWork;
using EventHandler = Firesafe.Domain.Core.Event.EventHandler;

namespace Firesafe.Domain.EventHandlers.EventStoreHandlers;

public class ExceptionEventHandler(IUnitOfWork uow) : EventHandler(uow), IEventHandler<ExceptionOccuredEvent>
{
    public Task Handle(ExceptionOccuredEvent notification, CancellationToken cancellationToken)
    {
        Uow.ExceptionStoreRepository.Add(new ExceptionStore
        {
            Message = notification.Exception.Message,
            StackTrace = notification.Exception.StackTrace
        });
        Uow.Commit();
        return Task.CompletedTask;
    }
}