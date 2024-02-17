using Application.ViewModels.Requests.GetNewspaperList.GetAll;
using FluentValidation;

namespace Application.ViewModels.Requests.GetNewspaperList.GetByField;

public class GetNewspapersByFieldValidator : AbstractValidator<GetNewspapersByFieldRequest>
{
    public GetNewspapersByFieldValidator()
    {
        RuleFor(x => x.Page).NotNull().NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than 1!");
    }
}