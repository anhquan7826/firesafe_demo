using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Requests.EditUserProfile;

public class EditProfileRequest : BaseRequestModel
{
    public string? Name { get; init; }
    public IFormFile? Avatar { get; init; }
    public IFormFile? Banner { get; init; }
}