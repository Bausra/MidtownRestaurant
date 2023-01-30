
namespace MidtownRestaurantSystem.Models
{
    public class Order
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

        private int _completenessTimestamp;
        public int CompletenessTimestamp
        {
            get { return _completenessTimestamp; }
            set { _completenessTimestamp = value; }
        }

        private OrderStatus _status;
        public OrderStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public Order(int id, int tableId, int completenessTimestamp, OrderStatus status)
        {
            Id = id;
            TableID = tableId;
            CompletenessTimestamp = completenessTimestamp;
            Status = status;
        }
    }
}
