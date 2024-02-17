namespace Application.DTOs;

public class NewspaperShortDto
{
    public required Guid NewspaperId { get; init; }
    public required string Title { get; init; }
    public required string Thumbnail { get; set; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? EditedAt { get; init; }
}

public class NewspaperDto : NewspaperShortDto
{
    public required string Content { get; set; }
}