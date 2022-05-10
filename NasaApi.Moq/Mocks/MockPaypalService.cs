using Moq;
using NasaApi.Library.DataAccess;
using NasaApi.Moq.Utils;
using System;

namespace NasaApi.Moq.Mocks
{
    public static class MockPaypalService
    {
        public static string responseString = "";
        public static Mock<IPaypalService> GetTransactionsService(string filename)
        {
            responseString = ReadSamplePaypalJson.ReadFormatJson(filename);

            var mockRepo = new Mock<IPaypalService>();
            mockRepo.Setup(r => r.GetTransactionsByDate(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>())).ReturnsAsync(responseString);

            return mockRepo;
        }
    }
}
