using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem.Interfaces.IServices
{
    public interface ITablesService
    {
        List<int> ReserveTablesForCount(int numberOfPeople);
        void ChangeTableStatus(int tableID, TableStatus changeTableStatusTo);
    }
}
