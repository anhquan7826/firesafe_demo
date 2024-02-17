using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Requests.EditUserProfile;

public class EditProfileValidator : AbstractValidator<EditProfileRequest>
{
    public EditProfileValidator()
    {
        RuleFor(x => x.Name).Length(10, 50).When(n => n.Name != null).WithMessage("Name must be in 10 to 50 characters long!");
        RuleFor(x => x.Avatar).Must(BeAValidFile!).When(x => x.Avatar != null)
            .WithMessage("Avatar must be less than 5mb!");
        RuleFor(x => x.Banner).Must(BeAValidFile!).When(x => x.Avatar != null)
            .WithMessage("Banner must be less than 5mb!");
    }
    
    private bool BeAValidFile(IFormFile file)
    {
        return file.Length <= 5 * 1024 * 1024;
    }
}