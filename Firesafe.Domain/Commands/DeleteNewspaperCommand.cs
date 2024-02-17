using Firesafe.Domain.Core.Command;

namespace Firesafe.Domain.Commands;

public class DeleteNewspaperCommand : Command<bool>
{
    public required Guid NewspaperId { get; init; }
}