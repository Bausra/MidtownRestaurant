using MidtownRestaurantSystem.Interfaces.Repositories;
using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem.Repositories
{

    public class OrdersRepository : IOrdersRepository
    {
        private IDatabase _database;

        public OrdersRepository(IDatabase database)
        {
            _database = database;
        }

        public int AddOrder(Order order)
        {
            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"INSERT INTO orders (tableID, completenessTimestamp, status) VALUES (@tableID, @completenessTimestamp, @status)";

                connection.Open();
                command.Parameters.AddWithValue("@tableID", order.TableID);
                command.Parameters.AddWithValue("@completenessTimestamp", order.CompletenessTimestamp);
                command.Parameters.AddWithValue("@status", order.Status.ToString());
                int result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("Database error occurred while adding order!");
                }

                return result;
            }
        }

        public List<Order> GetOrdersByStatus(OrderStatus orderStatus)
        {
            List<Order> result = new List<Order>();

            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM orders WHERE status = '{orderStatus}'";
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(new Order(
                                Convert.ToInt32(reader["id"]),
                                Convert.ToInt32(reader["tableID"]),
                                Convert.ToInt32(reader["completenessTimestamp"]),
                                (OrderStatus)Enum.Parse(typeof(OrderStatus), reader["status"].ToString())
                                ));
                        }
                    }
                    return result;
                }
            }
        }

        public int UpdateOrder(int orderID, int completenessTimestamp, OrderStatus status)
        {
            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @$"UPDATE orders
                                        SET completenessTimestamp = {completenessTimestamp}, 
	                                        status = '{status}'
                                        WHERE id = {orderID}";
                int result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("Database error occurred while updating order!");
                }
                return result;
            }
        }

        public int DeleteOpenOrder(int ID)
        {
            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                string queryDelete = $"DELETE FROM orders WHERE id = @id";
                command.CommandText = queryDelete;
                command.Parameters.AddWithValue("@id", ID);
                int result = command.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Database error occurred while deleting order!");
                }
                return result;
            }
        }
    }
}
