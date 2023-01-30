using MidtownRestaurantSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtownRestaurantSystem.Interfaces.Repositories
{
    public interface IOrdersRepository
    {

        public int AddOrder(Order order);

        public List<Order> GetOrdersByStatus(OrderStatus orderStatus);

        public int UpdateOrder(int orderID, int completenessTimestamp, OrderStatus status);

        public int DeleteOpenOrder(int ID);

    }
}
