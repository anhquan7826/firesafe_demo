using MediatR;

namespace Firesafe.Domain.Core.Query;

public abstract class Query<T> : IRequest<T>
{
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
}