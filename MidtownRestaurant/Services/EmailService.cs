using MidtownRestaurantSystem.Email;
using MidtownRestaurantSystem.Interfaces.IServices;
using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem
{
    public class EmailService : IEmailService
    {
        private IEmailClient _emailClient;

        public EmailService(IEmailClient emailClient)
        {
            _emailClient = emailClient;
        }

        public void CombineEmail(string email, int orderID, string attachmentLocation)
        {
            string emailSubject = EmailConstants.emailSubject;
            string purposeOfEmail = $"{EmailConstants.purposeOfEmail} {orderID}";
            string emailBodyText = String.Join(
                                                Environment.NewLine,
                                                EmailConstants.greeting,
                                                Environment.NewLine,
                                                purposeOfEmail,
                                                Environment.NewLine,
                                                EmailConstants.thankYou,
                                                Environment.NewLine,
                                                EmailConstants.regards);
            _emailClient.SendMail(email, emailSubject, emailBodyText, attachmentLocation);
        }
    }
}
