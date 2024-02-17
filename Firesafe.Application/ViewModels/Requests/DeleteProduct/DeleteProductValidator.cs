using Application.Services;
using Application.Services.Interface;
using FluentValidation;

namespace Application.ViewModels.Requests.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductRequest>
{
    private readonly IProductService _service;

    public DeleteProductValidator(IProductService service)
    {
        _service = service;
        RuleFor(x => x.Id).NotNull().NotEmpty().Must(BeAValidProduct).WithMessage("Product is not exist!");
    }

    private bool BeAValidProduct(Guid id)
    {
        return _service.IsExist(id);
    }
}