using Firesafe.Domain.Core.Event;

namespace Firesafe.Domain.Events;

public class NewspaperDeletedEvent : Event
{
    public required Guid NewspaperId { get; init; }
}