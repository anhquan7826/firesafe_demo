using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Requests.GetProductDetail;

public class GetProductDetailRequest : BaseRequestModel
{
    [Required] public Guid Id { get; init; }
}