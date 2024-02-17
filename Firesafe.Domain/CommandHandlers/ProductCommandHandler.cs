using AutoMapper;
using Firesafe.Domain.Commands;
using Firesafe.Domain.Core.Command;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Domain.Entities;
using Firesafe.Domain.Events;
using Firesafe.Domain.UnitOfWork;

namespace Firesafe.Domain.CommandHandlers;

public class ProductCommandHandler(
    IUnitOfWork uow,
    IMediatorHandler mediatorHandler,
    IMapper mapper) : CommandHandler(uow, mediatorHandler),
    ICommandHandler<AddNewProductCommand, bool>,
    ICommandHandler<DeleteProductCommand, bool>
{
    public Task<bool> Handle(AddNewProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request);
        product.ProductCertificates = request.Certificates.Select(record => new ProductCertificate
        {
            ProductCertificateId = record.Item1,
            Name = record.Item2
        }).ToList();
        product.ProductImages = request.ImageIds.Select((id, index) => new ProductImage
        {
            ProductImageId = id,
            Order = (short)index
        }).ToList();
        product.Categories = request.Categories.Select(c => Uow.ProductCategoryRepository.Get(c)!).ToList();

        Uow.ProductRepository.Add(product);

        Uow.Commit();
        MediatorHandler.PublishEvent(new ProductCreatedEvent(product));
        return Task.FromResult(true);
    }

    public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Uow.ProductRepository.Remove(request.ProductId);
        Uow.Commit();
        MediatorHandler.PublishEvent(new ProductDeletedEvent(request.ProductId));
        return Task.FromResult(true);
    }
}