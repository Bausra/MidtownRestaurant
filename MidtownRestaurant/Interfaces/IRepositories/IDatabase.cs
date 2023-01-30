using System.Data.SQLite;

namespace MidtownRestaurantSystem.Interfaces.Repositories
{
    public interface IDatabase
    {
        bool NewDbCreated { get; set; }
        SQLiteConnection CreateConnection();
    }
}
