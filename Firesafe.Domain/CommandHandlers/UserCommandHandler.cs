using Firesafe.Domain.Commands;
using Firesafe.Domain.Core.Command;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Domain.Events;
using Firesafe.Domain.UnitOfWork;

namespace Firesafe.Domain.CommandHandlers;

public class UserCommandHandler(IMediatorHandler mediatorHandler, IUnitOfWork uow)
    : CommandHandler(uow, mediatorHandler),
        ICommandHandler<AddNewUserCommand, bool>,
        ICommandHandler<SetUserRolesCommand, bool>,
        ICommandHandler<EditUserProfileCommand, bool>,
        ICommandHandler<RegisterFcmCommand, bool>,
        ICommandHandler<UnregisterFcmCommand, bool>
{
    public Task<bool> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
    {
        var user = Uow.UserRepository.AddNewUser(request.FirebaseId);
        Uow.Commit();
        MediatorHandler.PublishEvent(new UserCreatedEvent(user));
        return Task.FromResult(true);
    }

    public Task<bool> Handle(SetUserRolesCommand request, CancellationToken cancellationToken)
    {
        Uow.UserRepository.SetRoles(request.UserId, request.Roles);
        Uow.Commit();

        MediatorHandler.PublishEvent(new UserRolesSetEvent
        {
            UserId = request.UserId,
            Roles = request.Roles
        });
        return Task.FromResult(true);
    }

    public Task<bool> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = Uow.UserRepository.SetInfo(request.UserId, request.Name)!;
        Uow.Commit();
        if (true)
            MediatorHandler.PublishEvent(new UserProfileEditedEvent
            {
                User = user
            });
        return Task.FromResult(true);
    }

    public Task<bool> Handle(RegisterFcmCommand request, CancellationToken cancellationToken)
    {
        Uow.UserDeviceRepository.AddToken(request.UserId, request.Token);
        Uow.Commit();

        MediatorHandler.PublishEvent(new FcmTokenRegisteredEvent
        {
            UserId = request.UserId,
            Token = request.Token
        });

        return Task.FromResult(true);
    }

    public Task<bool> Handle(UnregisterFcmCommand request, CancellationToken cancellationToken)
    {
        Uow.UserDeviceRepository.RemoveToken(request.UserId, request.Token);
        Uow.Commit();
        MediatorHandler.PublishEvent(new FcmTokenUnregisteredEvent
        {
            UserId = request.UserId,
            Token = request.Token
        });

        return Task.FromResult(true);
    }
}