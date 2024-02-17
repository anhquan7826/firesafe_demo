using Firesafe.Domain.Core.Command;
using Firesafe.Domain.Core.Event;
using Firesafe.Domain.Core.Mediator;
using MediatR;

namespace Infrastructure.Mediator;

public class MediatorHandler(IMediator mediator) : IMediatorHandler
{
    public Task SendCommand<T>(T command) where T : Command
    {
        return mediator.Send(command);
    }

    public Task<T> SendCommand<T>(Command<T> command)
    {
        return mediator.Send(command);
    }

    // public Task<T> SendQuery<T>(Query<T> query)
    // {
    //     return mediator.Send(query);
    // }

    public Task PublishEvent<T>(T @event) where T : Event
    {
        // TODO: Store events here.
        return mediator.Publish(@event);
    }
}