using FluentValidation;

namespace Application.ViewModels.Requests.GetNewspaperList.GetAll;

public class GetAllNewspaperValidator : AbstractValidator<GetAllNewspapersRequest>
{
    public GetAllNewspaperValidator()
    {
        RuleFor(x => x.Page).NotNull().NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than 1!");
    }
}