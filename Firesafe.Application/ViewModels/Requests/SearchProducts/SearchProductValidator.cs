using Application.Services;
using Application.Services.Interface;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Application.ViewModels.Requests.SearchProducts;

public class SearchProductValidator : AbstractValidator<SearchProductsRequest>
{
    private readonly ICategoryService _categoryService;

    public SearchProductValidator(ICategoryService categoryService)
    {
        _categoryService = categoryService;
        RuleFor(x => x.Query).NotNull().NotEmpty().WithMessage("Query cannot be empty!");
        RuleFor(x => x.Categories).Must(BeAValidCategories).When(r => r.Categories != null)
            .WithMessage("At least 1 category must be specified!");
        RuleFor(x => x.Year).LessThanOrEqualTo(DateTime.UtcNow.Year).When(r => r.Year != null)
            .WithMessage("Year is invalid!");
        RuleFor(x => x.Rating).LessThanOrEqualTo((short)5).GreaterThanOrEqualTo((short)0).When(r => r.Rating != null)
            .WithMessage("Rating is invalid!");
        RuleFor(x => x.PriceRange).Must(BeAValidPriceRange).When(r => r.PriceRange != null)
            .WithMessage("Price range is invalid!");
    }

    private bool BeAValidCategories(string? categories)
    {
        if (categories == null) return false;
        var cList = categories.Split(";");
        if (cList.Length == 0) return false;
        return cList.Where(c => !c.IsNullOrEmpty()).All(c => _categoryService.IsProductCategoryExist(c));
    }

    private bool BeAValidPriceRange(string? range)
    {
        if (range == null) return false;
        var pList = range.Split(";");
        if (pList.Length != 2) return false;
        List<int> prices;
        try
        {
            prices = pList.Where(p => !p.IsNullOrEmpty()).Select(int.Parse).ToList();
        }
        catch (Exception)
        {
            return false;
        }

        if (prices[1] <= prices[0]) return false;
        if (prices[0] <= 0) return false;

        return true;
    }
}