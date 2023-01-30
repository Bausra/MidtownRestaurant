using MidtownRestaurantSystem.Models;

namespace MidtownRestaurantSystem.Repositories
{
    public static class MenuItemRepository
    {

        public static List<MenuItem> ReadDrinksRowListFromResource()
        {
            return ReadMenuItems(MidtownRestaurant.Properties.Resource.drinks);
        }

        public static List<MenuItem> ReadFoodRowListFromResource()
        {
            return ReadMenuItems(MidtownRestaurant.Properties.Resource.food);
        }

        private static List<MenuItem> ReadMenuItems(string dataString)
        {
            string[] lines = dataString.Split(GetLineSeparator(dataString));

            List<MenuItem> result = new List<MenuItem>();

            foreach (string line in lines)
            {
                string[] data = line.Split(",");
                MenuItem row = new MenuItem(
                        int.Parse(data[0]),
                        data[1],
                        double.Parse(data[2])
                        );

                result.Add(row);
            }

            return result;
        }

        private static string GetLineSeparator(string text)
        {
            if (text.Contains("\r\n"))
            {
                return "\r\n";
            }

            return "\n";
        }
    }
}
