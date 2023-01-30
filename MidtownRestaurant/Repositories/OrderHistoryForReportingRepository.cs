using MidtownRestaurantSystem.Interfaces.Repositories;
using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem.Repositories
{
    public class OrderHistoryForReportingRepository : IOrderHistoryForReportingRepository
    {
        private IDatabase _database;

        public OrderHistoryForReportingRepository(IDatabase database)
        {
            _database = database;
        }

        public OrderHistoryForReporting GetOrderHistory(int orderID)
        {
            OrderHistoryForReporting result = null;

            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @$"SELECT 
                                         ord.id,
                                         ord.tableID,
                                         t.numberOfSeats AS tableSeatNumber,
                                         ord.completenessTimestamp
                                        FROM orders ord
                                        INNER JOIN tables t ON ord.tableID = t.id
                                        WHERE ord.id = {orderID}";
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = new OrderHistoryForReporting(
                                Convert.ToInt32(reader["id"]),
                                Convert.ToInt32(reader["tableID"]),
                                Convert.ToInt32(reader["tableSeatNumber"]),
                                Convert.ToInt32(reader["completenessTimestamp"])
                                );
                        }
                    }
                    return result;
                }
            }
        }
    }
}
