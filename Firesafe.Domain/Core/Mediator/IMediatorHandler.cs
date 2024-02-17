using Firesafe.Domain.Core.Command;

namespace Firesafe.Domain.Core.Mediator;

public interface IMediatorHandler
{
    public Task SendCommand<T>(T command) where T : Command.Command;

    public Task<T> SendCommand<T>(Command<T> command);

    // public Task<T> SendQuery<T>(Query<T> query);

    public Task PublishEvent<T>(T @event) where T : Event.Event;
}