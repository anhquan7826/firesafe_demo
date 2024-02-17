using Application.Services;
using Application.Services.Interface;
using Asp.Versioning;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Service.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firesafe.Service.Controllers;

/// <summary>
///     Controller for getting countries data.
/// </summary>
[AllowAnonymous]
[ApiVersion("1.0")]
public class CountryController(ICountryService service, IMediatorHandler mediatorHandler)
    : BaseController(mediatorHandler)
{
    /// <summary>
    ///     Get all valid countries with their Id.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    public Task<IActionResult> GetAllCountries()
    {
        return Perform(() => Task.FromResult<IActionResult>(Ok(new
        {
            Countries = service.GetAllCountries()
        })));
    }
}