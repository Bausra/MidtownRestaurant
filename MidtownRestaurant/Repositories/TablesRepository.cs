using MidtownRestaurantSystem.Interfaces.Repositories;
using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem.Repositories
{
    public class TablesRepository : ITablesRepository
    {
        private IDatabase _database;

        public TablesRepository(IDatabase database)
        {
            _database = database;
        }

        public List<Table> GetTablesByStatus(TableStatus tableStatus)
        {
            List<Table> result = new List<Table>(); 

            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM tables WHERE status = '{tableStatus}'";
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(new Table(
                                Convert.ToInt32(reader["id"]),
                                Convert.ToInt32(reader["numberOfSeats"]),
                                (TableStatus)Enum.Parse(typeof(TableStatus), reader["status"].ToString())
                                ));
                        }
                    }
                    return result;
                }
            }
        }

        public int UpdateTableStatus(int tableID, TableStatus tableStatus)
        {
            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = @$"UPDATE tables 
                                        SET status = '{tableStatus}' 
                                        WHERE id = {tableID}";
                int result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("Database error occurred while updating table status!");
                }

                return result;
            }
        }

        public Table GetTableWithExactNumberOfSeats(int numberOfPeople)
        {
            Table result = null;

            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM tables WHERE status = 'Available' and numberOfSeats = {numberOfPeople} limit 1";
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        result = new Table(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["numberOfSeats"]),
                            (TableStatus)Enum.Parse(typeof(TableStatus), reader["status"].ToString())
                            );
                    }
                    
                }
            }

            return result;
        }

        public Table GetTableWithGreaterNumberOfSeats(int numberOfPeople)
        {
            Table result = null;

            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM tables WHERE status = 'Available' and numberOfSeats > {numberOfPeople} order by numberOfSeats asc limit 1";
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        result = new Table(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["numberOfSeats"]),
                            (TableStatus)Enum.Parse(typeof(TableStatus), reader["status"].ToString())
                            );
                    }

                }
            }

            return result;
        }

        public Table GetTableWithLessNumberOfSeats(int numberOfPeople)
        {
            Table result = null;

            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM tables WHERE status = 'Available' and numberOfSeats < {numberOfPeople} order by numberOfSeats desc limit 1";
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        result = new Table(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["numberOfSeats"]),
                            (TableStatus)Enum.Parse(typeof(TableStatus), reader["status"].ToString())
                            );
                    }

                }
            }

            return result;
        }

    }
}