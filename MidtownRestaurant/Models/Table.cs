
namespace MidtownRestaurantSystem.Models
{
    public class Table
    {
        private int _id;
        public int ID 
        { 
            get { return _id; } 
            set { _id = value; }
        }

        private int _numberOfSeats;
        public int NumberOfSeats
        {
            get { return _numberOfSeats; }
            set { _numberOfSeats = value; }
        }

        private TableStatus _status;
        public TableStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public Table(int id, int numberOfSeats, TableStatus status)
        {
            ID = id;
            NumberOfSeats = numberOfSeats;
            Status = status;
        }
    }
}
