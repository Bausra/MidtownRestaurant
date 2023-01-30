using MidtownRestaurantSystem.Models;
using MidtownRestaurantSystem.Interfaces.IServices;
using MidtownRestaurantSystem.Interfaces.Repositories;

namespace MidtownRestaurantSystem.Services
{
    public class TablesService : ITablesService
    {
        private ITablesRepository _tablesRepository;

        public TablesService(ITablesRepository tablesRepository) 
        { 
            _tablesRepository = tablesRepository;
        }


        private bool ValidateEnoughSeatsAcrossTables(int numberOfpeople)
        {
            List<Table> availableTables = _tablesRepository.GetTablesByStatus(TableStatus.Available);
            int maxAvailableSeats = availableTables.Select(table => table.NumberOfSeats).ToList().Sum();

            return maxAvailableSeats >= numberOfpeople;
        }

        public List<int> ReserveTablesForCount(int numberOfPeople)
        {
            bool enoughSeats = ValidateEnoughSeatsAcrossTables(numberOfPeople);
            if (!enoughSeats)
            {
                throw new Exception("Not enough tables!");
            }

            int standingPeople = numberOfPeople;
            List<int> tableIDsReserved = new List<int>();

            while (standingPeople > 0)
            {
                Table table = _tablesRepository.GetTableWithExactNumberOfSeats(standingPeople);
                if (table != null)
                {
                    standingPeople -= table.NumberOfSeats;
                    tableIDsReserved.Add(table.ID);
                    ChangeTableStatus(table.ID, TableStatus.Reserved);
                    continue;
                }

                table = _tablesRepository.GetTableWithGreaterNumberOfSeats(standingPeople);
                if (table != null)
                {
                    standingPeople -= table.NumberOfSeats;
                    tableIDsReserved.Add(table.ID);
                    ChangeTableStatus(table.ID, TableStatus.Reserved);
                    continue;
                }

                table = _tablesRepository.GetTableWithLessNumberOfSeats(standingPeople);
                if (table != null)
                {
                    standingPeople -= table.NumberOfSeats;
                    tableIDsReserved.Add(table.ID);
                    ChangeTableStatus(table.ID, TableStatus.Reserved);
                    continue;
                }

                throw new Exception("Couldn't find a table!");
            }

            return tableIDsReserved;
        }

        public void ChangeTableStatus(int tableID, TableStatus changeTableStatusTo)
        {

            _tablesRepository.UpdateTableStatus(tableID, changeTableStatusTo);
        }
    }
}
