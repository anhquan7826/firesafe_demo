using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Requests.SearchProducts;

public class SearchProductsRequest : BaseRequestModel
{
    [Required] public required string Query { get; init; }

    public int Page { get; init; } = 1;

    public string? Categories { get; init; }

    public int? Year { get; init; }

    public short? Rating { get; init; }

    public string? PriceRange { get; init; }
}