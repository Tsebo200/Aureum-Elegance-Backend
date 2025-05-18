using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Mystefy.Controllers;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;
using Xunit;
using static Mystefy.DTOs.FinishedProductPackagingDTO;

namespace MystefyUnitTest.Tests
{
    public class FinishedProductPackagingServiceTest
    {
        private readonly Mock<IFinishedProductPackaging> _mockFinishedProductPackagingService;
        private readonly FinishedProductPackagingController _finishedProductPackagingController;

        public FinishedProductPackagingServiceTest()
        {
            _mockFinishedProductPackagingService = new Mock<IFinishedProductPackaging>();
            _finishedProductPackagingController = new FinishedProductPackagingController(_mockFinishedProductPackagingService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithList()
        {
            // Arrange
            var mockData = new List<FinishedProductPackaging>
            {
                new FinishedProductPackaging
                {
                    ProductID = 1,
                    PackagingID = 2,
                    Amount = 100,
                    FinishedProduct = new FinishedProduct
                    {
                        ProductID = 1,
                        ProductName = "Perfume A",
                        FragranceID = 3,
                        Quantity = 50
                    },
                    Packaging = new Packaging
                    {
                        Id = 2,
                        Name = "Bottle",
                        Type = "Glass",
                        Stock = 100
                    }
                }
            };

            _mockFinishedProductPackagingService
                .Setup(s => s.GetAllFinishedProductPackaging())
                .ReturnsAsync(mockData);

            // Act
            var result = await _finishedProductPackagingController.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
          var returnValue = Assert.IsAssignableFrom<IEnumerable<FinishedProductPackageDTO>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNotExists()
        {
            _mockFinishedProductPackagingService
                .Setup(s => s.GetFinishedProductPackagingById(1, 2))
                .ReturnsAsync((FinishedProductPackaging?)null);

            var result = await _finishedProductPackagingController.GetById(1, 2);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Post_ReturnsCreatedAtAction()
        {
            // Arrange
            var dto = new PostFinishedProductPackagingDTO
            {
                ProductID = 1,
                PackagingId = 2,
                Amount = 100
            };

            var mockEntity = new FinishedProductPackaging
            {
                ProductID = 1,
                PackagingID = 2,
                Amount = 100
            };

            _mockFinishedProductPackagingService
                .Setup(s => s.AddFinishedProductPackaging(It.IsAny<FinishedProductPackaging>()))
                .ReturnsAsync(mockEntity);

            // Act
            var result = await _finishedProductPackagingController.Post(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<FinishedProductPackaging>(createdResult.Value);
            Assert.Equal(1, returnValue.ProductID);
            Assert.Equal(2, returnValue.PackagingID);
        }

        [Fact]
        public async Task Put_ReturnsNoContent_WhenUpdated()
        {
            var dto = new PostFinishedProductPackagingDTO
            {
                ProductID = 1,
                PackagingId = 2,
                Amount = 150
            };

            _mockFinishedProductPackagingService
                .Setup(s => s.UpdateFinishedProductPackaging(1, 2, It.IsAny<FinishedProductPackaging>()))
                .ReturnsAsync(true);

            var result = await _finishedProductPackagingController.Put(1, 2, dto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleted()
        {
            _mockFinishedProductPackagingService
                .Setup(s => s.DeleteFinishedProductPackaging(1, 2))
                .ReturnsAsync(true);

            var result = await _finishedProductPackagingController.Delete(1, 2);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
