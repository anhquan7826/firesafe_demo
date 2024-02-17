using Application.DTOs;
using Application.ViewModels.Requests.AddProduct;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Interface;

public interface ISupplierService
{
    public bool IsExist(Guid id);

    public Task RegisterSupplier(Guid userId);

    public Task EditSupplierProfile(Guid supplierId, string? name = null, string? description = null,
        DateOnly? establishedAt = null, IFormFile? avatar = null, IFormFile? banner = null, string? address = null);

    public SupplierDto GetSupplier(Guid id);

    public SupplierDto GetSupplierOfProduct(Guid productId);

    public List<ProductShortDto> GetSupplierProducts(Guid id, int page);

    public Task<Guid> AddProduct(Guid supplierId, AddProductRequest request);

    public void EditProduct(Guid productId,
        string? name,
        string? specification,
        short? year,
        short? inStock,
        int? price,
        int? priceMax,
        int? priceMin,
        IEnumerable<string>? categories);

    public Task DeleteProduct(Guid supplierId, Guid productId);
}