using Application.DTOs;
using Application.Middlewares;
using Application.Services.Interface;
using AutoMapper;
using Firesafe.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Impl;

public class ProductService(
    IMapper mapper,
    ICloudStorage cloudStorage,
    IUnitOfWork uow)
    : BaseService, IProductService
{
    public bool IsExist(Guid productId)
    {
        return uow.ProductRepository.GetById(productId) != null;
    }

    public bool IsOwnedBy(Guid productId, Guid supplierId)
    {
        var product = uow.ProductRepository.GetById(productId)!;
        return product.SupplierId == supplierId;
    }

    public IEnumerable<ProductShortDto> GetByCategoryPaged(string category, int page, DateTime timestamp)
    {
        var products = uow.ProductRepository.GetByCategoryPaged(category.Trim(), page, timestamp);
        return products.Select(product =>
        {
            var dto = mapper.Map<ProductShortDto>(product);
            dto.Thumbnail = cloudStorage.ProductStorage.GetProductThumbnailUrl(product.ProductId)!;
            return dto;
        });
    }

    public IEnumerable<ProductShortDto> SearchProduct(
        DateTime timestamp,
        string query,
        int page = 0,
        List<string>? categories = null,
        int? year = null,
        short? rating = null,
        List<int>? priceRange = null)
    {
        var set = uow.ProductRepository
            .GetAll()
            .Where(p => p.CreatedAt < timestamp)
            .Include(p => p.Categories)
            .AsQueryable();

        if (categories != null) set = set.Where(p => p.Categories.Any(c => categories.Contains(c.CategoryId)));

        if (year != null) set = set.Where(p => p.CreatedAt.Year >= year);

        if (rating != null) set = set.Where(p => p.Rating >= rating);

        if (priceRange != null)
            set = set.Where(p =>
                p.Price != null ? priceRange[0] <= p.Price && p.Price <= priceRange[1] :
                p.PriceMax != null ? priceRange[0] <= p.PriceMin && p.PriceMax <= priceRange[1] :
                priceRange[0] <= p.PriceMin && p.PriceMin <= priceRange[1]);

        var result = set.ToList().OrderBy(p => CalculateLevenshteinDistance(query.Trim(), p.Name)).Skip(10 * page)
            .Take(10)
            .ToList();
        return result.Select(product =>
        {
            var dto = mapper.Map<ProductShortDto>(product);
            dto.Thumbnail = cloudStorage.ProductStorage.GetProductThumbnailUrl(product.ProductId)!;
            return dto;
        });
    }

    public ProductDto GetProduct(Guid productId)
    {
        var product = uow.ProductRepository.GetProduct(productId)!;
        var dto = mapper.Map<ProductDto>(product);

        var cid = product.ProductCertificates;
        var iid = product.ProductImages.Select(pi => pi.ProductImageId).ToList();

        dto.Categories = product.Categories.Select(c => c.CategoryId).ToList();
        
        dto.Thumbnail = cloudStorage.ProductStorage.GetProductThumbnailUrl(product.ProductId)!;
        dto.Images = cloudStorage.ProductStorage.GetProductImageUrls(product.ProductId, iid)
            .Where(url => url != null).Select(url => url!).ToList();
        dto.Certificates = cid.Select(c => new ProductDto.ProductCertificateDto
        {
            Name = c.Name,
            Image = cloudStorage.ProductStorage.GetProductCertificateUrl(product.ProductId, c.ProductCertificateId) ??
                    ""
        }).ToList();
        return dto;
    }
    
    private int CalculateLevenshteinDistance(string str1, string str2)
    {
        var distance = new int[str1.Length + 1, str2.Length + 1];

        for (var i = 0; i <= str1.Length; i++)
            distance[i, 0] = i;

        for (var j = 0; j <= str2.Length; j++)
            distance[0, j] = j;

        for (var i = 1; i <= str1.Length; i++)
        for (var j = 1; j <= str2.Length; j++)
        {
            var cost = str2[j - 1] == str1[i - 1] ? 0 : 1;

            distance[i, j] = Math.Min(
                Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                distance[i - 1, j - 1] + cost
            );
        }

        return distance[str1.Length, str2.Length];
    }
}