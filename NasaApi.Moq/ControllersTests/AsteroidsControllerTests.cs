using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NasaApi.Controllers;
using NasaApi.Library.Queries;
using NasaApi.Models.DTO;
using NasaApi.Moq.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NasaApi.Moq.ControllersTests
{
    public class AsteroidsControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private DefaultHttpContext _httpContext;
        private AsteroidsController _controller;
        public AsteroidsControllerTests()
        {
            _mockMediator = new Mock<IMediator>();

            _httpContext = new DefaultHttpContext();
        }

        [Theory]
        [InlineData(0)]
        public async Task GetOkEmptyHttpResponse(int number)
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetNeosListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(RandomDTONeosGenerator.GenerateNeosList(number));

            _controller = new AsteroidsController(_mockMediator.Object);

            var result = await _controller.Get(new Random().Next(6)+1);

            var okResult = result as StatusCodeResult;

            Assert.NotNull(okResult);
            Assert.Equal(204, okResult?.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(3)]
        public async Task GetOkHttpResponse(int number)
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetNeosListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(RandomDTONeosGenerator.GenerateNeosList(number));

            _controller = new AsteroidsController(_mockMediator.Object);

            var result = await _controller.Get(new Random().Next(6) + 1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(8)]
        public async Task GetBadRequestHttpResponse(int number)
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetNeosListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(RandomDTONeosGenerator.GenerateNeosList(new Random().Next(6)));

            _controller = new AsteroidsController(_mockMediator.Object);
            _controller.ControllerContext.HttpContext = _httpContext;

            var result = await _controller.Get(number);

            var badResult = result as BadRequestResult;

            Assert.NotNull(badResult);
            Assert.Equal(400, badResult?.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetOkHttpResponseCount(int number)
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetNeosListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(RandomDTONeosGenerator.GenerateNeosList(number));

            _controller = new AsteroidsController(_mockMediator.Object);

            var result = await _controller.Get(new Random().Next(6) + 1);

            var okResult = result as OkObjectResult;

            var neos = okResult?.Value as List<NearEarthObjectDTO>;

            Assert.NotNull(okResult);
            Assert.NotNull(neos);
            Assert.Equal(number, neos?.Count);
        }

        [Theory]
        [InlineData(0)]
        public async Task PostOkEmptyHttpResponse(int number)
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetNeosListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(RandomDTONeosGenerator.GenerateNeosList(number));

            _controller = new AsteroidsController(_mockMediator.Object);

            var result = await _controller.PostTop3HazardousNeos(new Random().Next(6) + 1);

            var okResult = result as StatusCodeResult;

            Assert.NotNull(okResult);
            Assert.Equal(204, okResult?.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(3)]
        public async Task PostOkHttpResponse(int number)
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetNeosListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(RandomDTONeosGenerator.GenerateNeosList(number));

            _controller = new AsteroidsController(_mockMediator.Object);
            _controller.ControllerContext.HttpContext = _httpContext;

            var result = await _controller.PostTop3HazardousNeos(new Random().Next(6) + 1);

            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(8)]
        public async Task PostBadRequestHttpResponse(int number)
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetNeosListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(RandomDTONeosGenerator.GenerateNeosList(new Random().Next(6)));

            _controller = new AsteroidsController(_mockMediator.Object);
            _controller.ControllerContext.HttpContext = _httpContext;

            var result = await _controller.PostTop3HazardousNeos(number);

            var badResult = result as BadRequestResult;

            Assert.NotNull(badResult);
            Assert.Equal(400, badResult?.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task PostOkHttpResponseCount(int number)
        {
            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetNeosListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(RandomDTONeosGenerator.GenerateNeosList(number));

            _controller = new AsteroidsController(_mockMediator.Object);
            _controller.ControllerContext.HttpContext = _httpContext;

            var result = await _controller.PostTop3HazardousNeos(new Random().Next(6) + 1);

            var okResult = result as OkObjectResult;

            var neos = okResult?.Value as List<NearEarthObjectDTO>;

            Assert.NotNull(okResult);
            Assert.NotNull(neos);
            Assert.Equal(number, neos?.Count);
        }
    }
}
