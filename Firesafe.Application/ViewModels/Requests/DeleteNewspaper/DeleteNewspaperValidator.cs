using Application.Services.Interface;
using Application.ViewModels.Requests.GetNewspaper;
using FluentValidation;

namespace Application.ViewModels.Requests.DeleteNewspaper;

public class DeleteNewspaperValidator : AbstractValidator<DeleteNewspaperRequest>
{
    private readonly INewspaperService _newspaperService;

    public DeleteNewspaperValidator(INewspaperService newspaperService)
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