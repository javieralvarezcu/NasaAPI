using NasaApi.Models;

namespace NasaApi.Services
{
    public interface INearEarthObjectService
    {
        Task<IEnumerable<NearEarthObjectDTO>> GetAllNeosAsync(int days);
    }
}
