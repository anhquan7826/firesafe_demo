using Firesafe.Domain.Core.Event;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Events;

public class ProductCreatedEvent(Product product) : Event
{
    public readonly Product Product = product;
}