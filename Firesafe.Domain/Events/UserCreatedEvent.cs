using Firesafe.Domain.Core.Event;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Events;

public class UserCreatedEvent(User user) : Event
{
    public readonly User User = user;
}