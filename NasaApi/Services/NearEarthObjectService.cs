using NasaApi.Models;
using Newtonsoft.Json;
using System.Text;

namespace NasaApi.Services
{
    public class NearEarthObjectService : INearEarthObjectService
    {
        private readonly HttpClient _httpClient;
        private const string start_date_param = "start_date=";
        private const string end_date_param = "end_date=";
        private const string detailed_param = "detailed=false";
        private const string api_key_param = "api_key=Na1sKwJGK1HVeOF4Yx8aLNp4u8ygT5GSSMF26HQ2";

        

        public NearEarthObjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<NearEarthObjectDTO>> GetAllNeosAsync(int days)
        {
            //Variable declaration
            List<NearEarthObjectDTO> list = new List<NearEarthObjectDTO>();
            NearEarthObjectDTO neo;
            var feed = await _httpClient.GetFromJsonAsync<Rootobject>(Connection_String_Generator(days));

            //Data filtering and parsing into DTO
            if (feed != null)
            {
                foreach (var register in feed.near_earth_objects)
                {
                    foreach(var item in register.Value)
                    {
                        if (item != null)
                        {
                            neo = new NearEarthObjectDTO();
                            neo.Id = item.id;
                            neo.Nombre = item.name;
                            neo.Fecha = item.close_approach_data[0].close_approach_date;
                            neo.Velocidad = item.close_approach_data[0].relative_velocity.kilometers_per_hour;
                            neo.Diametro = DiameterCalc.Calc(item.estimated_diameter.meters.estimated_diameter_min,
                                item.estimated_diameter.meters.estimated_diameter_min);
                            neo.Planeta = item.close_approach_data[0].orbiting_body;

                            if (item.is_potentially_hazardous_asteroid && neo.Planeta.Equals("Earth") && !list.Contains(neo))
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

        public string Connection_String_Generator(int days)
        {
            DateTime today = DateTime.Now;
            DateTime nextday = today.AddDays(days);

            //URL parsing & data request
            return "feed?" + start_date_param + today.Date.ToString(
                "yyyy-MM-dd") + "&" + end_date_param + nextday.Date.ToString("yyyy-MM-dd") + "&" + detailed_param + "&" + api_key_param;
            
            //return url + start_date_param + today.Date.ToString(
            //    "yyyy-MM-dd") + "&" + end_date_param + nextday.Date.ToString("yyyy-MM-dd") + "&" + detailed_param + "&" + api_key_param;
        }
    }
}
