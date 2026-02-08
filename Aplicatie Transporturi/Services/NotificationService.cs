using Aplicatie_Transporturi.Entities;

namespace Aplicatie_Transporturi.Services
{
    public interface INotificationService
    {
        Task SendDeliveryNotification(Delivery delivery, string messageType);
        Task SendUpcomingDeliveryReminders();
    }

    public class NotificationService : INotificationService
    {

        public async Task SendDeliveryNotification(Delivery delivery, string messageType)
        {
            Console.WriteLine($"[NOTIFICATION] {messageType} for Delivery #{delivery.Id}");
            
            switch (messageType)
            {
                case "CREATED":
                    await LogNotification($"New delivery created: {delivery.PickupLocation} ? {delivery.DropoffLocation}");
                    break;
                case "STARTED":
                    await LogNotification($"Delivery #{delivery.Id} has started");
                    break;
                case "COMPLETED":
                    await LogNotification($"Delivery #{delivery.Id} completed. Profit: {delivery.Profit:C}");
                    break;
                case "DELAYED":
                    await LogNotification($"?? Delivery #{delivery.Id} is delayed");
                    break;
            }
        }

        public async Task SendUpcomingDeliveryReminders()
        {
        
            await LogNotification("Checking for upcoming deliveries...");
        }

        private async Task LogNotification(string message)
        {
            await Task.Run(() => Console.WriteLine($"[{DateTime.UtcNow}] {message}"));
        }
    }
}
