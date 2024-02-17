using MediatR;

namespace Firesafe.Domain.Core.Command;

public abstract class Command : IRequest
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;
}

public abstract class Command<T> : IRequest<T>
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;
}