using Application.DTOs;
using Application.Middlewares;
using Application.Services.Interface;
using Application.ViewModels.Requests.AddProduct;
using AutoMapper;
using Firesafe.Domain.Commands;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Domain.Entities;
using Firesafe.Domain.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Impl;

public class SupplierService(
    IUnitOfWork uow,
    IMapper mapper,
    IMediatorHandler mediatorHandler,
    ICloudStorage cloudStorage) : BaseService, ISupplierService
{
    public bool IsExist(Guid id)
    {
        var supplier = uow.SupplierRepository.GetById(id);
        return supplier != null;
    }

    public async Task RegisterSupplier(Guid userId)
    {
        await mediatorHandler.SendCommand(new RegisterSupplierCommand
        {
            UserId = userId
        });
    }

    public async Task EditSupplierProfile(Guid supplierId, string? name = null, string? description = null,
        DateOnly? establishedAt = null, IFormFile? avatar = null, IFormFile? banner = null, string? address = null)
    {
        await mediatorHandler.SendCommand(new EditSupplierProfileCommand
        {
            SupplierId = supplierId,
            Name = name?.Trim(),
            EstablishedAt = establishedAt,
            Description = description?.Trim(),
            Address = address
        });
        cloudStorage.SupplierStorage.UploadSupplierImages(supplierId, avatar, banner);
    }

    public SupplierDto GetSupplier(Guid id)
    {
        if (id == Guid.Empty) NotifyError("Invalid supplier Id!");
        return MapToDto(uow.SupplierRepository.GetById(id)!);
    }

    public SupplierDto GetSupplierOfProduct(Guid productId)
    {
        return MapToDto(
            uow.ProductRepository.GetByProductId(productId)!
        );
    }

    public List<ProductShortDto> GetSupplierProducts(Guid id, int page)
    {
        if (id.Equals(Guid.Empty)) NotifyError("You must be a supplier or a supplier Id must be specified!");
        return uow.ProductRepository.GetBySupplierPaged(id, page).Select(product =>
        {
            var dto = mapper.Map<ProductShortDto>(product);
            dto.Thumbnail = cloudStorage.ProductStorage.GetProductThumbnailUrl(product.ProductId)!;
            return dto;
        }).ToList();
    }

    public async Task<Guid> AddProduct(Guid supplierId, AddProductRequest request)
    {
        var pid = Guid.NewGuid();
        var cid = request.CertificateNames.Select(_ => Guid.NewGuid()).ToList();
        var iid = request.Images?.Select(_ => Guid.NewGuid()).ToList() ?? [];
        var command = mapper.Map<AddNewProductCommand>(request);
        command.ProductId = pid;
        command.SupplierId = supplierId;
        command.Certificates = cid.Zip(request.CertificateNames).ToList();
        command.ImageIds = iid;
        await mediatorHandler.SendCommand(command);
        cloudStorage.ProductStorage.UploadProductImages(
            pid,
            request.Thumbnail,
            cid.Zip(request.CertificateImages).ToDictionary(),
            request.Images == null
                ? null
                : iid
                    .Zip(request.Images)
                    .ToDictionary()
        );
        return pid;
    }

    public void EditProduct(Guid productId, string? name, string? specification, short? year, short? inStock,
        int? price,
        int? priceMax, int? priceMin, IEnumerable<string>? categories)
    {
        throw new NotImplementedException();
    }


    public async Task DeleteProduct(Guid supplierId, Guid productId)
    {
        var product = uow.ProductRepository.GetById(productId)!;
        if (product.SupplierId != supplierId) NotifyError("You dont have the permission to delete this product!");
        await mediatorHandler.SendCommand(new DeleteProductCommand
        {
            ProductId = productId
        });
        cloudStorage.ProductStorage.DeleteProductFiles(productId);
    }
    
    private SupplierDto MapToDto(Supplier supplier)
    {
        var dto = mapper.Map<SupplierDto>(supplier);
        dto.Avatar = cloudStorage.SupplierStorage.GetSupplierAvatarUrl(supplier.SupplierId);
        dto.Banner = cloudStorage.SupplierStorage.GetSupplierBannerUrl(supplier.SupplierId);
        return dto;
    }
}