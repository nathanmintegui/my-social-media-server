namespace Application.Implementations.Validations;

public class Notifiable
{
    private List<Notification> _notifications = new();

    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public void AddNotifications(IEnumerable<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public bool IsValid()
    {
        return !_notifications.Any();
    }
}