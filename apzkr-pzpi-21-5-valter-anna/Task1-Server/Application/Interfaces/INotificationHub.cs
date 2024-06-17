namespace Application.Interfaces;

public interface INotificationHub
{
    Task SendNotification(string message);
}
