using Application.DTOs;
using Application.Services;
using Application.Services.Interface;
using Application.ViewModels.Requests.SetRole;
using Asp.Versioning;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Service.Core;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firesafe.Service.Controllers;

/// <summary>
///     Controller for managing roles.
/// </summary>
[Authorize(Policy = FiresafePolicy.MustBeRegistered)]
[ApiVersion("1.0")]
public class RoleController(
    IRoleService roleService,
    IMediatorHandler mediatorHandler,
    IValidator<SetRolesRequest> setRoleValidator)
    : BaseController(mediatorHandler)
{
    /// <summary>
    ///     Get all available user roles.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("available")]
    [Authorize(Policy = FiresafePolicy.FirebaseAuthenticated)]
    public Task<IActionResult> GetAvailableRoles()
    {
        return Perform(() => Task.FromResult<IActionResult>(Ok(new
        {
            Roles = roleService.GetAllRoles()
        })));
    }

    /// <summary>
    ///     Set the role for current user.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("set")]
    public Task<IActionResult> SetRoles([FromForm] SetRolesRequest request)
    {
        return Perform(request, setRoleValidator, () =>
        {
            roleService.SetRole(CurrentUserId, request.Roles);
            return Task.FromResult<IActionResult>(Created());
        });
    }
}