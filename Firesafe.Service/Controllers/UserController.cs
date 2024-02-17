using Application.DTOs;
using Application.Services;
using Application.Services.Interface;
using Application.ViewModels.Requests.EditUserProfile;
using Application.ViewModels.Requests.SetRole;
using Asp.Versioning;
using AutoMapper;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Service.Core;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firesafe.Service.Controllers;

/// <summary>
///     Controller for operations on a generic user.
/// </summary>
[Authorize(Policy = FiresafePolicy.MustBeRegistered)]
[ApiVersion("1.0")]
public class UserController(
    IMediatorHandler mediatorHandler,
    IUserService userService,
    IValidator<EditProfileRequest> editProfileValidator) : BaseController(mediatorHandler)
{
    /// <summary>
    ///     Get the userdata of the current user.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("get")]
    public Task<IActionResult> GetUserData()
    {
        return Perform(() =>
        {
            try
            {
                var user = userService.GetUserFromFirebaseId(CurrentFid!);
                return Task.FromResult<IActionResult>(Ok(new
                {
                    User = user
                }));
            }
            catch (BaseService.ServiceException e)
            {
                return Task.FromResult<IActionResult>(BadRequest(Error([e.Message])));
            }
        });
    }

    /// <summary>
    /// Edit the current user profile.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("edit-profile")]
    public Task<IActionResult> EditProfile([FromForm] EditProfileRequest request)
    {
        return Perform(request, editProfileValidator, async () =>
        {
            await userService.EditProfile(CurrentUserId, request.Name, request.Avatar, request.Banner);
            return Ok();
        });
    }
}