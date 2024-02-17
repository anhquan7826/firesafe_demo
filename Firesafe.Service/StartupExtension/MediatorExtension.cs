using Firesafe.Domain.CommandHandlers;
using Firesafe.Domain.Commands;
using Firesafe.Domain.Core.Mediator;
using Firesafe.Domain.Entities;
using Firesafe.Domain.EventHandlers;
using Firesafe.Domain.EventHandlers.EventStoreHandlers;
using Firesafe.Domain.Events;
using Firesafe.Domain.Events.EventStore;
using Infrastructure.Mediator;
using MediatR;
// using Firesafe.Domain.Queries;
// using Firesafe.Domain.QueryHandlers;

namespace Firesafe.Service.StartupExtension;

internal static class MediatorExtension
{
    internal static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        // COMMANDS
        services.AddScoped<IRequestHandler<AddNewUserCommand, bool>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<SetUserRolesCommand, bool>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<EditSupplierProfileCommand, bool>, SupplierCommandHandler>();
        services.AddScoped<IRequestHandler<AddNewProductCommand, bool>, ProductCommandHandler>();
        services.AddScoped<IRequestHandler<EditUserProfileCommand, bool>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteProductCommand, bool>, ProductCommandHandler>();
        services.AddScoped<IRequestHandler<RegisterSupplierCommand, bool>, SupplierCommandHandler>();
        services.AddScoped<IRequestHandler<AddNewNewspaperCommand, bool>, NewspaperCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteNewspaperCommand, bool>, NewspaperCommandHandler>();
        services.AddScoped<IRequestHandler<RegisterFcmCommand, bool>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<UnregisterFcmCommand, bool>, UserCommandHandler>();
        
        // EVENTS
        services.AddScoped<INotificationHandler<NewNewspaperAddedEvent>, FiresafeEventHandler>();
        
        // EVENT STORE
        services.AddScoped<INotificationHandler<ExceptionOccuredEvent>, ExceptionEventHandler>();
    }
}