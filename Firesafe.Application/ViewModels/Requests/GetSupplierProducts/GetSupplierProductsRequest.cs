namespace Application.ViewModels.Requests.GetSupplierProducts;

public class GetSupplierProductsRequest : BaseRequestModel
{
    public Guid? Id { get; set; }

    public int Page { get; init; } = 1;
}