using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NasaApi.Library.DataAccess;
using NasaApi.Library.Settings;
using NasaApi.Moq.Utils;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NasaApi.Moq.ServicesTests
{
    public class PaypalServiceTests
    {
        private IPaypalService _newPaypalService;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        private Mock<IOptions<PaypalSettings>> _mockPaypalSettings;

        public PaypalServiceTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.tests.json")
                 .AddEnvironmentVariables()
                 .Build();

            _mockPaypalSettings = new Mock<IOptions<PaypalSettings>>();
            _mockPaypalSettings.Setup(ap => ap.Value).Returns(new PaypalSettings()
            {
                BaseUrl = config.GetSection(nameof(PaypalSettings)).GetValue<string>("BaseUrl"),
                AuthUrl = config.GetSection(nameof(PaypalSettings)).GetValue<string>("AuthUrl"),
                TransactionsUrl = config.GetSection(nameof(PaypalSettings)).GetValue<string>("TransactionsUrl"),
                ClientId = config.GetSection(nameof(PaypalSettings)).GetValue<string>("ClientId"),
                ClientSecret = config.GetSection(nameof(PaypalSettings)).GetValue<string>("ClientSecret")
            });


        }

        [Theory]
        [InlineData("auth_ok_response.json")]
        public async Task Successful_Service_Parse(string filename)
        {
            _newPaypalService = new PaypalService(
                MockHttpMessageHandlerGenerator(filename), _mockPaypalSettings.Object);

            var result = await _newPaypalService.GetTransactionsByDate(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(new Random().Next(30) + 1));
            var responseJson = Assert.IsType<string>(result);

            Assert.Equal(ReadSamplePaypalJson.ReadFormatJson(filename), responseJson);
        }

        private HttpClient MockHttpMessageHandlerGenerator(string filename)
        {
            //Mocking http client for dummy response
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(ReadSamplePaypalJson.ReadFormatJson(filename), Encoding.UTF8, "application/json")
                });
            HttpClient httpClientDouble = new HttpClient(_mockHttpMessageHandler.Object);
            return httpClientDouble;
        }
    }
}
