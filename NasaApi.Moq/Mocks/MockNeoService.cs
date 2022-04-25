using Moq;
using NasaApi.Models.DTO;
using NasaApi.Library.DataAccess;
using System.Collections.Generic;
using NasaApi.Moq.Utils;

namespace NasaApi.Moq.Mocks
{
    public static class MockNeoService
    {
        public static List<NearEarthObjectDTO> neosList = new List<NearEarthObjectDTO>();
        public static Mock<INearEarthObjectService> GetNeoService(int number)
        {
            neosList = RandomDTONeosGenerator.GenerateNeosList(number);

            var mockRepo = new Mock<INearEarthObjectService>();
            mockRepo.Setup(r => r.GetAllNeosAsync(It.IsAny<int>())).ReturnsAsync(neosList);

            return mockRepo;
        }
    }
}