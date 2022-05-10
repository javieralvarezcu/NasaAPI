using Moq;
using NasaApi.Library.DataAccess;
using NasaApi.Models.DTO;
using NasaApi.Moq.Utils;
using System.Collections.Generic;

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