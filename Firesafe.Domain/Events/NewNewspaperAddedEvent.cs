using Firesafe.Domain.Core.Event;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Events;

public class NewNewspaperAddedEvent : Event
{
    public required Newspaper Newspaper { get; init; }
}