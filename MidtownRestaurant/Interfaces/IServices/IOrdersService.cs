using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem.Interfaces.IServices
{
    public interface IOrdersService
    {
        bool ValidateIfTableHasOrderWithStatus(int tableId, OrderStatus orderStatus);
        int CreateNewOrder(int tableId);
        int GetOrderID(int tableId);
        int FinishOrder(int tableId);
        void DeleteOpenOrder(int orderID);
    }
}
