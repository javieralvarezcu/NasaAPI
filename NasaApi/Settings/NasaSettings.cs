using Microsoft.Extensions.Options;

namespace NasaApi.Settings
{
    public class NasaSettings : IOptions<NasaSettings>
    {
        public string BaseUrl { get; set; }

        public string ApiKey { get; set; }

        public NasaSettings Value => this;
    }
}
