
namespace MidtownRestaurantSystem.Models
{
    public class MenuItem
    {
        private int _id;
        public int Id 
        { 
            get { return _id; } 
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private double _unitPrice;
        public double UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        public MenuItem(int id, string name, double unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }
    }
}
