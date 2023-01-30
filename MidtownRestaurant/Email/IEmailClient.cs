
namespace MidtownRestaurantSystem.Email
{
    public interface IEmailClient
    {
        public void SendMail(string email, string subject, string bodyText, string attachmentLocation);
    }
}
