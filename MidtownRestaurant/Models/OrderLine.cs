
namespace MidtownRestaurantSystem.Models
{
    public class OrderLine
    {
        private int _id;
        public int Id 
        { 
            get { return _id; } 
            set { _id = value; }
        }

        private int _orderID;
        public int OrderID
        {
            get { return _orderID; }
            set { _orderID = value; }
        }

        private string _menuitem;
        public string MenuItem
        {
            get { return _menuitem; }
            set { _menuitem = value; }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        private double _unitPrice;
        public double UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        public OrderLine(int id, int orderID, string menuItem, int quantity, double unitPrice) 
        { 
            Id = id;
            OrderID = orderID;
            MenuItem = menuItem;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
