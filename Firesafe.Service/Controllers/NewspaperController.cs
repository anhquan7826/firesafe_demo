using Application.Middlewares;
using Application.Services;
using Application.Services.Interface;
using Application.ViewModels.Requests.DeleteNewspaper;
using Application.ViewModels.Requests.GetNewspaper;
using Application.ViewModels.Requests.GetNewspaperList.GetAll;
using Application.ViewModels.Requests.GetNewspaperList.GetByCategory;
using Application.ViewModels.Requests.GetNewspaperList.GetByField;
using Application.ViewModels.Requests.GetNewspaperList.GetNew;
using Application.ViewModels.Requests.PutNewspaper;
using Asp.Versioning;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Service.Core;
using FluentValidation;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firesafe.Service.Controllers;

/// <summary>
/// Controller for managing news.
/// </summary>
[Authorize(Policy = FiresafePolicy.MustBeRegistered)]
[ApiVersion("1.0")]
public class NewspaperController(
    INewspaperService service,
    IMediatorHandler mediatorHandler,
    IValidator<GetNewspaperRequest> getNewspaperValidator,
    IValidator<PutNewspaperRequest> putNewspaperValidator,
    IValidator<DeleteNewspaperRequest> deleteNewspaperValidator,
    IValidator<GetAllNewspapersRequest> getAllValidator,
    IValidator<GetNewNewspapersRequest> getNewValidator,
    IValidator<GetNewspapersByCategoryRequest> getByCategoryValidator,
    IValidator<GetNewspapersByFieldRequest> getByFieldValidator) : BaseController(mediatorHandler)
{
    /// <summary>
    /// Get news.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("get")]
    public Task<IActionResult> GetNewspaper([FromQuery] GetNewspaperRequest request)
    {
        return Perform(request, getNewspaperValidator, () =>
        {
            var newspaper = service.GetNewspaper(request.Id);
            return Task.FromResult<IActionResult>(Ok(new
            {
                Newspaper = newspaper
            }));
        });
    }

    /// <summary>
    /// Put or edit existing newspaper.
    /// if id is specified, the existing newspaper with that id is edited, else
    /// a new newspaper will be added.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("put")]
    public Task<IActionResult> PutNewspaper([FromForm] PutNewspaperRequest request)
    {
        return Perform(request, putNewspaperValidator, async () =>
        {
            if (request.Id == null)
                await service.AddNewspaper(
                    request.Title!,
                    request.Thumbnail!,
                    request.Content!,
                    request.Images ?? [],
                    request.NewspaperCategories!
                );
            return Created();
        });
    }

    /// <summary>
    /// Delete a newspaper.
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("delete")]
    public Task<IActionResult> DeleteNewspaper([FromQuery] DeleteNewspaperRequest request)
    {
        return Perform(request, deleteNewspaperValidator, async () =>
        {
            await service.DeleteNewspaper(request.Id);
            return Ok();
        });
    }

    /// <summary>
    /// Get all newspapers.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("all")]
    public Task<IActionResult> GetAll([FromQuery] GetAllNewspapersRequest request)
    {
        return Perform(request, getAllValidator, () =>
        {
            SetRequestTimestamp("get_all_newspapers_timestamp", request.Page == 1);
            var result =
                service.GetAllNewspapers(request.Page - 1, GetRequestTimestamp("get_all_newspapers_timestamp"));
            return Task.FromResult<IActionResult>(Ok(new
            {
                Newspapers = result
            }));
        });
    }

    /// <summary>
    /// Get new newspapers.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("new")]
    public Task<IActionResult> GetNew([FromQuery] GetNewNewspapersRequest request)
    {
        return Perform(request, getNewValidator, () =>
        {
            SetRequestTimestamp("get_new_newspapers_timestamp", request.Page == 1);
            var result =
                service.GetNewNewspapers(request.Page - 1, GetRequestTimestamp("get_new_newspapers_timestamp"));
            return Task.FromResult<IActionResult>(Ok(new
            {
                Newspapers = result
            }));
        });
    }

    /// <summary>
    /// Get by product categories.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("product-category")]
    public Task<IActionResult> GetByProductCategory([FromQuery] GetNewspapersByCategoryRequest request)
    {
        return Perform(request, getByCategoryValidator, () =>
        {
            SetRequestTimestamp("get_newspapers_by_category_timestamp", request.Page == 1);
            var result = service.GetNewspapersByCategory(request.Page - 1,
                GetRequestTimestamp("get_newspapers_by_category_timestamp"));
            return Task.FromResult<IActionResult>(Ok(new
            {
                Newspapers = result
            }));
        });
    }

    /// <summary>
    /// Get by fields.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Route("field")]
    public Task<IActionResult> GetByField([FromQuery] GetNewspapersByFieldRequest request)
    {
        return Perform(request, getByFieldValidator, () =>
        {
            SetRequestTimestamp("get_newspapers_by_field_timestamp", request.Page == 1);
            var result = service.GetAllNewspapersByField(request.Page - 1,
                GetRequestTimestamp("get_newspapers_by_field_timestamp"));
            return Task.FromResult<IActionResult>(Ok(new
            {
                Newspapers = result
            }));
        });
    }
}