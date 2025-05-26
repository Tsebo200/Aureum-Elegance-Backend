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
    public class StockRequestIngredientsServiceTest
    {
        private readonly Mock<IStockRequestIngredientsRepository> _mockRepo;
        private readonly StockRequestIngredientsController _controller;

        public StockRequestIngredientsServiceTest()
        {
            _mockRepo = new Mock<IStockRequestIngredientsRepository>();
            _controller = new StockRequestIngredientsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetStockRequestIngredients_ReturnsAllRequests()
        {
            // Arrange
            var requests = new List<StockRequestIngredients>
            {
                new StockRequestIngredients { Id = 1, AmountRequested = 100, Status = StockRequestIngredients.StockStatus.Pending },
                new StockRequestIngredients { Id = 2, AmountRequested = 200, Status = StockRequestIngredients.StockStatus.Approved }
            };

            _mockRepo.Setup(r => r.GetAllStockRequestIngredientsAsync()).ReturnsAsync(requests);

            // Act
            var result = await _controller.GetStockRequestIngredients();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<StockRequestIngredientsDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetStockRequestIngredientsById_ReturnsRequest_WhenExists()
        {
            // Arrange
            var request = new StockRequestIngredients
            {
                Id = 1,
                AmountRequested = 100,
                Status = StockRequestIngredients.StockStatus.Pending
            };

            _mockRepo.Setup(r => r.GetStockRequestIngredientsByIdAsync(1)).ReturnsAsync(request);

            // Act
            var result = await _controller.GetStockRequestIngredients(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<StockRequestIngredientsDTO>(okResult.Value);
            Assert.Equal(request.Id, dto.Id);
            Assert.Equal(request.AmountRequested, dto.AmountRequested);
        }

        [Fact]
        public async Task GetStockRequestIngredientsById_ReturnsNotFound_WhenNotExists()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetStockRequestIngredientsByIdAsync(99)).ReturnsAsync((StockRequestIngredients)null);

            // Act
            var result = await _controller.GetStockRequestIngredients(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostStockRequestIngredients_ReturnsCreated_WhenValid()
        {
            // Arrange
            var newRequest = new StockRequestIngredients
            {
                Id = 1,
                AmountRequested = 150,
                Status = StockRequestIngredients.StockStatus.Pending
            };

            _mockRepo.Setup(r => r.CreateStockRequestIngredientsAsync(It.IsAny<StockRequestIngredients>())).ReturnsAsync(newRequest);

            // Act
            var result = await _controller.PostStockRequestIngredients(newRequest);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var dto = Assert.IsType<StockRequestIngredientsDTO>(createdResult.Value);
            Assert.Equal(newRequest.Id, dto.Id);
        }

        [Fact]
        public async Task PutStockRequestIngredients_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var request = new StockRequestIngredients { Id = 1, AmountRequested = 180 };

            _mockRepo.Setup(r => r.UpdateStockRequestIngredientsAsync(request)).ReturnsAsync(request);

            // Act
            var result = await _controller.PutStockRequestIngredients(1, request);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutStockRequestIngredients_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var request = new StockRequestIngredients { Id = 1, AmountRequested = 180 };

            // Act
            var result = await _controller.PutStockRequestIngredients(5, request);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutStockRequestIngredients_ReturnsNotFound_WhenUpdateFails()
        {
            // Arrange
            var request = new StockRequestIngredients { Id = 1, AmountRequested = 180 };

            _mockRepo.Setup(r => r.UpdateStockRequestIngredientsAsync(request)).ReturnsAsync((StockRequestIngredients)null);

            // Act
            var result = await _controller.PutStockRequestIngredients(1, request);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteStockRequestIngredients_ReturnsNoContent_WhenDeleted()
        {
            // Arrange
            var request = new StockRequestIngredients { Id = 1 };

            _mockRepo.Setup(r => r.DeleteStockRequestIngredientsAsync(1)).ReturnsAsync(request);

            // Act
            var result = await _controller.DeleteStockRequestIngredients(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteStockRequestIngredients_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteStockRequestIngredientsAsync(99)).ReturnsAsync((StockRequestIngredients)null);

            // Act
            var result = await _controller.DeleteStockRequestIngredients(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
