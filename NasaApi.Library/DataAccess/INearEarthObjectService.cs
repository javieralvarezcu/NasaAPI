using NasaApi.Library.Models;

namespace NasaApi.Library.DataAccess
{
    public interface INearEarthObjectService
    {
        Task<List<NearEarthObjectDTO>> GetAllNeosAsync(int days);
    }
}
