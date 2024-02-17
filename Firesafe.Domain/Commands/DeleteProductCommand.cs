using Firesafe.Domain.Core.Command;

namespace Firesafe.Domain.Commands;

public class DeleteProductCommand : Command<bool>
{
    public required Guid ProductId { get; init; }
}