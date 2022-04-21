using Microsoft.Extensions.Options;
using NasaApi.Models;
using NasaApi.Settings;
using Newtonsoft.Json;
using System.Text;

// quitar usings sin usar

namespace NasaApi.Services
{
    public class NearEarthObjectService : INearEarthObjectService
    {
        private readonly HttpClient _httpClient;
        private readonly NasaSettings _nasaSettings;
        private const string start_date_param = "start_date=";
        private const string end_date_param = "end_date=";
        private const string detailed_param = "detailed=false";
        private const string api_key_param = "api_key=";
        
        public NearEarthObjectService(HttpClient httpClient, IOptions<NasaSettings> nasaSettings)
        {
            _httpClient = httpClient;
            _nasaSettings = nasaSettings.Value;
        }

        public async Task<IEnumerable<NearEarthObjectDTO>> GetAllNeosAsync(int days)
        {
            //Variable declaration
            List<NearEarthObjectDTO> list = new List<NearEarthObjectDTO>();
            // TODO: no declarar aquí neo.
            //NearEarthObjectDTO neo;
                        
            _httpClient.BaseAddress = new Uri(_nasaSettings.BaseUrl);
            var feed = await _httpClient.GetFromJsonAsync<Rootobject>(Connection_String_Generator(days));            

            // TODO: yo hubiera hecho el bucle así
            var allAsteroids = feed?.near_earth_objects.SelectMany(s => s.Value).ToList();

            if (allAsteroids != null)
            {
                foreach (var asteroid in allAsteroids)
                {
                    foreach (var item in asteroid.close_approach_data)
                    {
                        // TODO: usa las sujerencias para, por ejemplo, simplificar inicializaciones de objetos
                        // TODO: neo se declara aquí dentro del bucle. Es en el ámbito donde se utiliza
                        var neo = new NearEarthObjectDTO
                        {
                            Id = asteroid.id,
                            Nombre = asteroid.name,
                            Fecha = item.close_approach_date,
                            Velocidad = item.relative_velocity.kilometers_per_hour,
                            Diametro = DiameterCalc.Calc(asteroid.estimated_diameter.meters.estimated_diameter_min,
                            asteroid.estimated_diameter.meters.estimated_diameter_min),
                            Planeta = item.orbiting_body
                        };

                        if (asteroid.is_potentially_hazardous_asteroid && neo.Planeta.Equals("Earth") && !list.Contains(neo))
                        {
                            list.Add(neo);
                        }
                    }
                }
            }

            //Data filtering and parsing into DTO
           /* if (feed != null)
            {
                foreach (var register in feed.near_earth_objects)
                {
                    foreach(var item in register.Value)
                    {
                        if (item != null)
                        {
                            // TODO: usa las sujerencias para, por ejemplo, simplificar inicializaciones de objetos
                            // TODO: neo se declara aquí dentro del bucle. Es en el ámbito donde se utiliza
                            var neo = new NearEarthObjectDTO
                            {
                                Id = item.id,
                                Nombre = item.name,
                                Fecha = item.close_approach_data[0].close_approach_date,
                                Velocidad = item.close_approach_data[0].relative_velocity.kilometers_per_hour,
                                Diametro = DiameterCalc.Calc(item.estimated_diameter.meters.estimated_diameter_min,
                                item.estimated_diameter.meters.estimated_diameter_min),
                                Planeta = item.close_approach_data[0].orbiting_body
                            };

                            if (item.is_potentially_hazardous_asteroid && neo.Planeta.Equals("Earth") && !list.Contains(neo))
                            {
                                list.Add(neo);
                            }
                        }
                    }
                }
            }*/

            //Return just the top 3 biggest potentially hazardous asteroids
            return list.OrderByDescending(x => x.Diametro).Take(3).ToList();
        }

        // TODO: Este método debe ser private
        public string Connection_String_Generator(int days)
        {
            DateTime today = DateTime.Now;
            DateTime nextday = today.AddDays(days);

            //URL parsing & data request

            // es mucho más legible la interpolación de cadenas!!!
            // https://docs.microsoft.com/es-es/dotnet/csharp/language-reference/tokens/interpolated
            
            return $"feed?{start_date_param}{today.Date:yyy-MM-dd}" +
                $"&{end_date_param}{nextday.Date:yyyy-MM-dd}" +
                $"&{detailed_param}" +
                $"&{api_key_param}{_nasaSettings.ApiKey}";

            //return "feed?" + start_date_param + today.Date.ToString(
                //"yyyy-MM-dd") + "&" + end_date_param + nextday.Date.ToString("yyyy-MM-dd") + "&" + detailed_param + "&" + api_key_param;
            
            //TODO: no se dejan comentarios

            //return url + start_date_param + today.Date.ToString(
            //    "yyyy-MM-dd") + "&" + end_date_param + nextday.Date.ToString("yyyy-MM-dd") + "&" + detailed_param + "&" + api_key_param;
        }
    }
}
