using Moq;
using NasaApi.Models.DTO;
using NasaApi.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;
using System.Linq;
using NasaApi.Controllers;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq.Protected;
using NasaApi.Models.Raw;
using NasaApi.Models.Raw.Diameter;
using MediatR;
using NasaApi.Library.Settings;
using Microsoft.Extensions.Options;
using NasaApi.Library.Queries;

namespace NasaApi.Moq
{
    //public class MoqTests
    //{
    //    private readonly INearEarthObjectService _newEarthObjectsService;
    //    Mock<INearEarthObjectService> _mockEarthObjectsService = new Mock<INearEarthObjectService>();
    //    Mock<HttpMessageHandler> _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
    //    Mock<IOptions<NasaSettings>> _mockNasaSettings;
    //    Mock<IMediator> _mockMediator;

    //    public MoqTests()
    //    {
    //        Initializing random data for Mock testing
    //        Dictionary<string, Near_Earth_Objects[]> observation = new Dictionary<string, Near_Earth_Objects[]>();
    //        RandomNeoGenerator randomNeoHazardous;

    //        for (int i = 0; i < 3; i++)
    //        {
    //            Near_Earth_Objects[] observationArray = new Near_Earth_Objects[4];
    //            DateTime dateTime = DateTime.Now.AddDays(i);

    //            for (int j = 0; j < 2; j++)
    //            {
    //                randomNeoHazardous = new RandomNeoGenerator()
    //                {
    //                    NeoDate = dateTime,
    //                    Hazardous = true
    //                };
    //                observationArray[j] = randomNeoHazardous.Observation;
    //            }

    //            observation.Add(dateTime.ToString("yyyy-MM-dd"), observationArray);
    //        }

    //        Rootobject feed = new Rootobject { near_earth_objects = observation };

    //        Mocking http client for dummy response
    //        _mockHttpMessageHandler.Protected()
    //            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
    //            .ReturnsAsync(new HttpResponseMessage
    //            {
    //                StatusCode = HttpStatusCode.OK,
    //                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(feed), Encoding.UTF8, "application/json")
    //            });


    //        HttpClient httpClientDouble = new HttpClient(_mockHttpMessageHandler.Object);
    //        _mockNasaSettings = new Mock<IOptions<NasaSettings>>();
    //        _mockNasaSettings.Setup(ap => ap.Value).Returns(new NasaSettings());

    //        _newEarthObjectsService = new NearEarthObjectService(httpClientDouble, _mockNasaSettings.Object);

    //        _mockMediator
    //.Setup(m => m.Send(It.IsAny<GetNeosListQuery>(), It.IsAny<CancellationToken>()))
    //.ReturnsAsync(new List<NearEarthObjectDTO>()
    //{

    //})
    //.Verifiable("Notification was not sent.");
    //    }

    //    [Fact]
    //    public async void Successful_Controller_Count_Test()
    //    {
    //        Arrange
    //        _mockEarthObjectsService.Setup(p => p.GetAllNeosAsync(It.IsAny<int>()))
    //            .ReturnsAsync(new List<NearEarthObjectDTO>() {
    //                new NearEarthObjectDTO(),
    //                new NearEarthObjectDTO(),
    //                new NearEarthObjectDTO()
    //            });
    //        var controller = new AsteroidsController(_mockEarthObjectsService.Object);

    //        Act
    //        var result = await controller.Get(6);

    //        Assert
    //        var okResult = result.Result as OkObjectResult;
    //        var neos = Assert.IsType<List<NearEarthObjectDTO>>(okResult.Value);

    //        Assert.Equal(3, neos.Count());
    //    }

    //    [Fact]
    //    public async Task Successful_Service_Count_Test()
    //    {
    //        Arrange
    //        var controller = new AsteroidsController(_newEarthObjectsService);

    //        Act
    //        var result = await controller.Get(6);
    //        var okResult = result.Result as OkObjectResult;

    //        var neos = Assert.IsType<List<NearEarthObjectDTO>>(okResult.Value);

    //        Assert
    //        Assert.True(neos.Count() >= 0 && neos.Count() <= 3);
    //    }

    //    [Theory]
    //    [InlineData(-1)]
    //    [InlineData(9)]
    //    [InlineData(65)]
    //    public async Task Successful_Controller_Exception_Control(int days)
    //    {
    //        Arrange
    //        var controller = new AsteroidsController(_newEarthObjectsService);

    //        Act
    //        var result = await controller.Get(days);
    //        var badResult = result.Result as StatusCodeResult;

    //        Assert
    //        Assert.Equal(400, badResult.StatusCode);
    //    }
    //}
}
