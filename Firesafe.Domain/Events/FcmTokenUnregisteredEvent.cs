using Firesafe.Domain.Core.Event;

namespace Firesafe.Domain.Events;

public class FcmTokenUnregisteredEvent : Event
{
    public required Guid UserId { get; init; }
    public required string Token { get; init; }
}