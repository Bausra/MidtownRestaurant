
namespace MidtownRestaurantSystem.Models
{
    public class OrderHistoryForReporting
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _tableID;
        public int TableID
        {
            get { return _tableID; }
            set { _tableID = value; }
        }

        private int _numberOfSeats;
        public int NumberOfSeats
        {
            get { return _numberOfSeats; }
            set { _numberOfSeats = value; }
        }

        private int _completenessTimestamp;
        public int CompletenessTimestamp
        {
            get { return _completenessTimestamp; }
            set { _completenessTimestamp = value; }
        }

        public OrderHistoryForReporting(int id, int tableId, int numberOfSeats, int completenessTimestamp)
        {
            Id = id;
            TableID = tableId;
            NumberOfSeats = numberOfSeats;
            CompletenessTimestamp = completenessTimestamp;
        }
    }
}
