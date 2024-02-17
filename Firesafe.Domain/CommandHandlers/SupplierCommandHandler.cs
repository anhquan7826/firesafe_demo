using Firesafe.Domain.Commands;
using Firesafe.Domain.Core.Command;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Domain.Entities;
using Firesafe.Domain.Events;
using Firesafe.Domain.UnitOfWork;

namespace Firesafe.Domain.CommandHandlers;

public class SupplierCommandHandler(IMediatorHandler mediatorHandler, IUnitOfWork uow)
    : CommandHandler(uow, mediatorHandler),
        ICommandHandler<RegisterSupplierCommand, bool>,
        ICommandHandler<EditSupplierProfileCommand, bool>
{
    public Task<bool> Handle(EditSupplierProfileCommand request, CancellationToken cancellationToken)
    {
        var supplier = Uow.SupplierRepository.GetById(request.SupplierId)!;
        if (request.Name != null) supplier.Name = request.Name;

        if (request.Description != null) supplier.Description = request.Description;

        if (request.EstablishedAt != null) supplier.EstablishedAt = (DateOnly)request.EstablishedAt;

        if (request.Address != null) supplier.Address = request.Address;
        Uow.SupplierRepository.Update(supplier);
        Uow.Commit();

        MediatorHandler.PublishEvent(new SupplierProfileUpdatedEvent
        {
            Supplier = supplier
        });

        return Task.FromResult(true);
    }

    public Task<bool> Handle(RegisterSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = Uow.SupplierRepository.GetByUserId(request.UserId);
        if (supplier == null)
        {
            supplier = new Supplier
            {
                UserId = request.UserId
            };
            Uow.SupplierRepository.Add(supplier);
        }

        Uow.UserRoleRepository.AddUserRole(request.UserId, [Roles.Supplier]);
        Uow.Commit();

        MediatorHandler.PublishEvent(new SupplierCreatedEvent
        {
            Supplier = supplier
        });

        return Task.FromResult(true);
    }
}