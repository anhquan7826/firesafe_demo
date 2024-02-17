using Application.Services;
using Application.Services.Interface;
using FluentValidation;

namespace Application.ViewModels.Requests.GetProductDetail;

public class GetProductDetailValidator : AbstractValidator<GetProductDetailRequest>
{
    private IProductService _productService;

    public GetProductDetailValidator(IProductService productService)
    {
        _productService = productService;
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Product Id is not valid!").Must(BeAValidProductId)
            .WithMessage("Product is not exist!");
    }

    private bool BeAValidProductId(Guid productId)
    {
        return _productService.IsExist(productId);
    }
}