using Moq;
using NasaApi.Library.DataAccess;
using NasaApi.Library.Handlers;
using NasaApi.Library.Queries;
using NasaApi.Models.DTO;
using NasaApi.Moq.Mocks;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NasaApi.Moq.QueriesTests
{
    public class GetNeosListQueryTests
    {
        // TODO: No necesitas esta propiedad aquí, úsala sólo en el método test y la declaras en él
        // si tuvieras más de un test, puedes usar el constructor de la clase
        // aunque como tienes esto hecho, no tiene mucho sentido. 

        private Mock<INearEarthObjectService> _mockRepo;
        

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(3)]
        public async Task GetNeosListTest(int number)
        {
            // TODO: var
            _mockRepo = MockNeoService.GetNeoService(number);


            var handler = new GetNeosListHandler(_mockRepo.Object);

            var result = await handler.Handle(new GetNeosListQuery(3), CancellationToken.None);

            var neos = Assert.IsType<List<NearEarthObjectDTO>>(result);

            Assert.Equal(number, neos.Count);

            foreach (var inputNeo in MockNeoService.neosList)
            {
                foreach (var outputNeo in result)
                {
                    // TODO: Assert.Contains(outputNeo, neos);
                    Assert.True(neos.Contains(outputNeo));
                }
            }
        }
    }
}
