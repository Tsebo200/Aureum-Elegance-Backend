using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Mystefy.Controllers;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;
using Xunit;

namespace MystefyUnitTest.Tests
{
    public class StockRequestPackagingsServiceTest
    {
        private readonly Mock<IStockRequestPackagingsRepository> _mockRepo;
        private readonly StockRequestPackagingsController _controller;

        public StockRequestPackagingsServiceTest()
        {
            _mockRepo = new Mock<IStockRequestPackagingsRepository>();
            _controller = new StockRequestPackagingsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetStockRequestPackagings_ReturnsAllRequests()
        {
            // Arrange
            var requests = new List<StockRequestPackagings>
            {
                new StockRequestPackagings { Id = 1, AmountRequested = 100, Status = StockRequestPackagings.StockPackagingStatus.Pending },
                new StockRequestPackagings { Id = 2, AmountRequested = 200, Status = StockRequestPackagings.StockPackagingStatus.Approved }
            };

            _mockRepo.Setup(r => r.GetAllStockRequestPackagingsAsync()).ReturnsAsync(requests);

            // Act
            var result = await _controller.GetStockRequestPackagings();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<StockRequestPackagingsDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetStockRequestPackagingsById_ReturnsRequest_WhenExists()
        {
            // Arrange
            var request = new StockRequestPackagings
            {
                Id = 1,
                AmountRequested = 100,
                Status = StockRequestPackagings.StockPackagingStatus.Pending
            };

            _mockRepo.Setup(r => r.GetStockRequestPackagingsByIdAsync(1)).ReturnsAsync(request);

            // Act
            var result = await _controller.GetStockRequestPackagings(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<StockRequestPackagingsDTO>(okResult.Value);
            Assert.Equal(request.Id, dto.Id);
            Assert.Equal(request.AmountRequested, dto.AmountRequested);
        }

        [Fact]
        public async Task GetStockRequestPackagingsById_ReturnsNotFound_WhenNotExists()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetStockRequestPackagingsByIdAsync(99)).ReturnsAsync((StockRequestPackagings)null);

            // Act
            var result = await _controller.GetStockRequestPackagings(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostStockRequestPackagings_ReturnsCreated_WhenValid()
        {
            // Arrange
            var newRequest = new StockRequestPackagings
            {
                Id = 1,
                AmountRequested = 150,
                Status = StockRequestPackagings.StockPackagingStatus.Pending
            };

            _mockRepo.Setup(r => r.CreateStockRequestPackagingsAsync(It.IsAny<StockRequestPackagings>())).ReturnsAsync(newRequest);

            // Act
            var result = await _controller.PostStockRequestPackagings(newRequest);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var dto = Assert.IsType<StockRequestPackagingsDTO>(createdResult.Value);
            Assert.Equal(newRequest.Id, dto.Id);
        }

        [Fact]
        public async Task PutStockRequestPackagings_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var request = new StockRequestPackagings { Id = 1, AmountRequested = 180 };

            _mockRepo.Setup(r => r.UpdateStockRequestPackagingsAsync(request)).ReturnsAsync(request);

            // Act
            var result = await _controller.PutStockRequestPackagings(1, request);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutStockRequestPackagings_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var request = new StockRequestPackagings { Id = 1, AmountRequested = 180 };

            // Act
            var result = await _controller.PutStockRequestPackagings(5, request);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutStockRequestPackagings_ReturnsNotFound_WhenUpdateFails()
        {
            // Arrange
            var request = new StockRequestPackagings { Id = 1, AmountRequested = 180 };

            _mockRepo.Setup(r => r.UpdateStockRequestPackagingsAsync(request)).ReturnsAsync((StockRequestPackagings)null);

            // Act
            var result = await _controller.PutStockRequestPackagings(1, request);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteStockRequestPackagings_ReturnsNoContent_WhenDeleted()
        {
            // Arrange
            var request = new StockRequestPackagings { Id = 1 };

            _mockRepo.Setup(r => r.DeleteStockRequestPackagingsAsync(1)).ReturnsAsync(request);

            // Act
            var result = await _controller.DeleteStockRequestPackagings(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteStockRequestPackagings_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteStockRequestPackagingsAsync(99)).ReturnsAsync((StockRequestPackagings)null);

            // Act
            var result = await _controller.DeleteStockRequestPackagings(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
