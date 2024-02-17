using Firesafe.Domain.Core.Command;

namespace Firesafe.Domain.Commands;

public class SetUserRolesCommand : Command<bool>
{
    public required Guid UserId { get; init; }
    public required List<string> Roles { get; init; }
}