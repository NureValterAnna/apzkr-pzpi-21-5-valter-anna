using Microsoft.AspNetCore.SignalR;
using Application.Interfaces;

namespace Infrastructure.Hubs;

public class NotificationHub : Hub<INotificationHub>
{
    public async Task SendNotification(string message)
    {
        await Clients.Others.SendNotification(message);
    }
}
