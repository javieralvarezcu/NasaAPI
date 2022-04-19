using System.Text.Json.Serialization;

namespace NasaApi.Models
{
    public class NearEarthObjectDTO
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public float Diametro { get; set; }
        public string Velocidad { get; set; }
        public string Fecha { get; set; }
        public string Planeta { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is NearEarthObjectDTO dTO &&
                   Id == dTO.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
