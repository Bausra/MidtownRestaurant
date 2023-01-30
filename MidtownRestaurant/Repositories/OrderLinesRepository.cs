using MidtownRestaurantSystem.Interfaces.Repositories;
using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem.Repositories
{
    public class OrderLinesRepository : IOrderLinesRepository
    {
        private IDatabase _database;

        public OrderLinesRepository(IDatabase database)
        {
            _database = database;
        }

        public int AddOrderLine(OrderLine orderLine)
        {
            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"INSERT INTO orderLines (orderID, menuItem, quantity, unitPrice) VALUES (@orderID, @menuItem, @quantity, @unitPrice)";

                connection.Open();
                command.Parameters.AddWithValue("@orderID", orderLine.OrderID);
                command.Parameters.AddWithValue("@menuItem", orderLine.MenuItem);
                command.Parameters.AddWithValue("@quantity", orderLine.Quantity);
                command.Parameters.AddWithValue("@unitPrice", orderLine.UnitPrice);
                int result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("Database error occurred while adding order line!");
                }

                return result;
            }
        }

        public List<OrderLine> GetOrderLinesForOrder(int orderID)
        {
            List<OrderLine> result = new List<OrderLine>();

            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM orderLines WHERE orderID = '{orderID}'";
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(new OrderLine(
                                Convert.ToInt32(reader["id"]),
                                Convert.ToInt32(reader["orderID"]),
                                reader["menuItem"].ToString(),
                                Convert.ToInt32(reader["quantity"]),
                                Convert.ToDouble(reader["unitPrice"])
                                ));
                        }
                    }
                    return result;
                }
            }
        }

        public int DeleteOrderLine(int ID)
        {
            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                string queryDelete = $"DELETE FROM orderLines WHERE id = @id";
                command.CommandText = queryDelete;
                command.Parameters.AddWithValue("@id", ID);
                int result = command.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("Database error occurred while deleting order line!");
                }
                return result;
            }
        }
    }
}
