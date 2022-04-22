using Moq;
using NasaApi.Models.DTO;
using NasaApi.Library.DataAccess;
using NasaApi.Library.Settings;
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

namespace NasaApi.Moq
{
    public class MoqTests
    {
    //    private readonly INearEarthObjectService _newEarthObjectsService;
    //    Mock<INearEarthObjectService> _mockEarthObjectsService = new Mock<INearEarthObjectService>();
    //    Mock<HttpMessageHandler> mockHttpMessageHandler = new Mock<HttpMessageHandler>();

    //    public MoqTests()
    //    {
    //        //Initializing random data for Mock testing
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

    //        //Mocking http client for dummy response
    //        mockHttpMessageHandler.Protected()
    //            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
    //            .ReturnsAsync(new HttpResponseMessage
    //            {
    //                StatusCode = HttpStatusCode.OK,
    //                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(feed), Encoding.UTF8, "application/json")
    //            });

    //        HttpClient httpClientDouble = new HttpClient(mockHttpMessageHandler.Object);
    //        _newEarthObjectsService = new NearEarthObjectService(httpClientDouble);
    //    }

    //    [Fact]
    //    public async void Successful_Controller_Count_Test()
    //    {
    //        //Arrange
    //        _mockEarthObjectsService.Setup(p => p.GetAllNeosAsync(It.IsAny<int>()))
    //            .ReturnsAsync(new List<NearEarthObjectDTO>() {
    //                new NearEarthObjectDTO(),
    //                new NearEarthObjectDTO(),
    //                new NearEarthObjectDTO()
    //            });
    //        var controller = new AsteroidsController(_mockEarthObjectsService.Object);

    //        //Act
    //        var result = await controller.Get(6);

    //        //Assert
    //        var okResult = result.Result as OkObjectResult;
    //        var neos = Assert.IsType<List<NearEarthObjectDTO>>(okResult.Value);

    //        Assert.Equal(3, neos.Count());
    //    }

    //    [Fact]
    //    public async Task Successful_Service_Count_Test()
    //    {
    //        //Arrange
    //        var controller = new AsteroidsController(_newEarthObjectsService);

    //        //Act
    //        var result = await controller.Get(6);
    //        var okResult = result.Result as OkObjectResult;

    //        var neos = Assert.IsType<List<NearEarthObjectDTO>>(okResult.Value);

    //        //Assert
    //        Assert.True(neos.Count()>=0 && neos.Count()<=3);
    //    }

    //    [Theory]
    //    [InlineData(-1)]
    //    [InlineData(9)]
    //    [InlineData(65)]
    //    public async Task Successful_Controller_Exception_Control(int days)
    //    {
    //        //Arrange
    //        var controller = new AsteroidsController(_newEarthObjectsService);

    //        //Act
    //        var result = await controller.Get(days);
    //        var badResult = result.Result as StatusCodeResult;

    //        //Assert
    //        Assert.Equal(400, badResult.StatusCode);
    //    }
    //}

    ////Ramdon NEO generator for mocking the NASA API response
    //public class RandomNeoGenerator
    //{
    //    public Near_Earth_Objects near_earth_objects;
    //    public bool Hazardous { get; set; }
    //    public DateTime NeoDate { get; set; }

    //    public Near_Earth_Objects Observation
    //    {
    //        get {
    //            var rand = new Random();

    //            near_earth_objects = new Near_Earth_Objects()
    //            {
    //                id = rand.Next(100).ToString(),
    //                name = "test_name_" + rand.Next(100),
    //                estimated_diameter = new Estimated_Diameter()
    //                {
    //                    meters = new Meters()
    //                    {
    //                        estimated_diameter_max = (float)rand.Next(100),
    //                        estimated_diameter_min = (float)rand.Next(100)
    //                    }
    //                },
    //                close_approach_data = new Close_Approach_Data[]
    //                {
    //                    new Close_Approach_Data()
    //                    {
    //                        close_approach_date = NeoDate.ToString("yyyy-MM-dd"),
    //                        orbiting_body = "Earth",
    //                        relative_velocity = new Relative_Velocity()
    //                        {
    //                            kilometers_per_hour=rand.Next(100000).ToString()
    //                        }

    //                    }
    //                },
    //                is_potentially_hazardous_asteroid = Hazardous
    //            };
    //            return near_earth_objects;
    //        }
    //    }
    }
}