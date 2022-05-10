using Microsoft.Extensions.Options;

namespace NasaApi.Library.Settings
{
    public class PaypalSettings : IOptions<PaypalSettings>
    {
        public string? BaseUrl { get; set; }
        public string? TransactionsUrl { get; set; }
        public string? AuthUrl { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }

        public PaypalSettings Value => this;
    }
}
