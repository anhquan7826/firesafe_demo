using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Requests.EditProduct;

public class EditProductRequest : BaseRequestModel
{
    [Required] public required Guid ProductId { get; init; }
    public string? Name { get; init; }
    public string? Specification { get; init; } = "{}";
    public short? Year { get; init; }
    public short? InStock { get; init; }
    public int? Price { get; init; }
    public int? PriceMax { get; init; }
    public int? PriceMin { get; init; }
    public IEnumerable<string>? Categories { get; init; }
}