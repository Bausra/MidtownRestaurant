
using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem.Interfaces.Repositories
{
    public interface ITablesRepository
    {
        List<Table> GetTablesByStatus(TableStatus tableStatus);
        int UpdateTableStatus(int tableID, TableStatus tableStatus);
        Table GetTableWithExactNumberOfSeats(int numberOfPeople);
        Table GetTableWithGreaterNumberOfSeats(int numberOfPeople);
        Table GetTableWithLessNumberOfSeats(int numberOfPeople);
    }
}
