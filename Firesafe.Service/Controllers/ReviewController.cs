using Asp.Versioning;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Service.Core;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firesafe.Service.Controllers;

/// <summary>
/// Controller for managing reviews.
/// </summary>
[Authorize(Policy = FiresafePolicy.MustBeRegistered)]
[ApiVersion("1.0")]
public class ReviewController(IMediatorHandler mediatorHandler) : BaseController(mediatorHandler)
{
    /// <summary>
    /// Get product reviews. (Not implemented)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("product")]
    public Task<IActionResult> GetProductReviews()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Add review to a product. (Not implemented)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost]
    [Route("review-product")]
    public Task<IActionResult> AddProductReview()
    {
        throw new NotImplementedException();
    }
}