using Application.Services;
using Application.Services.Interface;
using Asp.Versioning;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Service.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firesafe.Service.Controllers;

/// <summary>
///     Controller for product category.
/// </summary>
/// [Authorize]
[AllowAnonymous]
[ApiVersion("1.0")]
public class CategoryController(ICategoryService categoryService, IMediatorHandler mediatorHandler)
    : BaseController(mediatorHandler)
{
    /// <summary>
    ///     Get all available product categories.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("product")]
    public Task<IActionResult> GetProductCategories()
    {
        return Perform(() =>
        {
            var categories = categoryService.GetProductCategories();
            return Task.FromResult<IActionResult>(Ok(new
            {
                Categories = categories
            }));
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("newspaper")]
    public Task<IActionResult> GetNewspaperCategories()
    {
        return Perform(() =>
        {
            var categories = categoryService.GetNewspaperCategories();
            return Task.FromResult<IActionResult>(Ok(new
            {
                Categories = categories
            }));
        });
    }
}