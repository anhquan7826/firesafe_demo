using Application.Services.Interface;
using FluentValidation;

namespace Application.ViewModels.Requests.GetNewspaper;

public class GetNewspaperValidator : AbstractValidator<GetNewspaperRequest>
{
    private readonly INewspaperService _newspaperService;

    public GetNewspaperValidator(INewspaperService newspaperService)
    {
        _newspaperService = newspaperService;
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id cannot be empty!").Must(BeAValidNewspaper).WithMessage(
            "Newspaper id is not exist!");
    }

    private bool BeAValidNewspaper(Guid id)
    {
        return _newspaperService.IsExist(id);
    }
}