using FluentValidation;

namespace Application.ViewModels.Requests.EditProduct;

public class EditProductValidator : AbstractValidator<EditProductRequest>
{
    public EditProductValidator()
    {
        RuleFor(x => x.ProductId).NotEqual(Guid.Empty).WithMessage("Product Id cannot be empty!");
    }
}