using MediatR;

namespace Firesafe.Domain.Core.Event;

public interface IEventHandler<in T> : INotificationHandler<T> where T : Event
{
}