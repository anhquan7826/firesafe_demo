using System.Text.Json;
using Application.Services.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.ViewModels.Requests.AddProduct;

public class AddProductValidator : AbstractValidator<AddProductRequest>
{
    private readonly ICategoryService _categoryService;
    private readonly ICountryService _countryService;

    public AddProductValidator(ICategoryService categoryService, ICountryService countryService)
    {
        _categoryService = categoryService;
        _countryService = countryService;
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Product name cannot be empty!").Length(10, 50)
            .WithMessage("Product name must be 10-50 in length!");
        RuleFor(x => x.OriginId).NotNull().NotEmpty().Length(2).Must(BeAValidOriginId).WithMessage("Invalid Origin Id!");
        RuleFor(x => x.Brand).NotNull().NotEmpty().WithMessage("Brand cannot be empty!").MaximumLength(100)
            .WithMessage("Brand cannot be longer than 100 characters!");
        RuleFor(x => x.Model).NotNull().NotEmpty().WithMessage("Model cannot be empty!").MaximumLength(100)
            .WithMessage("Model cannot be longer than 100 characters!");
        RuleFor(x => x.ShortDescription).NotNull().NotEmpty().WithMessage("Description cannot be empty!")
            .Length(300, 10000)
            .WithMessage("Description must be from 300 to 10000 characters long!");
        RuleFor(x => x.Warranty).NotNull().GreaterThanOrEqualTo((short)0).WithMessage("Invalid Warranty duration!");
        RuleFor(x => x.Packaging).NotNull().NotEmpty().MaximumLength(500)
            .WithMessage("Packaging must be 1-500 in length!");
        RuleFor(x => x.ShippingTime).NotNull().GreaterThanOrEqualTo((short)0).WithMessage("Invalid shipping time!");
        RuleFor(x => x.HasSample).NotNull().NotEmpty().MaximumLength(500)
            .WithMessage("HasSample must be 1-500 in length!");
        RuleFor(x => x.ProductionRate).NotNull().NotEmpty().MaximumLength(500)
            .WithMessage("ProductionRate must be 1-500 in length!");
        RuleFor(x => x.Accessories).MaximumLength(500).When(x => x.Accessories != null)
            .WithMessage("Accessories must be less than 500 in length or null!");
        RuleFor(x => x.PostSupport).MaximumLength(500).When(x => x.PostSupport != null)
            .WithMessage("PostSupport must be less than 500 in length or null!");
        RuleFor(x => x.AdditionalSpecification)
            .Must(BeAValidSpecification!).When(x => x.AdditionalSpecification != null)
            .WithMessage("Specification is not a valid JSON!");
        RuleFor(x => x.Faq).MaximumLength(10000).When(x => x.Faq != null)
            .WithMessage("FAQ must be less than 10000 in length!");
        RuleFor(x => x).Custom((x, context) =>
        {
            if (x.Price == null)
            {
                if (x.PriceMin != null) return;
                context.AddFailure(x.PriceMax == null
                    ? "Product price or price range must be provided!"
                    : "A lower price boundary must be provided!");
            }

            if (x.Price == null) return;
            if (x.PriceMax != null || x.PriceMin != null)
                context.AddFailure("Only price or price range can only be provided!");
        });
        RuleFor(x => x.Shape).MaximumLength(500).When(x => x.Shape != null)
            .WithMessage("Shape must be less than 500 in length!");
        RuleFor(x => x.Color).MaximumLength(500).When(x => x.Color != null)
            .WithMessage("Color must be less than 500 in length!");
        RuleFor(x => x.Material).MaximumLength(500).When(x => x.Material != null)
            .WithMessage("Material must be less than 500 in length!");
        RuleFor(x => x.Structure).MaximumLength(1000).When(x => x.Structure != null)
            .WithMessage("Structure must be less than 1000 in length!");
        RuleFor(x => x.FireResistant).MaximumLength(1000).When(x => x.FireResistant != null)
            .WithMessage("FireResistant must be less than 1000 in length!");
        RuleFor(x => x.WaterResistant).MaximumLength(1000).When(x => x.WaterResistant != null)
            .WithMessage("WaterResistant must be less than 1000 in length!");
        RuleFor(x => x.Applications).MaximumLength(1000).When(x => x.Applications != null)
            .WithMessage("Applications must be less than 1000 in length!");
        RuleFor(x => x.Weight).GreaterThanOrEqualTo(0).When(x => x.Weight != null).WithMessage("Invalid Weight!");
        RuleFor(x => x.Volume).GreaterThanOrEqualTo(0).When(x => x.Volume != null).WithMessage("Invalid Volume!");
        RuleFor(x => x.CertificateNames).NotNull().NotEmpty()
            .WithMessage("At least one certification must be provided!");
        RuleFor(x => x.CertificateImages.Count == x.CertificateNames.Count).Must(value => value)
            .WithMessage("Certifications and their images are not matching!");
        RuleFor(x => x.Thumbnail).NotNull().WithMessage("Thumbnail is required!").Must(BeAValidFileSize)
            .WithMessage("File size must be less than or equal to 5Mb!");
        RuleForEach(x => x.Images).Must(BeAValidFileSize).WithMessage("File size must be less than or equal to 5Mb!");
        RuleFor(x => x.Categories).NotNull().NotEmpty().WithMessage("At least 1 category must be specified!");
        RuleForEach(x => x.Categories).NotNull().NotEmpty()
            .Must(BeAValidCategory).WithMessage("Category {PropertyValue} is not exist!");
    }

    private bool BeAValidCategory(string category)
    {
        return _categoryService.IsProductCategoryExist(category);
    }

    private bool BeAValidSpecification(string specification)
    {
        try
        {
            JsonDocument.Parse(specification);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool BeAValidFileSize(IFormFile file)
    {
        return file.Length <= 5 * 1024 * 1024;
    }

    private bool BeAValidOriginId(string id)
    {
        return _countryService.IsExist(id);
    }
}