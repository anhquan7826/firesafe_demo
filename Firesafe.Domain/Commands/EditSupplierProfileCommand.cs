using Firesafe.Domain.Core.Command;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Commands;

public class EditSupplierProfileCommand : Command<bool>
{
    public required Guid SupplierId { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public DateOnly? EstablishedAt { get; init; }
    public string? Address { get; init; }
}