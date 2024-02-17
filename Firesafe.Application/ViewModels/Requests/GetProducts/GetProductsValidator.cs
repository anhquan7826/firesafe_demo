using Application.Services;
using Application.Services.Interface;
using FluentValidation;

namespace Application.ViewModels.Requests.GetProducts;

public class GetProductsValidator : AbstractValidator<GetProductsRequest>
{
    private readonly ICategoryService _categoryService;

    public GetProductsValidator(ICategoryService categoryService)
    {
        _categoryService = categoryService;
        RuleFor(x => x.Page).NotNull().NotEmpty().WithMessage("Page cannot be empty!").GreaterThan(0)
            .WithMessage("Page must be greater than 0!");
        RuleFor(x => x.Category).NotNull().NotEmpty().WithMessage("Category cannot be empty!").Must(BeAValidCategory)
            .WithMessage("Category {PropertyValue} is not exist!");
    }

    private bool BeAValidCategory(string category)
    {
        return _categoryService.IsProductCategoryExist(category);
    }
}