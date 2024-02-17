using Firesafe.Domain.Core.Command;

namespace Firesafe.Domain.Commands;

public class AddNewNewspaperCommand : Command<bool>
{
    public required Guid NewspaperId { get; init; }
    public required string Title { get; set; }
    
    public required List<Guid> NewspaperImageIds { get; init; }
    public required List<string> NewspaperCategories { get; init; }
}