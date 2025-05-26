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
    public class PackagingServiceTest
    {
        private readonly Mock<IPackagingRepository> _mockPackagingRepo;
        private readonly PackagingController _packagingController;

        public PackagingServiceTest()
        {
            _mockPackagingRepo = new Mock<IPackagingRepository>();
            _packagingController = new PackagingController(_mockPackagingRepo.Object);
        }

        [Fact]
        public async Task GetPackaging_ReturnsAllPackaging()
        {
            // Arrange
            var packagingList = new List<Packaging>
            {
                new Packaging { Id = 1, Name = "Box A", Type = "Cardboard", Stock = 100 },
                new Packaging { Id = 2, Name = "Bottle B", Type = "Glass", Stock = 50 }
            };

            _mockPackagingRepo.Setup(repo => repo.GetAllPackagingAsync())
                .ReturnsAsync(packagingList);

            // Act
            var result = await _packagingController.GetPackaging();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<PackagingDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetPackagingById_ReturnsPackaging_WhenExists()
        {
            // Arrange
            var packaging = new Packaging
            {
                Id = 1,
                Name = "Bottle B",
                Type = "Glass",
                Stock = 50
            };

            _mockPackagingRepo.Setup(repo => repo.GetPackagingWithDetailsAsync(1))
                .ReturnsAsync(packaging);

            // Act
            var result = await _packagingController.GetPackaging(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PackagingDTO>(okResult.Value);
            Assert.Equal(packaging.Id, returnValue.Id);
            Assert.Equal(packaging.Name, returnValue.Name);
        }

        [Fact]
        public async Task GetPackagingById_ReturnsNotFound_WhenNotExists()
        {
            // Arrange
            _mockPackagingRepo.Setup(repo => repo.GetPackagingWithDetailsAsync(99))
                .ReturnsAsync((Packaging)null);

            // Act
            var result = await _packagingController.GetPackaging(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostPackaging_ReturnsCreatedPackaging_WhenValidInput()
        {
            // Arrange
            var newPackaging = new Packaging { Id = 1, Name = "Bottle C", Type = "Plastic", Stock = 70 };

            _mockPackagingRepo.Setup(repo => repo.CreatePackagingAsync(It.IsAny<Packaging>()))
                .ReturnsAsync(newPackaging);

            // Act
            var result = await _packagingController.PostPackaging(newPackaging);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<PackagingDTO>(createdResult.Value);
            Assert.Equal(newPackaging.Id, returnValue.Id);
            Assert.Equal(newPackaging.Name, returnValue.Name);
        }

        [Fact]
        public async Task PutPackaging_ReturnsOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            var packaging = new Packaging { Id = 1, Name = "Box Updated", Type = "Cardboard", Stock = 80 };

            _mockPackagingRepo.Setup(repo => repo.UpdatePackagingAsync(packaging))
                .ReturnsAsync(packaging);

            // Act
            var result = await _packagingController.PutPackaging(1, packaging);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PackagingDTO>(okResult.Value);
            Assert.Equal(packaging.Id, returnValue.Id);
            Assert.Equal(packaging.Name, returnValue.Name);
        }

        [Fact]
        public async Task PutPackaging_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var packaging = new Packaging { Id = 5, Name = "Bottle Z", Type = "Glass", Stock = 30 };

            // Act
            var result = await _packagingController.PutPackaging(10, packaging);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutPackaging_ReturnsNotFound_WhenUpdateFails()
        {
            // Arrange
            var packaging = new Packaging { Id = 1, Name = "Missing Package", Type = "Plastic", Stock = 15 };

            _mockPackagingRepo.Setup(repo => repo.UpdatePackagingAsync(packaging))
                .ReturnsAsync((Packaging)null);

            // Act
            var result = await _packagingController.PutPackaging(1, packaging);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeletePackaging_ReturnsNoContent_WhenDeleted()
        {
            // Arrange
            var packaging = new Packaging { Id = 1, Name = "Box A" };

            _mockPackagingRepo.Setup(repo => repo.DeletePackagingAsync(1))
                .ReturnsAsync(packaging);

            // Act
            var result = await _packagingController.DeletePackaging(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeletePackaging_ReturnsNotFound_WhenPackagingNotExist()
        {
            // Arrange
            _mockPackagingRepo.Setup(repo => repo.DeletePackagingAsync(99))
                .ReturnsAsync((Packaging)null);

            // Act
            var result = await _packagingController.DeletePackaging(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
