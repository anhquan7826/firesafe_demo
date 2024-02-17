using Firesafe.Domain.Core.Event;
using Firesafe.Domain.Entities.EventStore;

namespace Firesafe.Domain.Events.EventStore;

public class ExceptionOccuredEvent : Event
{
    public required ExceptionStore Exception { get; init; }
}