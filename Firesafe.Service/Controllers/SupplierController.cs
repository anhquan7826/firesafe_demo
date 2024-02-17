using Application.DTOs;
using Application.Services;
using Application.Services.Interface;
using Application.ViewModels.Requests.AddProduct;
using Application.ViewModels.Requests.DeleteProduct;
using Application.ViewModels.Requests.EditProduct;
using Application.ViewModels.Requests.EditSupplierProfile;
using Application.ViewModels.Requests.GetSupplierProducts;
using Application.ViewModels.Requests.GetSupplierProfile;
using Asp.Versioning;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Service.Core;
using FluentValidation;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firesafe.Service.Controllers;

/// <summary>
///     Controller for Suppliers.
/// </summary>
[Authorize(Policy = FiresafePolicy.IsASupplier)]
[ApiVersion("1.0")]
public class SupplierController(
    IMediatorHandler mediatorHandler,
    IValidator<AddProductRequest> addValidator,
    IValidator<GetSupplierProductsRequest> getProductsValidator,
    IValidator<DeleteProductRequest> deleteProductValidator,
    IValidator<GetSupplierProfileRequest> getSupplierProfileValidator,
    IValidator<EditSupplierProfileRequest> editSupplierProfileValidator,
    ISupplierService supplierService)
    : BaseController(mediatorHandler)
{
    /// <summary>
    /// Register a new supplier.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    [Authorize(Policy = FiresafePolicy.MustBeRegistered)]
    public Task<IActionResult> RegisterSupplier()
    {
        return Perform(async () =>
        {
            await supplierService.RegisterSupplier(CurrentUserId);
            return Created();
        });
    }

    /// <summary>
    /// Edit supplier profile.
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [Route("edit-profile")]
    public Task<IActionResult> EditProfile([FromForm] EditSupplierProfileRequest request)
    {
        return Perform(request, editSupplierProfileValidator, async () =>
        {
            await supplierService.EditSupplierProfile(CurrentSupplierId, request.Name, request.Description,
                request.EstablishedAt, address: request.Address, avatar: request.Avatar, banner: request.Banner);
            return Created();
        });
    }

    /// <summary>
    /// Get supplier profile.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [AllowAnonymous]
    [Route("profile")]
    public Task<IActionResult> GetSupplierProfile([FromQuery] GetSupplierProfileRequest request)
    {
        return Perform(request, getSupplierProfileValidator, () =>
        {
            try
            {
                var supplier = supplierService.GetSupplier(request.Id ?? CurrentSupplierId);
                return Task.FromResult<IActionResult>(Ok(new
                {
                    Supplier = supplier
                }));
            }
            catch (BaseService.ServiceException e)
            {
                return Task.FromResult<IActionResult>(BadRequest(Error([e.Message])));
            }
        });
    }

    /// <summary>
    ///     Add a new product.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Route("add-product")]
    public Task<IActionResult> AddProduct([FromForm] AddProductRequest request)
    {
        return Perform(request, addValidator, async () =>
        {
            var id = await supplierService.AddProduct(
                CurrentSupplierId,
                request
            );
            return Ok(new
            {
                ProductId = id
            });
        });
    }

    /// <summary>
    ///     Edit an existing product. (Not implemented)
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut]
    [Route("edit-product")]
    public Task<IActionResult> EditProduct([FromBody] EditProductRequest request)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Get all products published by the current suppliers.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("products")]
    [AllowAnonymous]
    public Task<IActionResult> GetProducts([FromQuery] GetSupplierProductsRequest request)
    {
        return Perform(request, getProductsValidator, () =>
        {
            try
            {
                var supplierId = request.Id ?? CurrentSupplierId;
                return Task.FromResult<IActionResult>(Ok(new
                {
                    Products = supplierService.GetSupplierProducts(supplierId, request.Page - 1).Cast<ProductShortDto>()
                }));
            }
            catch (BaseService.ServiceException e)
            {
                return Task.FromResult<IActionResult>(BadRequest(Error([e.Message])));
            }
        });
    }

    /// <summary>
    ///     Delete a product.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("delete-product")]
    public Task<IActionResult> DeleteProduct([FromQuery] DeleteProductRequest request)
    {
        return Perform(request, deleteProductValidator, async () =>
        {
            try
            {
                await supplierService.DeleteProduct(CurrentSupplierId, request.Id);
                return Ok();
            }
            catch (BaseService.ServiceException e)
            {
                return Unauthorized(Error([e.Message]));
            }
        });
    }
}