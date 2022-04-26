using Microsoft.Extensions.Options;
using NasaApi.Library.Settings;
using NasaApi.Models.DTO;
using NasaApi.Models.Raw;
using System.Net.Http.Json;

namespace NasaApi.Library.DataAccess
{
    public class NearEarthObjectService : INearEarthObjectService
    {
        private readonly HttpClient _httpClient;
        private readonly NasaSettings _nasaSettings;

        public NearEarthObjectService(HttpClient httpClient, IOptions<NasaSettings> nasaSettings)
        {
            _httpClient = httpClient;
            _nasaSettings = nasaSettings.Value;
        }

        public async Task<List<NearEarthObjectDTO>> GetAllNeosAsync(int days)
        {
            //Variable declaration
            List<NearEarthObjectDTO> list = new List<NearEarthObjectDTO>();
            Rootobject feed = new Rootobject();

            _httpClient.BaseAddress = new Uri(_nasaSettings.BaseUrl);

                feed = await _httpClient.GetFromJsonAsync<Rootobject>(Connection_String_Generator(days));

            var allAsteroids = feed?.near_earth_objects.SelectMany(s => s.Value).ToList();

            if (allAsteroids != null)
            {
                foreach (var asteroid in allAsteroids)
                {
                    if (asteroid !=null)
                    {
                        foreach (var item in asteroid.close_approach_data)
                        {
                            var neo = new NearEarthObjectDTO
                            {
                                Id = asteroid.id,
                                Nombre = asteroid.name,
                                Fecha = item.close_approach_date,
                                Velocidad = item.relative_velocity.kilometers_per_hour,
                                Diametro = (asteroid.estimated_diameter.meters.estimated_diameter_min +
                                asteroid.estimated_diameter.meters.estimated_diameter_min) / 2,
                                Planeta = item.orbiting_body
                            };

                            if (asteroid.is_potentially_hazardous_asteroid && neo.Planeta.Equals("Earth") && !list.Contains(neo))
                            {
                                list.Add(neo);
                            }
                        }
                    }
                }
            }

            //Return just the top 3 biggest potentially hazardous asteroids
            return list.OrderByDescending(x => x.Diametro).Take(3).ToList();
        }

        public async Task<List<NearEarthObjectDTO>> PostTop3HazardousNeosAsync(int days)
        {
            List<NearEarthObjectDTO> top3 = new List<NearEarthObjectDTO>();
            return top3;
        }

        private string Connection_String_Generator(int days)
        {
            DateTime today = DateTime.Now;
            DateTime nextday = today.AddDays(days);

            //URL parsing & data request

            return $"feed?start_date={today.Date:yyyy-MM-dd}" +
                $"&end_date={nextday.Date:yyyy-MM-dd}" +
                $"&detailed=false&api_key={_nasaSettings.ApiKey}";
        }
    }
}
