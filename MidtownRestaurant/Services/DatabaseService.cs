using MidtownRestaurantSystem.Interfaces.IServices;
using MidtownRestaurantSystem.Interfaces.Repositories;

namespace MidtownRestaurantSystem.Services
{
    public class DatabaseService : IDatabaseService
    {
        private IDatabase _database;

        public DatabaseService(IDatabase database) 
        { 
            _database = database;
        }


        //Create tables
        private void CreateTable(string tableName, string tableColumns)
        {
            using (var connection = _database.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName} ({tableColumns})";
                connection.Open();
                int result = command.ExecuteNonQuery();
            }
        }

        public void CreateDBtables()
        {
            CreateTable("tables", "id INTEGER PRIMARY KEY AUTOINCREMENT, numberOfSeats INTEGER, status TEXT");
            CreateTable("orders", "id INTEGER PRIMARY KEY AUTOINCREMENT, tableID INTEGER, completenessTimestamp INTEGER, status TEXT");
            CreateTable("orderLines", "id INTEGER PRIMARY KEY AUTOINCREMENT, orderID INTEGER, menuItem TEXT, quantity INTEGER, unitPrice REAL");  
        }

        //Create initial data
        private List<string> InitTablesSQLs()
        {
            List<string> tablesSQLs = new List<string>();
            tablesSQLs.Add("INSERT INTO tables (numberOfSeats, status) VALUES (2, 'Available')");
            tablesSQLs.Add("INSERT INTO tables (numberOfSeats, status) VALUES (2, 'Available')");
            tablesSQLs.Add("INSERT INTO tables (numberOfSeats, status) VALUES (4, 'Available')");
            tablesSQLs.Add("INSERT INTO tables (numberOfSeats, status) VALUES (4, 'Available')");
            tablesSQLs.Add("INSERT INTO tables (numberOfSeats, status) VALUES (6, 'Available')");
            return tablesSQLs;
        }

        private List<string> InitOrdersSQLs()
        {
            List<string> ordersSQLs = new List<string>();
            ordersSQLs.Add("INSERT INTO orders (tableID, completenessTimestamp, status) VALUES (1, 1673969177, 'finished')");
            return ordersSQLs;
        }

        private List<string> InitOrderLinesSQLs()
        {
            List<string> orderLinesSQLs = new List<string>();
            orderLinesSQLs.Add("INSERT INTO orderLines (orderID, menuItem, quantity, unitPrice) VALUES (1, 'Nachos', 2, 5.99)");
            orderLinesSQLs.Add("INSERT INTO orderLines (orderID, menuItem, quantity, unitPrice) VALUES (1, 'Red wine', 1, 8.99)");
            orderLinesSQLs.Add("INSERT INTO orderLines (orderID, menuItem, quantity, unitPrice) VALUES (1, 'White wine', 1, 7.99)");
            return orderLinesSQLs;
        }

        public void ExecuteNonQuery(List<string> sql)
        {
            using (var connection = _database.CreateConnection())
            {
                connection.Open();
                foreach (string sqlItem in sql)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sqlItem;
                        int result = command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void CreateInitialData()
        {
            ExecuteNonQuery(InitTablesSQLs());
            ExecuteNonQuery(InitOrdersSQLs());
            ExecuteNonQuery(InitOrderLinesSQLs());
        }
    }

    
}
