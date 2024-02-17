using Firesafe.Domain.Core.Event;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Events;

public class SupplierProfileUpdatedEvent : Event
{
    public required Supplier Supplier { get; init; }
}