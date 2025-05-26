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
    public class WasteLossRecordPackagingServiceTest
    {
        private readonly Mock<IWasteLossRecordPackagingRepository> _mockRepo;
        private readonly WasteLossRecordPackagingController _controller;

        public WasteLossRecordPackagingServiceTest()
        {
            _mockRepo = new Mock<IWasteLossRecordPackagingRepository>();
            _controller = new WasteLossRecordPackagingController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetWasteLossRecordPackagings_ReturnsAllRecords()
        {
            // Arrange
            var records = new List<WasteLossRecordPackaging>
            {
                new WasteLossRecordPackaging { Id = 1, QuantityLoss = 10, Reason = "Broken", DateOfLoss = DateTime.UtcNow },
                new WasteLossRecordPackaging { Id = 2, QuantityLoss = 20, Reason = "Expired", DateOfLoss = DateTime.UtcNow }
            };

            _mockRepo.Setup(r => r.GetAllWasteLossRecordsAsync()).ReturnsAsync(records);

            // Act
            var result = await _controller.GetWasteLossRecordPackagings();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<WasteLossRecordPackagingDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetWasteLossRecordPackaging_ReturnsRecord_WhenExists()
        {
            // Arrange
            var record = new WasteLossRecordPackaging { Id = 1, QuantityLoss = 10, Reason = "Broken", DateOfLoss = DateTime.UtcNow };
            _mockRepo.Setup(r => r.GetWasteLossRecordByIdAsync(1)).ReturnsAsync(record);

            // Act
            var result = await _controller.GetWasteLossRecordPackaging(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<WasteLossRecordPackagingDTO>(okResult.Value);
            Assert.Equal(record.Id, dto.Id);
            Assert.Equal(record.QuantityLoss, dto.QuantityLoss);
        }

        [Fact]
        public async Task GetWasteLossRecordPackaging_ReturnsNotFound_WhenNotExists()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetWasteLossRecordByIdAsync(99)).ReturnsAsync((WasteLossRecordPackaging)null);

            // Act
            var result = await _controller.GetWasteLossRecordPackaging(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostWasteLossRecordPackaging_ReturnsCreated_WhenValid()
        {
            // Arrange
            var newRecord = new WasteLossRecordPackaging { Id = 1, QuantityLoss = 15, Reason = "Damaged", DateOfLoss = DateTime.UtcNow };
            _mockRepo.Setup(r => r.CreateWasteLossRecordAsync(It.IsAny<WasteLossRecordPackaging>())).ReturnsAsync(newRecord);

            // Act
            var result = await _controller.PostWasteLossRecordPackaging(newRecord);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var dto = Assert.IsType<WasteLossRecordPackagingDTO>(createdResult.Value);
            Assert.Equal(newRecord.Id, dto.Id);
        }

        [Fact]
        public async Task PutWasteLossRecordPackaging_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var record = new WasteLossRecordPackaging { Id = 1, QuantityLoss = 12, Reason = "Updated" };
            _mockRepo.Setup(r => r.UpdateWasteLossRecordAsync(record)).ReturnsAsync(record);

            // Act
            var result = await _controller.PutWasteLossRecordPackaging(1, record);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutWasteLossRecordPackaging_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var record = new WasteLossRecordPackaging { Id = 1, QuantityLoss = 12 };

            // Act
            var result = await _controller.PutWasteLossRecordPackaging(99, record);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PutWasteLossRecordPackaging_ReturnsNotFound_WhenUpdateFails()
        {
            // Arrange
            var record = new WasteLossRecordPackaging { Id = 1, QuantityLoss = 12 };
            _mockRepo.Setup(r => r.UpdateWasteLossRecordAsync(record)).ReturnsAsync((WasteLossRecordPackaging)null);

            // Act
            var result = await _controller.PutWasteLossRecordPackaging(1, record);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteWasteLossRecordPackaging_ReturnsNoContent_WhenDeleted()
        {
            // Arrange
            var record = new WasteLossRecordPackaging { Id = 1 };
            _mockRepo.Setup(r => r.DeleteWasteLossRecordAsync(1)).ReturnsAsync(record);

            // Act
            var result = await _controller.DeleteWasteLossRecordPackaging(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteWasteLossRecordPackaging_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteWasteLossRecordAsync(99)).ReturnsAsync((WasteLossRecordPackaging)null);

            // Act
            var result = await _controller.DeleteWasteLossRecordPackaging(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
