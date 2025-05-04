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
    public class BatchServiceTest
    {
        private readonly Mock<IBatchService> _mockBatchService;
        private readonly BatchController _batchController;

        public BatchServiceTest()
        {
            _mockBatchService = new Mock<IBatchService>();
            _batchController = new BatchController(_mockBatchService.Object);
        }

        [Fact]
        public async Task GetBatchs_ReturnsOkResult_WithListOfBatches()
        {
            // Arrange
            var batches = new List<Batch>
            {
                new Batch
                {
                    BatchID = 1,
                    ProductionDate = DateTime.UtcNow,
                    BatchSize = 100,
                    Status = BatchStatus.Processing,
                    BatchFinishedProducts = new List<BatchFinishedProduct>
                    {
                        new BatchFinishedProduct
                        {
                            ProductID = 1,
                            Quantity = 10,
                            Unit = "kg",
                            Status = "Available",
                            WarehouseID = 5
                        }
                    }
                }
            };

            _mockBatchService.Setup(s => s.GetAllBatches()).ReturnsAsync(batches);

            // Act
            var result = await _batchController.GetBatchs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<BatchWithFinishedProductDTO>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetBatch_ReturnsNotFound_WhenBatchDoesNotExist()
        {
            // Arrange
            _mockBatchService.Setup(s => s.GetBatchById(1)).ReturnsAsync((Batch)null);

            // Act
            var result = await _batchController.GetBatch(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetBatch_ReturnsOk_WhenBatchExists()
        {
            // Arrange
            var batch = new Batch
            {
                BatchID = 1,
                ProductionDate = DateTime.UtcNow,
                BatchSize = 50,
                Status = BatchStatus.Completed,
                BatchFinishedProducts = new List<BatchFinishedProduct>()
            };

            _mockBatchService.Setup(s => s.GetBatchById(1)).ReturnsAsync(batch);

            // Act
            var result = await _batchController.GetBatch(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var batchDto = Assert.IsType<BatchWithFinishedProductDTO>(okResult.Value);
            Assert.Equal(1, batchDto.BatchID);
        }

        [Fact]
        public async Task PostBatch_ReturnsBadRequest_WhenDtoIsNull()
        {
            // Act
            var result = await _batchController.PostBatch(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Batch data is required.", badRequestResult.Value);
        }

        [Fact]
        public async Task PostBatch_ReturnsBadRequest_WhenInvalidStatus()
        {
            // Arrange
            var batchDto = new BatchDTO
            {
                ProductionDate = DateTime.UtcNow,
                BatchSize = 100,
                Status = "InvalidStatus"
            };

            // Act
            var result = await _batchController.PostBatch(batchDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid status value.", badRequestResult.Value);
        }

        [Fact]
        public async Task PostBatch_ReturnsCreatedAtAction_WhenValid()
        {
            // Arrange
            var batchDto = new BatchDTO
            {
                ProductionDate = DateTime.UtcNow,
                BatchSize = 100,
                Status = "Processing"
            };

            var newBatch = new Batch
            {
                BatchID = 10,
                ProductionDate = batchDto.ProductionDate,
                BatchSize = 100,
                Status = BatchStatus.Processing
            };

            _mockBatchService.Setup(s => s.AddBatch(It.IsAny<Batch>())).ReturnsAsync(newBatch);

            // Act
            var result = await _batchController.PostBatch(batchDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Batch>(createdResult.Value);
            Assert.Equal(10, returnValue.BatchID);
        }

        [Fact]
        public async Task PutBatch_ReturnsNotFound_WhenBatchDoesNotExist()
        {
            // Arrange
            var batchDto = new BatchDTO
            {
                ProductionDate = DateTime.UtcNow,
                BatchSize = 10,
                Status = "Processing"
            };

            _mockBatchService.Setup(s => s.GetBatchById(It.IsAny<int>())).ReturnsAsync((Batch)null);

            // Act
            var result = await _batchController.PutBatch(1, batchDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PutBatch_ReturnsBadRequest_WhenStatusIsInvalid()
        {
            // Arrange
            var batchDto = new BatchDTO
            {
                ProductionDate = DateTime.UtcNow,
                BatchSize = 10,
                Status = "InvalidStatus"
            };

            var existingBatch = new Batch
            {
                BatchID = 1,
                ProductionDate = DateTime.UtcNow,
                BatchSize = 20,
                Status = BatchStatus.Processing
            };

            _mockBatchService.Setup(s => s.GetBatchById(It.IsAny<int>())).ReturnsAsync(existingBatch);

            // Act
            var result = await _batchController.PutBatch(1, batchDto);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid status value.", badResult.Value);
        }

        [Fact]
        public async Task PutBatch_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var batchDto = new BatchDTO
            {
                ProductionDate = DateTime.UtcNow,
                BatchSize = 25,
                Status = "Processing"
            };

            var existingBatch = new Batch
            {
                BatchID = 1,
                ProductionDate = DateTime.UtcNow,
                BatchSize = 20,
                Status = BatchStatus.Processing
            };

            _mockBatchService.Setup(s => s.GetBatchById(1)).ReturnsAsync(existingBatch);
            _mockBatchService.Setup(s => s.UpdateBatch(1, It.IsAny<Batch>())).ReturnsAsync(true);

            // Act
            var result = await _batchController.PutBatch(1, batchDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBatch_ReturnsNotFound_WhenBatchNotFound()
        {
            // Arrange
            _mockBatchService.Setup(s => s.DeleteBatch(1)).ReturnsAsync(false);

            // Act
            var result = await _batchController.DeleteBatch(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteBatch_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            _mockBatchService.Setup(s => s.DeleteBatch(1)).ReturnsAsync(true);

            // Act
            var result = await _batchController.DeleteBatch(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
