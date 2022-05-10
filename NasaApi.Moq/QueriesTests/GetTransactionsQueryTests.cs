using Moq;
using NasaApi.Library.DataAccess;
using NasaApi.Library.Handlers;
using NasaApi.Library.Queries;
using NasaApi.Moq.Mocks;
using NasaApi.Moq.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NasaApi.Moq.QueriesTests
{
    public class GetTransactionsQueryTests
    {
        private Mock<IPaypalService> _mockRepo;

        [Theory]
        [InlineData("paypal_ok_response.json")]
        public async Task GetTransactionsTest(string filename)
        {
            _mockRepo = MockPaypalService.GetTransactionsService(filename);


            var handler = new GetTransactionsQueryHandler(_mockRepo.Object);

            var result = await handler.Handle(new GetTransactionsQuery(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow), CancellationToken.None);

            var jsonResponse = Assert.IsType<string>(result);

            Assert.Equal(jsonResponse, ReadSamplePaypalJson.ReadFormatJson(filename));

        }
    }
}
