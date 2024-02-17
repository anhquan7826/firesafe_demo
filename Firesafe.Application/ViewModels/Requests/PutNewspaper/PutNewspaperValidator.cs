using Application.Services.Interface;
using FluentValidation;

namespace Application.ViewModels.Requests.PutNewspaper;

public class PutNewspaperValidator : AbstractValidator<PutNewspaperRequest>
{
    private readonly INewspaperService _newspaperService;
    private readonly ICategoryService _categoryService;

    public PutNewspaperValidator(INewspaperService newspaperService, ICategoryService categoryService)
    {
        _newspaperService = newspaperService;
        _categoryService = categoryService;
        RuleFor(x => x.Id).Must(id => BeAValidNewspaper((Guid)id!)).When(x => x.Id != null)
            .WithMessage("Newspaper Id is not exist!");
        RuleFor(x => x.Title).NotNull().NotEmpty().When(IsAdd).WithMessage("Title must not be empty when adding new!");
        RuleFor(x => x.Thumbnail).NotNull().NotEmpty().When(IsAdd)
            .WithMessage("Thumbnail must not be empty when adding new!");
        RuleFor(x => x.NewspaperCategories).NotNull().NotEmpty().When(IsAdd)
            .WithMessage("At least one category must be specified!").Must(BeAValidNewspaperCategory!)
            .WithMessage("Newspaper category is not exist!");
    }

    private bool IsAdd(PutNewspaperRequest request)
    {
        return request.Id == null;
    }

    private bool BeAValidNewspaper(Guid id)
    {
        return _newspaperService.IsExist(id);
    }

    private bool BeAValidNewspaperCategory(List<string> categories)
    {
        return categories.All(category => _categoryService.IsNewspaperCategoryExist(category));
    }
}