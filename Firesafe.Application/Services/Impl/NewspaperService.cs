using Application.DTOs;
using Application.Middlewares;
using Application.Services.Interface;
using Application.Utils;
using AutoMapper;
using Firesafe.Domain.Commands;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Domain.UnitOfWork;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services.Impl;

public class NewspaperService(
    IUnitOfWork uow,
    ICloudStorage cloudStorage,
    IMapper mapper,
    IMediatorHandler mediatorHandler) : BaseService, INewspaperService
{
    public bool IsExist(Guid id)
    {
        return uow.NewspaperRepository.GetById(id) != null;
    }

    public NewspaperDto GetNewspaper(Guid id)
    {
        var n = uow.NewspaperRepository.GetById(id)!;
        var dto = mapper.Map<NewspaperDto>(n);
        dto.Thumbnail = cloudStorage.NewspaperStorage.GetNewspaperThumbnailUrl(id)!;
        dto.Content = cloudStorage.NewspaperStorage.GetNewspaperContent(id)!;
        return dto;
    }

    public async Task AddNewspaper(string title, IFormFile thumbnail, IFormFile html, List<IFormFile> images, List<string> categories)
    {
        var id = Guid.NewGuid();
        var imageIds = images.Select(_ => Guid.NewGuid()).ToList();
        await mediatorHandler.SendCommand(new AddNewNewspaperCommand
        {
            NewspaperId = id,
            Title = title,
            NewspaperImageIds = imageIds,
            NewspaperCategories = categories
        });
        cloudStorage.NewspaperStorage.UploadNewspaper(id, null, thumbnail, imageIds.Zip(
                images, (k, v) => new { Key = k, Value = v }).ToDictionary(item => item.Key, item => item.Value)
        );

        var doc = ModifyHtml(html,
            cloudStorage.NewspaperStorage.GetNewspaperImageUrls(id, imageIds).Select(url => url!).ToList());
        cloudStorage.NewspaperStorage.UploadNewspaper(id, doc, null, null);
    }

    public Task EditNewspaper(Guid id, string? title = null, IFormFile? thumbnail = null, IFormFile? html = null,
        List<IFormFile>? images = null)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteNewspaper(Guid id)
    {
        await mediatorHandler.SendCommand(new DeleteNewspaperCommand
        {
            NewspaperId = id
        });
        cloudStorage.NewspaperStorage.DeleteNewspaperFiles(id);
    }

    public List<NewspaperShortDto> GetAllNewspapers(int page, DateTime timestamp)
    {
        var result = uow.NewspaperRepository.GetAll().Where(n => n.CreatedAt <= timestamp)
            .OrderBy(n => n.EditedAt > n.CreatedAt ? n.EditedAt : n.CreatedAt).Skip(page * 10).Take(10);
        return result.ToList().Select(n =>
        {
            var dto = mapper.Map<NewspaperShortDto>(n);
            dto.Thumbnail = cloudStorage.NewspaperStorage.GetNewspaperThumbnailUrl(dto.NewspaperId) ?? "";
            return dto;
        }).ToList();
    }

    public List<NewspaperShortDto> GetNewNewspapers(int page, DateTime timestamp)
    {
        var result = uow.NewspaperRepository.GetAll()
            .Where(n => n.CreatedAt <= timestamp && n.CreatedAt >= timestamp.AddDays(-7))
            .OrderBy(n => n.EditedAt > n.CreatedAt ? n.EditedAt : n.CreatedAt).Skip(page * 10).Take(10);
        return result.ToList().Select(n =>
        {
            var dto = mapper.Map<NewspaperShortDto>(n);
            dto.Thumbnail = cloudStorage.NewspaperStorage.GetNewspaperThumbnailUrl(dto.NewspaperId) ?? "";
            return dto;
        }).ToList();
    }

    public List<NewspaperShortDto> GetNewspapersByCategory(int page, DateTime timestamp)
    {
        var result = uow.NewspaperRepository
            .GetAll()
            .Where(n => n.CreatedAt <= timestamp)
            .Include(n => n.NewspaperCategories)
            .Where(n =>
                n.NewspaperCategories.Any(nc => nc.NewspaperCategoryId == "newspaper_category_product_category")
            )
            .OrderBy(n => n.EditedAt > n.CreatedAt ? n.EditedAt : n.CreatedAt).Skip(page * 10).Take(10);
        return result.ToList().Select(n =>
        {
            var dto = mapper.Map<NewspaperShortDto>(n);
            dto.Thumbnail = cloudStorage.NewspaperStorage.GetNewspaperThumbnailUrl(dto.NewspaperId) ?? "";
            return dto;
        }).ToList();
    }

    public List<NewspaperShortDto> GetAllNewspapersByField(int page, DateTime timestamp)
    {
        var result = uow.NewspaperRepository
            .GetAll()
            .Where(n => n.CreatedAt <= timestamp)
            .Include(n => n.NewspaperCategories)
            .Where(n =>
                n.NewspaperCategories.Any(nc => nc.NewspaperCategoryId == "newspaper_category_field")
            )
            .OrderBy(n => n.EditedAt > n.CreatedAt ? n.EditedAt : n.CreatedAt).Skip(page * 10).Take(10);
        return result.ToList().Select(n =>
        {
            var dto = mapper.Map<NewspaperShortDto>(n);
            dto.Thumbnail = cloudStorage.NewspaperStorage.GetNewspaperThumbnailUrl(dto.NewspaperId) ?? "";
            return dto;
        }).ToList();
    }


    private HtmlDocument ModifyHtml(IFormFile html, List<string> imageUrls)
    {
        var content = html.ReadAllText();
        var doc = new HtmlDocument();
        doc.LoadHtml(content);
        var nodes = doc.DocumentNode.SelectNodes("//img");
        if (nodes != null)
            for (var i = 0; i < imageUrls.Count; i++)
                try
                {
                    nodes[i].Attributes.RemoveAll();
                    nodes[i].SetAttributeValue("src", imageUrls[i]);
                }
                catch (Exception)
                {
                    break;
                }

        return doc;
    }
}