using Asp.Versioning;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Service.Core;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firesafe.Service.Controllers;

/// <summary>
/// Controller for managing favorites.
/// </summary>
[Authorize(Policy = FiresafePolicy.MustBeRegistered)]
[ApiVersion("1.0")]
public class FavoriteController(IMediatorHandler mediatorHandler) : BaseController(mediatorHandler)
{
    /// <summary>
    /// Add this prduct to the current user's favorite products. (Not implemented)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPut]
    [Route("set-product")]
    public Task<IActionResult> AddProductToFavorite()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Get favorite products. (Not implemented)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("products")]
    public Task<IActionResult> GetFavoriteProducts()
    {
        throw new NotImplementedException();
    }
}