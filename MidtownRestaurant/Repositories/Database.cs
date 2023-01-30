using MidtownRestaurantSystem.Interfaces.Repositories;
using System.Data.SQLite;

namespace MidtownRestaurantSystem.Repositories
{
    public class Database : IDatabase
    {
        private string _databaseName;
        private bool _newDbCreated = false;
        public bool NewDbCreated
        {
            get { return _newDbCreated; }
            set { _newDbCreated = value; }
        }

        public Database(string databaseName) 
        {
            _databaseName = databaseName;

            if (!File.Exists($"./{_databaseName}.sqlite")) 
            {
                SQLiteConnection.CreateFile($"{_databaseName}.sqlite");
                NewDbCreated = true;
                Console.WriteLine("Database created!");
            }
        }

        public SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection($"Data Source = {_databaseName}.sqlite");
        }
    }
}
