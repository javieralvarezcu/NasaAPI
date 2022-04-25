using Microsoft.AspNetCore.Mvc.Testing;
using NasaApi.Models.DTO;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NasaApi.Tests
{
    public class ApiTests
    {
        private WebApplicationFactory<Program> webAppFactory;
        private HttpClient httpClient;

        [SetUp]
        public void Setup()
        {
            webAppFactory = new WebApplicationFactory<Program>();
            httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(7)]
        public async Task Successful_Status_Code_Test(int value)
        {
            var response = await httpClient.GetAsync($"/asteroids?days={value}");

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestCase("/asteroids?days=9")]
        [TestCase("/asteroides?days=3")]
        [TestCase("")]
        public async Task Unsuccessful_Status_Code_Test(string value)
        {
            var response = await httpClient.GetAsync(value);

            Assert.IsTrue(!response.IsSuccessStatusCode);
        }

        [TestCase(-1)]
        [TestCase(9)]
        public async Task Bad_Request_Status_Code_Test(int value)
        {
            var response = await httpClient.GetAsync($"/asteroids?days={value}");

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(7)]
        public async Task Successful_Element_Count_Test(int value)
        {
            var feed = await httpClient.GetFromJsonAsync<List<NearEarthObjectDTO>>($"/asteroids?days={value}");

            Assert.IsTrue(feed.Count <= 3 && feed.Count >= 0);
        }


    }
}