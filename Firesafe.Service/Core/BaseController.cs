using Application.Utils;
using Application.ViewModels;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Domain.Entities.EventStore;
using Firesafe.Domain.Events.EventStore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Firesafe.Service.Core;

/// <summary>
///     The base controller.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController(IMediatorHandler mediator) : ControllerBase
{
    /// <summary>
    ///     Shorthand for creating new ErrorModel.
    /// </summary>
    /// <param name="messages"></param>
    /// <returns></returns>
    protected object Error(IEnumerable<string> messages)
    {
        return new
        {
            Success = false,
            Errors = messages
        };
    }

    /// <summary>
    /// Validate request.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="validator"></param>
    /// <typeparam name="TR"></typeparam>
    /// <typeparam name="TV"></typeparam>
    /// <returns></returns>
    protected async Task<IActionResult?> Validate<TR, TV>(TR request, TV validator)
        where TR : BaseRequestModel where TV : IValidator<TR>
    {
        var validation = await validator.ValidateAsync(request);
        validation.AddToModelState(ModelState);
        if (validation.IsValid) return null;
        var errors = ModelState.Values.SelectMany(v => v.Errors)
            .Select(e => e.Exception == null ? e.ErrorMessage : e.Exception.Message);
        return BadRequest(Error(errors));
    }

    /// <summary>
    /// Automatically perform the operation if validate result is success. Exceptions will be
    /// automatically published to Event Store.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="validator"></param>
    /// <param name="operation"></param>
    /// <typeparam name="TR"></typeparam>
    /// <typeparam name="TV"></typeparam>
    /// <returns></returns>
    protected async Task<IActionResult> Perform<TR, TV>(TR request, TV validator, Func<Task<IActionResult>> operation)
        where TR : BaseRequestModel where TV : IValidator<TR>
    {
        try
        {
            var v = await Validate(request, validator);
            if (v != null) return v;
            return await operation();
        }
        catch (Exception e)
        {
            await mediator.PublishEvent(new ExceptionOccuredEvent
            {
                Exception = new ExceptionStore
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace ?? ""
                }
            });
            return InternalError(Error(e.Message.Split("\n").Where(ex => !ex.IsNullOrEmpty())));
        }
    }

    /// <summary>
    /// Perform an operation with exceptions be automatically published to Event Store.
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    protected async Task<IActionResult> Perform(Func<Task<IActionResult>> operation)
    {
        try
        {
            return await operation();
        }
        catch (Exception e)
        {
            await mediator.PublishEvent(new ExceptionOccuredEvent
            {
                Exception = new ExceptionStore
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace ?? ""
                }
            });
            return InternalError(Error(e.Message.Split("\n").Where(ex => !ex.IsNullOrEmpty())));
        }
    }

    /// <summary>
    /// Set request timestamp to Session only if the condition is true.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="condition"></param>
    protected void SetRequestTimestamp(string key, bool condition)
    {
        if (!condition) return;
        var timestamp = DateTime.UtcNow.ToStringUtc();
        Session.SetString(key, timestamp);
    }

    /// <summary>
    /// Get the stored request timestamp from Session.
    /// </summary>
    /// <returns></returns>
    protected DateTime GetRequestTimestamp(string key)
    {
        var timestamp = Session.GetString(key);
        return timestamp != null ? DateTimeExtension.ParseUtc(timestamp) : DateTime.UtcNow;
    }

    /// <summary>
    ///     Get the current Firebase UID of this context.
    /// </summary>
    protected string? CurrentFid
    {
        get { return HttpContext.User.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value; }
    }

    /// <summary>
    ///     Get the current UserID of this context.
    /// </summary>
    protected Guid CurrentUserId
    {
        get
        {
            var value = HttpContext.User.FindFirst(c => c.Type == "UserId")?.Value;
            return value != null ? Guid.Parse(value) : Guid.Empty;
        }
    }

    /// <summary>
    ///     Get the current SupplierID of this context.
    /// </summary>
    protected Guid CurrentSupplierId
    {
        get
        {
            var value = HttpContext.User.FindFirst(c => c.Type == "SupplierId")?.Value;
            return value != null ? Guid.Parse(value) : Guid.Empty;
        }
    }

    /// <summary>
    /// Get the current session.
    /// </summary>
    private ISession Session => HttpContext.Session;

    /// <summary>
    /// Return an Status Code 500 Internal Server Error.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    protected IActionResult InternalError(object data)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, data);
    }
}