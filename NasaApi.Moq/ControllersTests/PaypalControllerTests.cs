using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NasaApi.Controllers;
using NasaApi.Library.Queries;
using NasaApi.Moq.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NasaApi.Moq.ControllersTests
{
    public class PaypalControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private DefaultHttpContext _httpContext;
        private PaypalController _controller;
        public PaypalControllerTests()
        {
            _mockMediator = new Mock<IMediator>();

            _httpContext = new DefaultHttpContext();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(3)]
        public async Task GetOkHttpResponse(int days)
        {
            _mockMediator
            .Setup(m => m.Send(It.IsAny<GetTransactionsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ReadSamplePaypalJson.ReadFormatJson("paypal_ok_response.json"));

            _controller = new PaypalController(_mockMediator.Object);

            var result = await _controller.Get(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(days));

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Theory]
        [InlineData(32)]
        [InlineData(-2)]
        [InlineData(90)]
        public async Task GetBadRequestHttpResponse(int days)
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetTransactionsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ReadSamplePaypalJson.ReadFormatJson("paypal_ok_response.json"));

            _controller = new PaypalController(_mockMediator.Object);
            _controller.ControllerContext.HttpContext = _httpContext;

            var result = await _controller.Get(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(days));

            var badResult = result as BadRequestResult;

            Assert.NotNull(badResult);
            Assert.Equal(400, badResult?.StatusCode);
        }
    }
}
