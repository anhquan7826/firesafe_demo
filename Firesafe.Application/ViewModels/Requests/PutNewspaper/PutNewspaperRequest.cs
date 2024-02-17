using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Requests.PutNewspaper;

public class PutNewspaperRequest : BaseRequestModel
{
    private readonly string? _title;

    public Guid? Id { get; init; }

    public string? Title
    {
        get => _title;
        init => _title = value?.Trim();
    }

    public IFormFile? Thumbnail { get; init; }
    public IFormFile? Content { get; init; }
    public List<IFormFile>? Images { get; init; }

    public List<string>? NewspaperCategories { get; init; }
}