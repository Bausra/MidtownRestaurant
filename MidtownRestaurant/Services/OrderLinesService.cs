using MidtownRestaurantSystem.Models;
using MidtownRestaurantSystem.Interfaces.IServices;
using MidtownRestaurantSystem.Interfaces.Repositories;

namespace MidtownRestaurantSystem.Services
{
    public class OrderLinesService : IOrderLinesService
    {
        private IOrderLinesRepository _orderLinesRepository;

        public OrderLinesService(IOrderLinesRepository orderLinesRepository)
        {
            _orderLinesRepository = orderLinesRepository;
        }

        public void AddOrderline(int orderID, MenuItem menuItem, int quantity)
        {
            int ID = -1;                    //Order line ID in DB will be added automatically
            OrderLine orderLine = new OrderLine
                (
                    ID,
                    orderID,
                    menuItem.Name,
                    quantity,
                    menuItem.UnitPrice
                );
            _orderLinesRepository.AddOrderLine(orderLine);
        }

        public void DeleteOrderline(int orderLineID)
        {
            _orderLinesRepository.DeleteOrderLine(orderLineID);
        }

        public void DeleteAllOrderLinesForOrderID(int orderID)
        {
            List<OrderLine> orderLines = _orderLinesRepository.GetOrderLinesForOrder(orderID);
            List<int> orderLinesIDs = orderLines.Select(orderLine => orderLine.Id).ToList();

            foreach(int orderLineID in orderLinesIDs)
            {
                _orderLinesRepository.DeleteOrderLine(orderLineID);
            }
        }
    }
}
