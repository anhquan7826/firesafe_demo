using Application.Middlewares;
using Application.Services;
using Application.Services.Interface;
using Application.ViewModels.Requests.FcmToken;
using Asp.Versioning;
using Firebase.Auth;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Service.Core;
using FluentValidation;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firesafe.Service.Controllers;

/// <summary>
///     Controller for user authentication.
/// </summary>
[Authorize(Policy = FiresafePolicy.FirebaseAuthenticated)]
[ApiVersion("1.0")]
public class AuthenticationController(
    IUserService userService,
    IFirebaseAuth firebaseAuth,
    IValidator<FcmTokenRequest> fcmTokenValidator,
    IMediatorHandler mediator)
    : BaseController(mediator)
{
    /// <summary>
    ///     Check if user is registered. The authentication part is handled by Firebase at client side.
    /// </summary>
    /// <returns>User data.</returns>
    [HttpGet]
    [Authorize(Policy = FiresafePolicy.MustBeRegistered)]
    [Route("sign-in")]
    public Task<IActionResult> SignIn()
    {
        return Perform(() =>
        {
            userService.GetUserFromFirebaseId(CurrentFid!);
            return Task.FromResult<IActionResult>(Ok());
        });
    }

    /// <summary>
    ///     Register a new user.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("sign-up")]
    public Task<IActionResult> SignUp()
    {
        return Perform(async () =>
        {
            try
            {
                await userService.AddUser(CurrentFid!);
                return Created();
            }
            catch (BaseService.ServiceException e)
            {
                return Conflict(Error([e.Message]));
            }
        });
    }

    /// <summary>
    ///     Get token from email and password.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [Route("get-token")]
    public async Task<IActionResult> GetToken([FromQuery] string email, [FromQuery] string password)
    {
        try
        {
            var token = await firebaseAuth.SignInWithEmailAndPassword(email, password);
            if (token == null) return Unauthorized(Error(["Wrong email or password!"]));
            return Ok(token);
        }
        catch (FirebaseAuthException)
        {
            return Unauthorized(Error(["Wrong email or password!"]));
        }
    }


    /// <summary>
    /// Register device FCM Token for push notification.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("fcm-register")]
    [Authorize(Policy = FiresafePolicy.MustBeRegistered)]
    public Task<IActionResult> RegisterFcmToken([FromBody] FcmTokenRequest request)
    {
        return Perform(request, fcmTokenValidator, async () =>
        {
            try
            {
                await userService.AddDevice(CurrentUserId, request.Token);
                return Created();
            }
            catch (BaseService.ServiceException e)
            {
                return InternalError(Error([e.Message]));
            }
        });
    }

    /// <summary>
    /// Unregister device FCM Token from push notification.
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("fcm-unregister")]
    [Authorize(Policy = FiresafePolicy.MustBeRegistered)]
    public Task<IActionResult> UnregisterFcmToken([FromBody] FcmTokenRequest request)
    {
        return Perform(request, fcmTokenValidator, async () =>
        {
            try
            {
                await userService.RemoveDevice(CurrentUserId, request.Token);
                return Ok();
            }
            catch (BaseService.ServiceException e)
            {
                return InternalError(Error([e.Message]));
            }
        });
    }
}