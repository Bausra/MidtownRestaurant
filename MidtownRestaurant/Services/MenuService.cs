using MidtownRestaurantSystem.Interfaces.Repositories;
using MidtownRestaurantSystem.Models;
using MidtownRestaurantSystem.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MidtownRestaurantSystem.Services
{
    public class MenuService
    {
        private ITablesRepository _tablesRepository;
        private IOrderLinesRepository _orderLinesRepository;
        
        public MenuService(ITablesRepository tablesRepository, IOrderLinesRepository orderLinesRepository)
        {
            _tablesRepository = tablesRepository;
            _orderLinesRepository = orderLinesRepository;
        }

        public int GetMenuChoice()
        {
            int choice = 0;
            while (choice <= 0 || choice > 8)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("[1] Make table reserved");
                Console.WriteLine("[2] Make table available");
                Console.WriteLine("[3] Start an order");
                Console.WriteLine("[4] Supplement order");
                Console.WriteLine("[5] Delete order item");
                Console.WriteLine("[6] Delete open order");
                Console.WriteLine("[7] Finish order");
                Console.WriteLine("[8] Exit\n");

                choice = GetPositiveInteger("Enter number from brackets:", false);

                if (choice > 8)
                {
                    Console.WriteLine("\nIncorrect input!");
                } 
            }
            return choice;
        }

        public int GetPositiveInteger(string requestName, bool allowZero)
        {
            int number = -1;
            while (number == -1)
            {
                Console.WriteLine($"\n{requestName}");
                string entry = Console.ReadLine();
                bool entryIsConvertableToNumber = Int32.TryParse(entry, out _);

                if (String.IsNullOrEmpty(entry))
                {
                    Console.WriteLine("\nEntry cannot be empty!");
                }
                else if (entryIsConvertableToNumber == false)
                {
                    Console.WriteLine("\nAmount must be numeric!");
                }
                else if (entry.Contains(",") || entry.Contains("."))
                {
                    Console.WriteLine("\nNo punctuation marks are allowed, enter whole number!");
                }
                else if (Int32.Parse(entry) < 0)
                {
                    Console.WriteLine("\nNegative numbers are not allowed!");
                }
                else if (Int32.Parse(entry) == 0 && !allowZero)
                {
                    Console.WriteLine("\nZero is not allowed!");
                }
                else
                {
                    number = Int32.Parse(entry);
                }
            }
            return number;
        }

        public int SelectTableIdByStatus(TableStatus tableStatus)
        {
            List<Table> filteredTables = _tablesRepository.GetTablesByStatus(tableStatus).OrderBy(x => x.ID).ToList();
            List<int> filteredTablesIDs = filteredTables.Select(table => table.ID).ToList();

            int tableID = 0;

            if (filteredTablesIDs.Count == 0)
            {
                throw new Exception("No tables to choose from!");
            }

            while (tableID == 0)
            {
                Console.WriteLine($"\n{tableStatus} tables:");
                foreach (Table table in filteredTables)
                {
                    Console.WriteLine($"[{table.ID}] table {table.NumberOfSeats} seats");
                }

                int tableIDChoice = GetPositiveInteger("\nEnter number in the brackets:", false);

                if (filteredTablesIDs.Contains(tableIDChoice))
                {
                    tableID = tableIDChoice;
                }
                else
                {
                    Console.WriteLine("\nIncorrect value!");
                    continue;
                }
            }
            return tableID;
        }

        private MenuItemTypes GetMenuItemType()
        {
            MenuItemTypes menuType = 0;
            while (menuType == 0)
            {
                Console.WriteLine("\nWhat menu would you like to choose from?");
                Console.WriteLine("[1] Drinks");
                Console.WriteLine("[2] Food\n");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        menuType = MenuItemTypes.Drinks;
                        break;
                    case "2":
                        menuType = MenuItemTypes.Food;
                        break;
                    default:
                        Console.WriteLine("\nIncorrect value!");
                        break;
                }
            }
            return menuType;
        }

        private MenuItem GetMenuItem(MenuItemTypes menuItemType)
        {
            MenuItem menuItem = null;
            while (menuItem == null)
            {
                List<MenuItem> items = GetMenuItemsTypeList(menuItemType);
                List<int> itemsIDs = items.Select(item => item.Id).ToList();

                Console.WriteLine("\nChoose from:");
                foreach (MenuItem item in items)
                {
                    Console.WriteLine($"[{item.Id}] {item.Name}");
                }
                Console.WriteLine();

                int choice = int.Parse(Console.ReadLine());

                if (itemsIDs.Contains(choice))
                {
                    menuItem =  items.Where(item => item.Id == choice).First();
                }
                else
                {
                    Console.WriteLine("\nIncorrect value!");
                }
            }
            return menuItem;
        }

        private List<MenuItem> GetMenuItemsTypeList(MenuItemTypes menuItemType)
        {
            List<MenuItem> items;
            if (menuItemType == MenuItemTypes.Drinks)
            {
                items = MenuItemRepository.ReadDrinksRowListFromResource();
            }
            else
            {
                items = MenuItemRepository.ReadFoodRowListFromResource();
            }
            return items;
        }

        public MenuItem GetMenuItem()
        {
            MenuItemTypes menuItemType = GetMenuItemType();

            MenuItem menuItem = null;
            if (menuItemType == MenuItemTypes.Drinks)
            {
                menuItem = GetMenuItem(MenuItemTypes.Drinks);
            }
            else
            {
                menuItem = GetMenuItem(MenuItemTypes.Food);
            }

            return menuItem;
        }

        public ContinueTypes GetProceedChoice(string requestName)
        {
            ContinueTypes proceedChoice = 0;
            while (proceedChoice == 0)
            {
                Console.WriteLine($"\n{requestName}");
                Console.WriteLine("[1] Yes");
                Console.WriteLine("[2] No\n");

                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        proceedChoice = ContinueTypes.Yes;
                        break;
                    case "2":
                        proceedChoice =  ContinueTypes.No;
                        break;
                    default:
                        Console.WriteLine("\nIncorrect value!");
                        break;
                }
            }
            return proceedChoice;
        }

        public int GetOrderLineToDelete(int orderID)
        {
            List<OrderLine> orderLines = _orderLinesRepository.GetOrderLinesForOrder(orderID);
            List<int> orderLinesIDs = orderLines.Select(orderLine=> orderLine.Id).ToList();

            int orderLineID = 0;
            while (orderLineID == 0)
            {
                Console.WriteLine("\nWhich order line would you like to delete?\n");
                foreach (OrderLine orderLine in orderLines)
                {
                    Console.WriteLine(String.Format($"[{orderLine.Id, -3}] {orderLine.MenuItem, -18} {orderLine.Quantity, 5} {orderLine.UnitPrice, 6}"));
                }

                int choice = GetPositiveInteger("Choose from brackets", false);

                if (orderLinesIDs.Contains(choice))
                {
                    orderLineID = choice;
                }
                else
                {
                    Console.WriteLine("Incorrect value!");
                }
            }
            return orderLineID;
        }

        public void PrintReservedTables(List<int> reservedTables)
        {
            string reservationNotification = "\nTable/s No. ";
            foreach (int reservedTable in reservedTables)
            {
                reservationNotification += $"{reservedTable}, ";
            }
            reservationNotification = reservationNotification.Remove(reservationNotification.LastIndexOf(","));
            reservationNotification += " - reserved!";

            Console.WriteLine(reservationNotification);
        }

        public string GetEmail()
        {
            string emailEntry = null;
            while (emailEntry == null)
            {
                Console.WriteLine("\nEnter email for report to be sent to:");
                string entry = Console.ReadLine();
                var email = new EmailAddressAttribute();
                if (email.IsValid(entry))
                {
                    emailEntry = entry;
                }
                else
                {
                    Console.WriteLine("\nIncorrect email!");
                }
            }
            return emailEntry;
        }
    }
}
