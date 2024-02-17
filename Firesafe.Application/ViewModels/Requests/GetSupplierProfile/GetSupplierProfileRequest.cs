namespace Application.ViewModels.Requests.GetSupplierProfile;

public class GetSupplierProfileRequest : BaseRequestModel
{
    public Guid? Id { get; init; }
}