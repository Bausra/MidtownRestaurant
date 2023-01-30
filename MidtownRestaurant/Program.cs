using MidtownRestaurantSystem;
using MidtownRestaurantSystem.Repositories;
using MidtownRestaurantSystem.Services;
using MidtownRestaurantSystem.Models;
using MidtownRestaurantSystem.Email;
using MidtownRestaurantSystem.Interfaces.IServices;
using MidtownRestaurantSystem.Interfaces.Repositories;

//Database should be saved to path ...\MidtownRestaurantSystem\bin\Debug\net6.0
//Cheques will be saved in location "C:\\cheques"

//Database
IDatabase database = new Database("midtownDB");
IDatabaseService databaseService = new DatabaseService(database);
//Tables
ITablesRepository tablesRepository = new TablesRepository(database);
ITablesService tablesService = new TablesService(tablesRepository);
//Orders
IOrdersRepository ordersRepository = new OrdersRepository(database);
IOrdersService ordersService = new OrdersService(ordersRepository);
//Order Lines
IOrderLinesRepository orderLinesRepository = new OrderLinesRepository(database);
IOrderLinesService orderLineService = new OrderLinesService(orderLinesRepository);
//Order History For Reporting
IOrderHistoryForReportingRepository orderHistoryForReportingRepository = new OrderHistoryForReportingRepository(database);
//Cheque
IChequeService chequeService = new ChequeService(orderLinesRepository, orderHistoryForReportingRepository);
//Email
IEmailClient client = new SmtpEmailClient();
IEmailService sendEmailWithAttachment = new EmailService(client);
//Menu
MenuService menu = new MenuService(tablesRepository, orderLinesRepository);


//If there is no database create tables and initial data
if (database.NewDbCreated)
{
    //Create tables
    databaseService.CreateDBtables();

    //Add initial data to DB
    databaseService.CreateInitialData();
}



while (true)
{
    int choice = menu.GetMenuChoice();
    bool areOpenOrdersForTable;
    int orderID;
    int tableID;
    ContinueTypes continueType = ContinueTypes.Yes;

    try
    {
        switch (choice)
        {
            case 1:
                int numberOfPeople = menu.GetPositiveInteger("Enter number of people:", false);
                List<int> reservedTableIDs = tablesService.ReserveTablesForCount(numberOfPeople);
                menu.PrintReservedTables(reservedTableIDs);
                break;

            case 2:
                int tableIdReserved = menu.SelectTableIdByStatus(TableStatus.Reserved);
                if (tableIdReserved > 0)
                {
                    tablesService.ChangeTableStatus(tableIdReserved, TableStatus.Available);
                }
                break;

            case 3:
                tableID = menu.SelectTableIdByStatus(TableStatus.Reserved);

                areOpenOrdersForTable = ordersService.ValidateIfTableHasOrderWithStatus(tableID, OrderStatus.Processing);
                if (areOpenOrdersForTable)
                {
                    throw new Exception("\nOrder for specified table is already in process! Go to Supplement order.");
                }

                orderID = ordersService.CreateNewOrder(tableID);

                while (continueType != ContinueTypes.No)
                {
                    MenuItem menuItem = menu.GetMenuItem();
                    int quantity = menu.GetPositiveInteger("Enter quantity:", false);
                    orderLineService.AddOrderline(orderID, menuItem, quantity);
                    continueType = menu.GetProceedChoice("Would you like to add more orders?");
                }
                Console.WriteLine("\nOrder added sucessfully!");

                break;

            case 4:
                tableID = menu.SelectTableIdByStatus(TableStatus.Reserved);

                areOpenOrdersForTable = ordersService.ValidateIfTableHasOrderWithStatus(tableID, OrderStatus.Processing);
                if (!areOpenOrdersForTable)
                {
                    throw new Exception("\nThere are no open orders!");
                }

                orderID = ordersService.GetOrderID(tableID);

                while (continueType != ContinueTypes.No)
                {
                    MenuItem menuItem = menu.GetMenuItem();
                    int quantity = menu.GetPositiveInteger("Enter quantity:", false);
                    orderLineService.AddOrderline(orderID, menuItem, quantity);
                    continueType = menu.GetProceedChoice("Would you like to add more orders?");
                }
                Console.WriteLine("\nOrder line added sucessfully!");

                break;

            case 5:
                tableID = menu.SelectTableIdByStatus(TableStatus.Reserved);

                areOpenOrdersForTable = ordersService.ValidateIfTableHasOrderWithStatus(tableID, OrderStatus.Processing);
                if (!areOpenOrdersForTable)
                {
                    throw new Exception("\nThere are no open orders!");
                }

                orderID = ordersService.GetOrderID(tableID);
                int orderLineID = menu.GetOrderLineToDelete(orderID);
                orderLineService.DeleteOrderline(orderLineID);

                break;

            case 6:

                tableID = menu.SelectTableIdByStatus(TableStatus.Reserved);

                areOpenOrdersForTable = ordersService.ValidateIfTableHasOrderWithStatus(tableID, OrderStatus.Processing);
                if (!areOpenOrdersForTable)
                {
                    throw new Exception("\nThere are no open orders!");
                }

                orderID = ordersService.GetOrderID(tableID);
                orderLineService.DeleteAllOrderLinesForOrderID(orderID);
                ordersService.DeleteOpenOrder(orderID);
                tablesService.ChangeTableStatus(tableID, TableStatus.Available);

                break;

            case 7:

                tableID = menu.SelectTableIdByStatus(TableStatus.Reserved);

                areOpenOrdersForTable = ordersService.ValidateIfTableHasOrderWithStatus(tableID, OrderStatus.Processing);
                if (!areOpenOrdersForTable)
                {
                    throw new Exception("\nThere is no order in status 'process' for specified table!");
                }

                orderID = ordersService.FinishOrder(tableID);

                string fullChequeLocationPath = chequeService.SaveStringToPdfFile(orderID);

                continueType = menu.GetProceedChoice("Should cheque be sent to customer?");
                if (continueType == ContinueTypes.Yes)
                {
                    string email = menu.GetEmail();
                    sendEmailWithAttachment.CombineEmail(email, orderID, fullChequeLocationPath);
                    Console.WriteLine("\nMail Sent! Double check in spam!\n");
                }
                tablesService.ChangeTableStatus(tableID, TableStatus.Available);

                break;

            case 8:
                return;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}
