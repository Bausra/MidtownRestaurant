namespace MidtownRestaurantSystem.Interfaces.IServices
{
    public interface IEmailService
    {
        void CombineEmail(string email, int orderID, string attachmentLocation);
    }
}
