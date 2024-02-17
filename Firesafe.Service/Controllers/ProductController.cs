using Application.DTOs;
using Application.Services.Interface;
using Application.Utils;
using Application.ViewModels.Requests.GetProductDetail;
using Application.ViewModels.Requests.GetProducts;
using Application.ViewModels.Requests.SearchProducts;
using Asp.Versioning;
using AutoMapper;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Service.Core;
using FluentValidation;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Firesafe.Service.Controllers;

/// <summary>
///     Controller for product operations.
/// </summary>
[Authorize(Policy = FiresafePolicy.MustBeRegistered)]
[ApiVersion("1.0")]
public class ProductController(
    IMediatorHandler mediatorHandler,
    IProductService productService,
    ISupplierService supplierService,
    IValidator<GetProductsRequest> getValidator,
    IValidator<SearchProductsRequest> searchValidator,
    IValidator<GetProductDetailRequest> getDetailValidator)
    : BaseController(mediatorHandler)
{
    /// <summary>
    ///     Get product list pagination.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [Route("get")]
    public Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request)
    {
        return Perform(request, getValidator, () =>
        {
            SetRequestTimestamp("get_products_timestamp", request.Page == 1);

            var products = productService.GetByCategoryPaged(
                request.Category,
                request.Page - 1,
                GetRequestTimestamp("get_products_timestamp")
            );
            return Task.FromResult<IActionResult>(Ok(new
            {
                Products = products,
                Timestamp = GetRequestTimestamp("get_products_timestamp")
            }));
        });
    }

    /// <summary>
    ///     Get the detailed product info.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [Route("details")]
    public Task<IActionResult> GetProductDetail([FromQuery] GetProductDetailRequest request)
    {
        return Perform(request, getDetailValidator, () =>
        {
            var product = productService.GetProduct(request.Id);
            var supplier = supplierService.GetSupplierOfProduct(request.Id);
            return Task.FromResult<IActionResult>(Ok(new
            {
                Product = product,
                Supplier = (SupplierShortDto)supplier
            }));
        });
    }

    /// <summary>
    ///     Search for products.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("search")]
    [AllowAnonymous]
    public Task<IActionResult> SearchProduct([FromQuery] SearchProductsRequest request)
    {
        return Perform(request, searchValidator, () =>
        {
            SetRequestTimestamp("search_product_timestamp", request.Page == 1);

            var result = productService.SearchProduct(
                GetRequestTimestamp("search_product_timestamp"),
                request.Query,
                request.Page - 1,
                request.Categories?.Split(";").Where(c => !c.IsNullOrEmpty()).ToList(),
                request.Year,
                request.Rating,
                request.PriceRange?.Split(";").Where(c => !c.IsNullOrEmpty()).Select(int.Parse).ToList()
            ).Cast<ProductShortDto>();
            return Task.FromResult<IActionResult>(Ok(new
            {
                Result = result.ToList(),
                Timestamp = GetRequestTimestamp("search_product_timestamp")
            }));
        });
    }
}