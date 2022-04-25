using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NasaApi.Library.DataAccess;
using NasaApi.Library.Settings;
using NasaApi.Models.DTO;
using NasaApi.Models.Raw;
using NasaApi.Moq.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NasaApi.Moq.ServicesTests
{
    public class NearEarthObjectServiceTests
    {
        private INearEarthObjectService _newEarthObjectsService;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        private Mock<IOptions<NasaSettings>> _mockNasaSettings;

        public NearEarthObjectServiceTests()
        {
            //Mocking app settings <NasaSettings>
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.tests.json")
                 .AddEnvironmentVariables()
                 .Build();

            _mockNasaSettings = new Mock<IOptions<NasaSettings>>();
            _mockNasaSettings.Setup(ap => ap.Value).Returns(new NasaSettings()
            {
                BaseUrl = config.GetSection(nameof(NasaSettings)).GetValue<string>("BaseUrl"),
                ApiKey = config.GetSection(nameof(NasaSettings)).GetValue<string>("ApiKey")
            });

            
        }

        [Theory]
        [InlineData(2, 3)]
        [InlineData(6, 1)]
        [InlineData(3, 3)]
        public async Task Successful_Service_Count_Test(int days, int neosPerDay)
        {
            _newEarthObjectsService = new NearEarthObjectService(
                MockHttpMessageHandlerGenerator(HttpFeedGenerator(days,neosPerDay)), _mockNasaSettings.Object);
            var result = await _newEarthObjectsService.GetAllNeosAsync(6);

            var neos = Assert.IsType<List<NearEarthObjectDTO>>(result);

            Assert.True(neos.Count() >= 0 && neos.Count() <= 3);
        }

        [Theory]
        [InlineData("123", "test1")]
        [InlineData("456", "test1")]
        [InlineData("789", "test1")]
        public async Task Successful_Parsing_Test(string id, string name)
        {
            //Creating the service with dummy args
            _newEarthObjectsService = new NearEarthObjectService(
                MockHttpMessageHandlerGenerator(HttpUniqueFeedGenerator(id, name)), _mockNasaSettings.Object);
            var result = await _newEarthObjectsService.GetAllNeosAsync(6);

            var neos = Assert.IsType<List<NearEarthObjectDTO>>(result);

            Assert.True(neos.Contains(new NearEarthObjectDTO { Id = id }));

            foreach (var neo in neos)
            {
                if(neo.Id == id)
                {
                    Assert.Equal(name, neo.Nombre);
                }
            }
        }


        private Rootobject HttpUniqueFeedGenerator(string id, string name)
        {
            //Initializing random data for Mock testing
            Dictionary<string, Near_Earth_Objects[]> observation = new Dictionary<string, Near_Earth_Objects[]>();
            RandomRawNeoGenerator randomRawNeo;

                Near_Earth_Objects[] observationArray = new Near_Earth_Objects[1];
                DateTime dateTime = DateTime.Now;

                    randomRawNeo = new RandomRawNeoGenerator()
                    {
                        NeoDate = dateTime,
                        Hazardous = true
                    };

                    observationArray[0] = randomRawNeo.Observation;
                    observationArray[0].id = id;
                    observationArray[0].name = name;


            observation.Add(dateTime.ToString("yyyy-MM-dd"), observationArray);

            Rootobject feed = new Rootobject { near_earth_objects = observation };
            return feed;
        }

        private Rootobject HttpFeedGenerator(int days, int neosPerDay)
        {
            //Initializing random data for Mock testing
            Dictionary<string, Near_Earth_Objects[]> observation = new Dictionary<string, Near_Earth_Objects[]>();
            RandomRawNeoGenerator randomRawNeo;

            for (int i = 0; i < days; i++)
            {
                Near_Earth_Objects[] observationArray = new Near_Earth_Objects[neosPerDay];
                DateTime dateTime = DateTime.Now.AddDays(i);

                for (int j = 0; j < neosPerDay; j++)
                {
                    randomRawNeo = new RandomRawNeoGenerator()
                    {
                        NeoDate = dateTime,
                        Hazardous = true
                    };
                    observationArray[j] = randomRawNeo.Observation;
                }

                observation.Add(dateTime.ToString("yyyy-MM-dd"), observationArray);
            }

            Rootobject feed = new Rootobject { near_earth_objects = observation };
            return feed;
        }

        private HttpClient MockHttpMessageHandlerGenerator(Rootobject feed)
        {
            //Mocking http client for dummy response
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(feed), Encoding.UTF8, "application/json")
                });
            HttpClient httpClientDouble = new HttpClient(_mockHttpMessageHandler.Object);
            return httpClientDouble;
        }
    }
    

}
