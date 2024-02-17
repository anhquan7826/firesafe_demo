using Firesafe.Domain.Core.Event;

namespace Firesafe.Domain.Events;

public class ProductDeletedEvent(Guid productId) : Event
{
    public readonly Guid ProductId = productId;
}