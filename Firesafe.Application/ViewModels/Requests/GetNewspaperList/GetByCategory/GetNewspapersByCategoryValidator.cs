using Application.ViewModels.Requests.GetNewspaperList.GetAll;
using FluentValidation;

namespace Application.ViewModels.Requests.GetNewspaperList.GetByCategory;

public class GetNewspapersByCategoryValidator : AbstractValidator<GetNewspapersByCategoryRequest>
{
    public GetNewspapersByCategoryValidator()
    {
        RuleFor(x => x.Page).NotNull().NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than 1!");
    }
}