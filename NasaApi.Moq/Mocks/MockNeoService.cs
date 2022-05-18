using Moq;
using NasaApi.Library.DataAccess;
using NasaApi.Models.DTO;
using NasaApi.Moq.Utils;
using System.Collections.Generic;

namespace NasaApi.Moq.Mocks
{
    public static class MockNeoService
    {
        // TODO: Cuando creamos una clase, hay que razonar bien el ámbito de sus propiedades y métodos
        // esta propiedad debería ser private, es más, creo que no haría falta ni crear un campo de la clase, la declararía en el metodo donde se usa
        public static List<NearEarthObjectDTO> neosList = new List<NearEarthObjectDTO>();
        public static Mock<INearEarthObjectService> GetNeoService(int number)
        {
            // TODO: declarala aquí
            neosList = RandomDTONeosGenerator.GenerateNeosList(number);

            var mockRepo = new Mock<INearEarthObjectService>();
            mockRepo.Setup(r => r.GetAllNeosAsync(It.IsAny<int>())).ReturnsAsync(neosList);

            return mockRepo;
        }
    }
}