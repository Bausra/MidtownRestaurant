namespace MidtownRestaurantSystem.Interfaces.IServices
{
    public interface IDatabaseService
    {
        void CreateDBtables();
        void ExecuteNonQuery(List<string> sql);
        void CreateInitialData();
    }
}
