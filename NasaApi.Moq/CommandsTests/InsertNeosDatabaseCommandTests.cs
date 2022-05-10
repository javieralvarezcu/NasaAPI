using NasaApi.Library.Commands;
using NasaApi.Library.Handlers;
using NasaApi.Models.DTO;
using NasaApi.Moq.Mocks;
using NasaApi.Moq.Utils;
using NasaApi.Persistence.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NasaApi.Moq.CommandsTests
{
    public class InsertNeosDatabaseCommandTests
    {
        private readonly MyDbContext _mockContext;

        public InsertNeosDatabaseCommandTests()
        {
            _mockContext = MockNeoContext.GetNeoContext();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(3)]
        public async Task InsertNeosListCommandTest(int number)
        {
            List<NearEarthObjectDTO> neosList = RandomDTONeosGenerator.GenerateNeosList(number);

            var handler = new InsertNeosDatabaseCommandHandler(_mockContext);

            var result = await handler.Handle(new InsertNeosDatabaseCommand(neosList), CancellationToken.None);

            var neos = Assert.IsType<List<NearEarthObjectDTO>>(result);

            Assert.Equal(number, neos.Count);

            foreach (var inputNeo in neosList)
            {
                foreach (var outputNeo in result)
                {
                    Assert.Contains(outputNeo, neos);
                }
            }
        }
    }
}
