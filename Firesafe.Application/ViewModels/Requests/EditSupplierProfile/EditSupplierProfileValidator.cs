using Application.Services.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.Requests.EditSupplierProfile;

public class EditSupplierProfileValidator : AbstractValidator<EditSupplierProfileRequest>
{
    private readonly IProvinceService _provinceService;

    public EditSupplierProfileValidator(IProvinceService provinceService)
    {
        _provinceService = provinceService;
        RuleFor(x => x.Name).Length(10, 50).When(n => n.Name != null)
            .WithMessage("Name must be in 10 to 50 characters long!");
        RuleFor(x => x.Description).Length(100, 10000).When(x => x.Description != null)
            .WithMessage("Description must be in 100 to 10000 characters long!");
        RuleFor(x => x.EstablishedAt).LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .When(x => x.EstablishedAt != null).WithMessage("Invalid establishment date!");
        RuleFor(x => x.Address).NotEmpty().Length(1, 200).When(x => x.Address != null)
            .WithMessage("Invalid Address!");
        RuleFor(x => x.Avatar).Must(BeAValidFile!).When(x => x.Avatar != null)
            .WithMessage("Avatar must be less than 5mb!");
        RuleFor(x => x.Banner).Must(BeAValidFile!).When(x => x.Avatar != null)
            .WithMessage("Banner must be less than 5mb!");
    }

    private bool BeAValidFile(IFormFile file)
    {
        return file.Length <= 5 * 1024 * 1024;
    }

    private bool BeAValidProvinceId(int id)
    {
        return _provinceService.Get(id) != null;
    }
}