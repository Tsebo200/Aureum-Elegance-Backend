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
    public class WasteLossRecordBatchFinishedProductsServiceTest
    {
        private readonly Mock<IWasteLossRecordBatchFinishedProductsRepository> _mockRepo;
        private readonly WasteLossRecordBatchFinishedProductsController _controller;

        public WasteLossRecordBatchFinishedProductsServiceTest()
        {
            _mockRepo = new Mock<IWasteLossRecordBatchFinishedProductsRepository>();
            _controller = new WasteLossRecordBatchFinishedProductsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllWasteLossRecords_ReturnsAllRecords()
        {
            // Arrange
            var records = new List<WasteLossRecordBatchFinishedProducts>
            {
                new WasteLossRecordBatchFinishedProducts { Id = 1, QuantityLoss = 10, Reason = "Leakage" },
                new WasteLossRecordBatchFinishedProducts { Id = 2, QuantityLoss = 20, Reason = "Spillage" }
            };
            _mockRepo.Setup(repo => repo.GetAllWasteLossRecordsAsync()).ReturnsAsync(records);

            // Act
            var result = await _controller.GetAllWasteLossRecords();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<WasteLossRecordBatchFinishedProductsDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetWasteLossRecordById_ReturnsRecord_WhenFound()
        {
            // Arrange
            var record = new WasteLossRecordBatchFinishedProducts { Id = 1, QuantityLoss = 15, Reason = "Damaged" };
            _mockRepo.Setup(repo => repo.GetWasteLossRecordByIdAsync(1)).ReturnsAsync(record);

            // Act
            var result = await _controller.GetWasteLossRecordById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<WasteLossRecordBatchFinishedProductsDTO>(okResult.Value);
            Assert.Equal(record.Id, dto.Id);
            Assert.Equal(record.QuantityLoss, dto.QuantityLoss);
        }

        [Fact]
        public async Task GetWasteLossRecordById_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetWasteLossRecordByIdAsync(99)).ReturnsAsync((WasteLossRecordBatchFinishedProducts)null);

            // Act
            var result = await _controller.GetWasteLossRecordById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateWasteLossRecord_ReturnsCreated_WhenValid()
        {
            // Arrange
            var newRecord = new WasteLossRecordBatchFinishedProducts { Id = 1, QuantityLoss = 5, Reason = "Breakage" };
            _mockRepo.Setup(repo => repo.CreateWasteLossRecordAsync(It.IsAny<WasteLossRecordBatchFinishedProducts>())).ReturnsAsync(newRecord);

            // Act
            var result = await _controller.CreateWasteLossRecord(newRecord);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var dto = Assert.IsType<WasteLossRecordBatchFinishedProductsDTO>(createdResult.Value);
            Assert.Equal(newRecord.Id, dto.Id);
        }

        [Fact]
        public async Task UpdateWasteLossRecord_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var updatedRecord = new WasteLossRecordBatchFinishedProducts { Id = 2, QuantityLoss = 8 };

            // Act
            var result = await _controller.UpdateWasteLossRecord(1, updatedRecord);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Record ID mismatch.", badRequest.Value);
        }

        [Fact]
        public async Task UpdateWasteLossRecord_ReturnsNotFound_WhenNotExists()
        {
            // Arrange
            var updatedRecord = new WasteLossRecordBatchFinishedProducts { Id = 1, QuantityLoss = 8 };
            _mockRepo.Setup(repo => repo.UpdateWasteLossRecordAsync(updatedRecord)).ReturnsAsync((WasteLossRecordBatchFinishedProducts)null);

            // Act
            var result = await _controller.UpdateWasteLossRecord(1, updatedRecord);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task UpdateWasteLossRecord_ReturnsOk_WhenUpdated()
        {
            // Arrange
            var updatedRecord = new WasteLossRecordBatchFinishedProducts { Id = 1, QuantityLoss = 8 };
            _mockRepo.Setup(repo => repo.UpdateWasteLossRecordAsync(updatedRecord)).ReturnsAsync(updatedRecord);

            // Act
            var result = await _controller.UpdateWasteLossRecord(1, updatedRecord);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<WasteLossRecordBatchFinishedProductsDTO>(okResult.Value);
            Assert.Equal(updatedRecord.Id, dto.Id);
        }

        [Fact]
        public async Task DeleteWasteLossRecord_ReturnsOk_WhenDeleted()
        {
            // Arrange
            var record = new WasteLossRecordBatchFinishedProducts { Id = 1, QuantityLoss = 7, Reason = "Expired" };
            _mockRepo.Setup(repo => repo.DeleteWasteLossRecordAsync(1)).ReturnsAsync(record);

            // Act
            var result = await _controller.DeleteWasteLossRecord(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<WasteLossRecordBatchFinishedProductsDTO>(okResult.Value);
            Assert.Equal(record.Id, dto.Id);
        }

        [Fact]
        public async Task DeleteWasteLossRecord_ReturnsNotFound_WhenNotExists()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteWasteLossRecordAsync(99)).ReturnsAsync((WasteLossRecordBatchFinishedProducts)null);

            // Act
            var result = await _controller.DeleteWasteLossRecord(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
