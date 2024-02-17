using Firesafe.Domain.Core.Event;

namespace Firesafe.Domain.Events;

public class UserRolesSetEvent : Event
{
    public required List<string> Roles { get; init; }
    public required Guid UserId { get; init; }
}