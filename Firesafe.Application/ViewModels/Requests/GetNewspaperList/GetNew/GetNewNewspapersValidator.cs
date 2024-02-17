using Application.ViewModels.Requests.GetNewspaperList.GetAll;
using FluentValidation;

namespace Application.ViewModels.Requests.GetNewspaperList.GetNew;

public class GetNewNewspapersValidator : AbstractValidator<GetNewNewspapersRequest>
{
    public GetNewNewspapersValidator()
    {
        RuleFor(x => x.Page).NotNull().NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than 1!");
    }
}