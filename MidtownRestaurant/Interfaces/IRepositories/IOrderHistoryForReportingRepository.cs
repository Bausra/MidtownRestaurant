using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem.Interfaces.Repositories
{
    public interface IOrderHistoryForReportingRepository
    {
        public OrderHistoryForReporting GetOrderHistory(int orderID);
    }
}
