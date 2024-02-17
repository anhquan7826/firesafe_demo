using Application.Services;
using Application.Services.Interface;
using FluentValidation;

namespace Application.ViewModels.Requests.GetSupplierProducts;

public class GetSupplierProductValidator : AbstractValidator<GetSupplierProductsRequest>
{
    private readonly ISupplierService _service;

    public GetSupplierProductValidator(ISupplierService service)
    {
        _service = service;
        RuleFor(x => x.Id).Must(BeAValidSupplier).When(id => id != null).WithMessage("Invalid Supplier id!");
        RuleFor(x => x.Page).NotNull().GreaterThanOrEqualTo(1).WithMessage("Page cannot be less than 1!");
    }

    private bool BeAValidSupplier(Guid? id)
    {
        return id == null || _service.IsExist((Guid)id);
    }
}