using Firesafe.Domain.Core.Command;
using Firesafe.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Firesafe.Domain.Commands;

public class EditUserProfileCommand : Command<bool>
{
    public required Guid UserId { get; init; }
    
    public required string? Name { get; init; }
}