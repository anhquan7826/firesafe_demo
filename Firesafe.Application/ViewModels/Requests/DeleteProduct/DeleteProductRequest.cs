using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Requests.DeleteProduct;

public class DeleteProductRequest : BaseRequestModel
{
    [Required] public required Guid Id { get; init; }
}