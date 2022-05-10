
namespace NasaApi.Library.DataAccess
{
    public interface IPaypalService
    {
        Task<string> GetTransactionsByDate(DateTimeOffset start_date, DateTimeOffset end_date);
    }
}
