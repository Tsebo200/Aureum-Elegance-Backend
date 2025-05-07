using System;
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
    public class WasteLossRecordFragranceServiceTest
    {
        private readonly Mock<IWasteLossRecordFragranceRepository> _mockRepo;
        private readonly WasteLossRecordFragranceController _controller;

        public WasteLossRecordFragranceServiceTest()
        {
            _mockRepo = new Mock<IWasteLossRecordFragranceRepository>();
            _controller = new WasteLossRecordFragranceController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllWasteLossRecords_ReturnsAllRecords()
        {
            // Arrange
            var records = new List<WasteLossRecordFragrance>
            {
                new WasteLossRecordFragrance { Id = 1, QuantityLoss = 5, Reason = "Spill", DateOfLoss = DateTime.UtcNow },
                new WasteLossRecordFragrance { Id = 2, QuantityLoss = 3, Reason = "Evaporation", DateOfLoss = DateTime.UtcNow }
            };
            _mockRepo.Setup(r => r.GetAllWasteLossRecordsAsync()).ReturnsAsync(records);

            // Act
            var result = await _controller.GetAllWasteLossRecords();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dtoList = Assert.IsAssignableFrom<IEnumerable<WasteLossRecordFragranceDTO>>(okResult.Value);
            Assert.Equal(2, dtoList.Count());
        }

        [Fact]
        public async Task GetWasteLossRecordById_ReturnsRecord_WhenExists()
        {
            // Arrange
            var record = new WasteLossRecordFragrance { Id = 1, QuantityLoss = 4, Reason = "Leak", DateOfLoss = DateTime.UtcNow };
            _mockRepo.Setup(r => r.GetWasteLossRecordByIdAsync(1)).ReturnsAsync(record);

            // Act
            var result = await _controller.GetWasteLossRecordById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<WasteLossRecordFragranceDTO>(okResult.Value);
            Assert.Equal(record.Id, dto.Id);
        }

        [Fact]
        public async Task GetWasteLossRecordById_ReturnsNotFound_WhenNotExists()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetWasteLossRecordByIdAsync(99)).ReturnsAsync((WasteLossRecordFragrance)null);

            // Act
            var result = await _controller.GetWasteLossRecordById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateWasteLossRecord_ReturnsCreated_WhenValid()
        {
            // Arrange
            var record = new WasteLossRecordFragrance { Id = 1, QuantityLoss = 5, Reason = "Broken bottle", DateOfLoss = DateTime.UtcNow };
            _mockRepo.Setup(r => r.CreateWasteLossRecordAsync(It.IsAny<WasteLossRecordFragrance>())).ReturnsAsync(record);

            // Act
            var result = await _controller.CreateWasteLossRecord(record);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var dto = Assert.IsType<WasteLossRecordFragranceDTO>(createdResult.Value);
            Assert.Equal(record.Id, dto.Id);
        }

        [Fact]
        public async Task UpdateWasteLossRecord_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var record = new WasteLossRecordFragrance { Id = 1, QuantityLoss = 6, Reason = "Accidental mix" };
            _mockRepo.Setup(r => r.UpdateWasteLossRecordAsync(record)).ReturnsAsync(record);

            // Act
            var result = await _controller.UpdateWasteLossRecord(1, record);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<WasteLossRecordFragranceDTO>(okResult.Value);
            Assert.Equal(record.QuantityLoss, dto.QuantityLoss);
        }

        [Fact]
        public async Task UpdateWasteLossRecord_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var record = new WasteLossRecordFragrance { Id = 1, QuantityLoss = 6 };

            // Act
            var result = await _controller.UpdateWasteLossRecord(5, record);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Record ID mismatch.", badResult.Value);
        }

        [Fact]
        public async Task UpdateWasteLossRecord_ReturnsNotFound_WhenRecordNotExists()
        {
            // Arrange
            var record = new WasteLossRecordFragrance { Id = 1, QuantityLoss = 6 };
            _mockRepo.Setup(r => r.UpdateWasteLossRecordAsync(record)).ReturnsAsync((WasteLossRecordFragrance)null);

            // Act
            var result = await _controller.UpdateWasteLossRecord(1, record);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task DeleteWasteLossRecord_ReturnsOk_WhenDeleted()
        {
            // Arrange
            var record = new WasteLossRecordFragrance { Id = 1, QuantityLoss = 4, Reason = "Spilled" };
            _mockRepo.Setup(r => r.DeleteWasteLossRecordAsync(1)).ReturnsAsync(record);

            // Act
            var result = await _controller.DeleteWasteLossRecord(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<WasteLossRecordFragranceDTO>(okResult.Value);
            Assert.Equal(record.Id, dto.Id);
        }

        [Fact]
        public async Task DeleteWasteLossRecord_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteWasteLossRecordAsync(99)).ReturnsAsync((WasteLossRecordFragrance)null);

            // Act
            var result = await _controller.DeleteWasteLossRecord(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
