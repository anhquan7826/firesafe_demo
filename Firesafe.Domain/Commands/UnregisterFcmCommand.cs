using Firesafe.Domain.Core.Command;

namespace Firesafe.Domain.Commands;

public class UnregisterFcmCommand : Command<bool>
{
    public required Guid UserId { get; init; }
    public required string Token { get; init; }
}