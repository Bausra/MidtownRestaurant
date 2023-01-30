using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem.Interfaces.IServices
{
    public interface IOrderLinesService
    {
        void AddOrderline(int orderID, MenuItem menuItem, int quantity);
        void DeleteOrderline(int orderLineID);
        void DeleteAllOrderLinesForOrderID(int orderID);
    }
}
