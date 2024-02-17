using FirebaseAdmin.Messaging;
using Firesafe.Domain.Core.Event;
using Firesafe.Domain.Events;
using Firesafe.Domain.UnitOfWork;
using EventHandler = Firesafe.Domain.Core.Event.EventHandler;

namespace Firesafe.Domain.EventHandlers;

public class FiresafeEventHandler(IUnitOfWork uow) : EventHandler(uow), IEventHandler<NewNewspaperAddedEvent>
{
    public async Task Handle(NewNewspaperAddedEvent notification, CancellationToken cancellationToken)
    {
        var page = 0;
        var tokens = Uow.UserDeviceRepository.GetAll().OrderByDescending(x => x.CreatedAt).Take(500).Select(ud => ud.FcmToken);
        do
        {
            var message = new MulticastMessage
            {
                Tokens = tokens.ToList(),
                Notification = new Notification
                {
                    Title = notification.Newspaper.Title,
                    ImageUrl = "https://storage.googleapis.com/firesafe-bucket/newspapers/211785c5-8e13-4297-bed8-68260b007c30/thumbnail.jpg"
                },
                Data = new Dictionary<string, string>
                {
                    { "newspaper", notification.Newspaper.NewspaperId.ToString() }
                }
            };

            await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(message, cancellationToken);
            page++;
            tokens = Uow.UserDeviceRepository.GetAll().Skip(500 * page).Take(500).Select(ud => ud.FcmToken);
        } while (tokens.Count() < 500);
    }
}