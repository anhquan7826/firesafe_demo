using Firesafe.Domain.Commands;
using Firesafe.Domain.Core.Command;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Domain.Entities;
using Firesafe.Domain.Events;
using Firesafe.Domain.UnitOfWork;

namespace Firesafe.Domain.CommandHandlers;

public class NewspaperCommandHandler(IUnitOfWork uow, IMediatorHandler mediatorHandler)
    : CommandHandler(uow, mediatorHandler), ICommandHandler<AddNewNewspaperCommand, bool>,
        ICommandHandler<DeleteNewspaperCommand, bool>
{
    public Task<bool> Handle(AddNewNewspaperCommand request, CancellationToken cancellationToken)
    {
        var categories = Uow.NewspaperCategoryRepository.GetAll()
            .Where(x => request.NewspaperCategories.Contains(x.NewspaperCategoryId)).ToList();
        var newspaper = new Newspaper
        {
            NewspaperId = request.NewspaperId,
            Title = request.Title,
            NewspaperCategories = categories
        };
        Uow.NewspaperRepository.Add(newspaper);
        Uow.Commit();
        MediatorHandler.PublishEvent(new NewNewspaperAddedEvent
        {
            Newspaper = newspaper
        });
        return Task.FromResult(true);
    }

    public Task<bool> Handle(DeleteNewspaperCommand request, CancellationToken cancellationToken)
    {
        Uow.NewspaperRepository.Remove(request.NewspaperId);
        Uow.Commit();
        MediatorHandler.PublishEvent(new NewspaperDeletedEvent
        {
            NewspaperId = request.NewspaperId
        });
        return Task.FromResult(true);
    }
}