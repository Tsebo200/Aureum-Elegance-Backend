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
    public class WasteLossRecordIngredientsServiceTest
    {
        private readonly Mock<IWasteLossRecordIngredientsRepository> _mockRepo;
        private readonly WasteLossRecordIngredientsController _controller;

        public WasteLossRecordIngredientsServiceTest()
        {
            _mockRepo = new Mock<IWasteLossRecordIngredientsRepository>();
            _controller = new WasteLossRecordIngredientsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetWasteLossRecordIngredients_ReturnsAllRecords()
        {
            // Arrange
            var records = new List<WasteLossRecordIngredients>
            {
                new WasteLossRecordIngredients { Id = 1, QuantityLoss = 10, Reason = "Spillage" },
                new WasteLossRecordIngredients { Id = 2, QuantityLoss = 20, Reason = "Expired" }
            };
            _mockRepo.Setup(r => r.GetAllWasteLossRecordsAsync()).ReturnsAsync(records);

            // Act
            var result = await _controller.GetWasteLossRecordIngredients();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<WasteLossRecordIngredientsDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetWasteLossRecordIngredientById_ReturnsRecord_WhenExists()
        {
            // Arrange
            var record = new WasteLossRecordIngredients
            {
                Id = 1,
                QuantityLoss = 10,
                Reason = "Spillage"
            };

            _mockRepo.Setup(r => r.GetWasteLossRecordByIdAsync(1)).ReturnsAsync(record);

            // Act
            var result = await _controller.GetWasteLossRecordIngredient(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<WasteLossRecordIngredientsDTO>(okResult.Value);
            Assert.Equal(record.Id, dto.Id);
            Assert.Equal(record.QuantityLoss, dto.QuantityLoss);
        }

        [Fact]
        public async Task GetWasteLossRecordIngredientById_ReturnsNotFound_WhenNotExists()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetWasteLossRecordByIdAsync(99)).ReturnsAsync((WasteLossRecordIngredients)null);

            // Act
            var result = await _controller.GetWasteLossRecordIngredient(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostWasteLossRecordIngredient_ReturnsCreated_WhenValid()
        {
            // Arrange
            var newRecord = new WasteLossRecordIngredients
            {
                Id = 1,
                QuantityLoss = 15,
                Reason = "Contamination"
            };

            _mockRepo.Setup(r => r.CreateWasteLossRecordAsync(It.IsAny<WasteLossRecordIngredients>())).ReturnsAsync(newRecord);

            // Act
            var result = await _controller.PostWasteLossRecordIngredient(newRecord);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var dto = Assert.IsType<WasteLossRecordIngredientsDTO>(createdResult.Value);
            Assert.Equal(newRecord.Id, dto.Id);
        }

        [Fact]
        public async Task PutWasteLossRecordIngredient_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var record = new WasteLossRecordIngredients { Id = 1, QuantityLoss = 12, Reason = "Leaked" };

            _mockRepo.Setup(r => r.UpdateWasteLossRecordAsync(record)).ReturnsAsync(record);

            // Act
            var result = await _controller.PutWasteLossRecordIngredient(1, record);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutWasteLossRecordIngredient_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var record = new WasteLossRecordIngredients { Id = 1, QuantityLoss = 12 };

            // Act
            var result = await _controller.PutWasteLossRecordIngredient(5, record);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID mismatch.", badRequest.Value);
        }

        [Fact]
        public async Task PutWasteLossRecordIngredient_ReturnsNotFound_WhenUpdateFails()
        {
            // Arrange
            var record = new WasteLossRecordIngredients { Id = 1, QuantityLoss = 10 };

            _mockRepo.Setup(r => r.UpdateWasteLossRecordAsync(record)).ReturnsAsync((WasteLossRecordIngredients)null);

            // Act
            var result = await _controller.PutWasteLossRecordIngredient(1, record);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteWasteLossRecordIngredient_ReturnsNoContent_WhenDeleted()
        {
            // Arrange
            var record = new WasteLossRecordIngredients { Id = 1 };

            _mockRepo.Setup(r => r.DeleteWasteLossRecordAsync(1)).ReturnsAsync(record);

            // Act
            var result = await _controller.DeleteWasteLossRecordIngredient(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteWasteLossRecordIngredient_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteWasteLossRecordAsync(99)).ReturnsAsync((WasteLossRecordIngredients)null);

            // Act
            var result = await _controller.DeleteWasteLossRecordIngredient(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
