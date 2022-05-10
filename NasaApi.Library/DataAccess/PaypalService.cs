
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using NasaApi.Library.Settings;
using NasaApi.Models.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NasaApi.Library.DataAccess
{
    public class PaypalService : IPaypalService
    {
        private readonly HttpClient _httpClient;
        private readonly PaypalSettings _paypalSettings;

        public PaypalService(HttpClient httpClient, IOptions<PaypalSettings> paypalSettings)
        {
            _httpClient = httpClient;
            _paypalSettings = paypalSettings.Value;
        }

        public async Task<string> GetTransactionsByDate(DateTimeOffset start_date, DateTimeOffset end_date)
        {
            _httpClient.BaseAddress = new Uri(_paypalSettings.BaseUrl);

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", TokenGenerator().Result);

            string jsonFormatted="";

            var stringTask = _httpClient.GetStringAsync(
                $"/v1/reporting/transactions?" +
                $"start_date={start_date.ToString("yyyy-MM-ddThh:mm:sszzz").Replace("+", "%2B")}&" +
                $"end_date={end_date.ToString("yyyy-MM-ddThh:mm:sszzz").Replace("+", "%2B")}");

            jsonFormatted = JValue.Parse(stringTask.Result).ToString(Formatting.Indented);

            return jsonFormatted;
        }

        private async Task<string> TokenGenerator()
        {

            _httpClient.DefaultRequestHeaders.Accept.Clear();

            var byteArray = Encoding.ASCII.GetBytes($"{_paypalSettings.ClientId}:{_paypalSettings.ClientSecret}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var dict = new Dictionary<string, string>();
            dict.Add("grant_type", "client_credentials");
            var req = new HttpRequestMessage(HttpMethod.Post, new Uri(_paypalSettings.AuthUrl)) { Content = new FormUrlEncodedContent(dict) };
            var response = _httpClient.SendAsync(req);
            var finalData = await response.Result.Content.ReadAsStringAsync();

            PaypalToken? paypalToken = JsonConvert.DeserializeObject<PaypalToken>(finalData);
            string bearerToken = $"{paypalToken.token_type} {paypalToken.access_token}";

            _httpClient.DefaultRequestHeaders.Clear();

            return bearerToken;
        }

        

    }
}
