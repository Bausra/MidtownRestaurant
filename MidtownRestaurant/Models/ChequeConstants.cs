
namespace MidtownRestaurantSystem.Models
{
    public class ChequeConstants
    {
        public static readonly string pad = "".PadLeft(40, Convert.ToChar("-"));
        public static readonly string businessInformation= String.Join(
                                                           Environment.NewLine,
                                                           String.Format($"{" ",8}UAB 'Midtown Restaurant'{" ",8}"), 
                                                           String.Format($"{" ",8}Company code - 325446845{" ",8}"), 
                                                           String.Format($"{" ",7}P.Kuldiga str. 1-3, Bauska{" ",7}\n\n"));
        public static readonly string chequePurpose = String.Format($"FOOD SERVICES{" ",27}\n");
        public static readonly string cashier = String.Format($"\nCASHIER: Rita{" ",27}\n");
        public static readonly string gratitude = String.Format($"{" ",14}!Thank you!{" ",14}\n");

        public static readonly string fileSavingLocation = "C:\\cheques";
    }
}
