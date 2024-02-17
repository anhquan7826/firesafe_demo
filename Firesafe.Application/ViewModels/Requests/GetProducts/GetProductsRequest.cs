using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Requests.GetProducts;

public class GetProductsRequest : BaseRequestModel
{
    [Required] public required string Category { get; init; }

    public int Page { get; init; } = 1;
}