using MidtownRestaurantSystem.Repositories;
using MidtownRestaurantSystem.Models;
using MidtownRestaurantSystem.Interfaces.IServices;
using MidtownRestaurantSystem.Interfaces.Repositories;

namespace MidtownRestaurantSystem.Services
{
    public class ChequeService : IChequeService
    {
        private IOrderLinesRepository _orderLinesRepository;
        private IOrderHistoryForReportingRepository _orderHistoryForReportingRepository;

        public ChequeService(IOrderLinesRepository orderLinesRepository, IOrderHistoryForReportingRepository orderHistoryForReportingRepository) 
        { 
            _orderLinesRepository = orderLinesRepository;
            _orderHistoryForReportingRepository = orderHistoryForReportingRepository;
        }

        private string GetChequeTemplate(int orderID)
        {
            List<OrderLine> orderLines = _orderLinesRepository.GetOrderLinesForOrder(orderID);
            OrderHistoryForReporting orderHistory = _orderHistoryForReportingRepository.GetOrderHistory(orderID);

            string result = "";
            result += ChequeConstants.businessInformation;
            result += ChequeConstants.chequePurpose;
            result += ChequeConstants.pad;

            double totalPrice = 0;
            foreach(OrderLine line in orderLines)
            {
                double totalItemPrice = line.Quantity * line.UnitPrice;
                totalPrice += totalItemPrice;
                result += String.Format($"\n- {line.MenuItem,-17}{line.Quantity,5}{String.Format("{0:0.00}", line.UnitPrice),8}{String.Format("{0:0.00}", totalItemPrice),8}\n");
            }

            result += ChequeConstants.pad;
            result += String.Format($"\nTOTAL Eur:{String.Format("{0:0.00}", totalPrice * 1.21),30}\n");
            result += String.Format($"VAT Eur:{String.Format("{0:0.00}", (totalPrice * 1.21) - totalPrice),32}\n");
            result += ChequeConstants.pad;
            result += ChequeConstants.cashier;
            result += ChequeConstants.pad;

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(orderHistory.CompletenessTimestamp).ToLocalTime();
            result += String.Format($"\nDate:{dateTime,21}{" ",14}\n");
            result += String.Format($"Table number: {orderHistory.TableID,-3}{" ",23}\n");
            result += String.Format($"Cheque/order number: {orderHistory.Id,-8}{" ",11}\n\n");
            result += ChequeConstants.gratitude;

            return result;
        }

        public string SaveStringToPdfFile(int orderID)
        {
            string chequeTemplate = GetChequeTemplate(orderID);
            OrderHistoryForReporting orderHistory = _orderHistoryForReportingRepository.GetOrderHistory(orderID);

            CreateDirectory(ChequeConstants.fileSavingLocation.ToString());

            string directory = $"{ChequeConstants.fileSavingLocation}\\{orderHistory.Id}cheque.txt";

            File.WriteAllText(directory, chequeTemplate);

            return directory;
        }

        private void CreateDirectory(string fileSavingLocation)
        {
            try
            {
                if (!Directory.Exists(fileSavingLocation))
                {
                    DirectoryInfo di = Directory.CreateDirectory(fileSavingLocation);
                }
            }
            catch
            {
                throw new Exception("\nError occured while creating directory!");
            }
        }
    }
}
