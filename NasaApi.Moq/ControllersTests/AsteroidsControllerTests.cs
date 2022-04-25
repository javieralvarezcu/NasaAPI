using MediatR;
using Moq;
using NasaApi.Library.Handlers;
using NasaApi.Library.Queries;
using NasaApi.Moq.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NasaApi.Moq.ControllersTests
{
    public class AsteroidsControllerTests
    {
        Mock<IMediator> _mockMediator;
        public AsteroidsControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMediator.Setup(m => m.Send(It.IsAny<GetNeosListQuery>, It.IsAny<CancellationToken>()))
                .Returns(async (GetNeosListQuery q, CancellationToken token) => await new GetNeosListHandler(MockNeoService.GetNeoService(3).Object).Handle(new GetNeosListQuery(3), token));
        }

        [Fact]
        public async Task GetOkHttpResponse()
        {

        }
    }
}
