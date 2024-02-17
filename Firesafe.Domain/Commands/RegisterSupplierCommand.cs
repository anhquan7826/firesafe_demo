using Firesafe.Domain.Core.Command;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Commands;

public class RegisterSupplierCommand : Command<bool>
{
    public required Guid UserId { get; init; }
}