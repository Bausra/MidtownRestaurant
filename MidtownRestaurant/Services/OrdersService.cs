using MidtownRestaurantSystem.Interfaces.IServices;
using MidtownRestaurantSystem.Interfaces.Repositories;
using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem.Services
{
    public class OrdersService : IOrdersService
    {
        private IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        private void AddNewOrder(int tableId)
        {
            int ID = -1;                    //In DB ID will be assigned automatically.
            int completenessTimestamp = 0;
            OrderStatus orderStatusProcessing = OrderStatus.Processing;

            Order newOrder = new Order(
                ID,
                tableId,
                completenessTimestamp,
                orderStatusProcessing
                );

            _ordersRepository.AddOrder(newOrder);
        }

        public bool ValidateIfTableHasOrderWithStatus(int tableId, OrderStatus orderStatus)
        {
            List<int> ordersIDs = _ordersRepository.GetOrdersByStatus(orderStatus).Select(order => order.TableID).ToList();

            return ordersIDs.Contains(tableId) ? true : false;
        }

        public int CreateNewOrder(int tableId)
        {
            AddNewOrder(tableId);
            int orderID = GetOrderID(tableId);

            return orderID;
        }

        public int GetOrderID(int tableId)
        {
            Order selectedOrder = _ordersRepository.GetOrdersByStatus(OrderStatus.Processing)
                .Where(order => order.TableID == tableId)
                .First();
            return selectedOrder.Id;
        }

        public int FinishOrder(int tableId)
        {
            int orderID = GetOrderID(tableId);

            DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
            int completenessTimestamp = Convert.ToInt32(now.ToUnixTimeSeconds());

            OrderStatus status = OrderStatus.Finished;

            _ordersRepository.UpdateOrder(orderID, completenessTimestamp, status);
            return orderID;
        }

        public void DeleteOpenOrder(int orderID)
        {
            _ordersRepository.DeleteOpenOrder(orderID);  
        }
    }
}
