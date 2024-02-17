using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Requests.EditSupplierProfile;

public class EditSupplierProfileRequest : BaseRequestModel
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public DateOnly? EstablishedAt { get; init; }
    public String? Address { get; init; }
    public IFormFile? Avatar { get; init; }
    public IFormFile? Banner { get; init; }
}