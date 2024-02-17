using MediatR;

namespace Firesafe.Domain.Core.Event;

public abstract class Event : INotification
{
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
}