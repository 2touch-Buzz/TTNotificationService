namespace Xenios.TTNotificationService.Models
{
    public class SendTextMessageRequest
    {
        public string? PhoneNumber { get; set; }
        public string? Message { get; set; }
    }
}