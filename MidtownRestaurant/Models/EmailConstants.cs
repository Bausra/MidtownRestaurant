
namespace MidtownRestaurantSystem.Models
{
    public static class EmailConstants
    {
        public static readonly string emailSubject = "Invoice from Midtown Restaurant";
        public static readonly string greeting = "Dear customer,";
        public static readonly string purposeOfEmail = $"Please find attached an invoice No.";
        public static readonly string thankYou = $"Thank you for using our services.";
        public static readonly string regards = String.Join(
                                                            Environment.NewLine,
                                                            "Yours sincerely,",
                                                            "Midtown Restaurant.");
    }
}
