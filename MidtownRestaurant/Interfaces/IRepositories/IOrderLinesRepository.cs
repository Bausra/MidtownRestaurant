using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem.Interfaces.Repositories
{
    public interface IOrderLinesRepository
    {
        public int AddOrderLine(OrderLine orderLine);
        public List<OrderLine> GetOrderLinesForOrder(int orderID);
        public int DeleteOrderLine(int ID);
    }
}
