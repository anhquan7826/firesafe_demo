using Application.Services.Impl;
using Application.Services.Interface;
using FluentValidation;

namespace Application.ViewModels.Requests.GetSupplierProfile;

public class GetSupplierProfileValidator : AbstractValidator<GetSupplierProfileRequest>
{
    private readonly ISupplierService _supplierService;

    public GetSupplierProfileValidator(ISupplierService supplierService)
    {
        _supplierService = supplierService;
        RuleFor(x => x.Id).Must(id => BeAValidSupplier((Guid)id!)).When(x => x.Id != null)
            .WithMessage("Supplier is not exist");
    }

    private bool BeAValidSupplier(Guid id)
    {
        return _supplierService.IsExist(id);
    }
}