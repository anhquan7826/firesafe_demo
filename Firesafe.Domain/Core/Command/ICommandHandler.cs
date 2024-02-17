using MediatR;

namespace Firesafe.Domain.Core.Command;

public interface ICommandHandler<in T> : IRequestHandler<T> where T : Command
{
}

public interface ICommandHandler<in T, TR> : IRequestHandler<T, TR> where T : Command<TR>
{
}