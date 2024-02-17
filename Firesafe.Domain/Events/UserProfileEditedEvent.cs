using Firesafe.Domain.Core.Event;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Events;

public class UserProfileEditedEvent : Event
{
    public required User User { get; init; }
}