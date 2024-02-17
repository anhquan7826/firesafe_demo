using Application.DTOs;

namespace Application.Services.Interface;

public interface IProductService
{
    public bool IsExist(Guid productId);

    public bool IsOwnedBy(Guid productId, Guid supplierId);
    
    public IEnumerable<ProductShortDto> GetByCategoryPaged(string category, int page, DateTime timestamp);

    public IEnumerable<ProductShortDto> SearchProduct(
        DateTime timestamp,
        string query,
        int page = 0,
        List<string>? categories = null,
        int? year = null,
        short? rating = null,
        List<int>? priceRange = null);

    public ProductDto GetProduct(Guid productId);
}