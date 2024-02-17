using Firesafe.Domain.Core.Query;
using Firesafe.Domain.Entities;
using Firesafe.Domain.Queries;
using Firesafe.Domain.UnitOfWork;

namespace Firesafe.Domain.QueryHandlers;

public class ProductQueryHandler(IUnitOfWork uow) : QueryHandler(uow),
    IQueryHandler<GetProductsByPageQuery, IEnumerable<Product>>,
    IQueryHandler<GetProductQuery, Product?>,
    IQueryHandler<GetProductImagesQuery, IEnumerable<ProductImage>>
{
    public Task<IEnumerable<ProductImage>> Handle(GetProductImagesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Uow.ProductImageRepository.GetProductImages(request.ProductId));
    }

    public Task<Product?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Uow.ProductRepository.GetById(request.ProductId));
    }

    public Task<IEnumerable<Product>> Handle(GetProductsByPageQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Uow.ProductRepository.GetByPage(request.Page));
    }
}