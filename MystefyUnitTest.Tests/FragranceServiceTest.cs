using Microsoft.AspNetCore.Mvc;
using Moq;
using Mystefy.Controllers;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MystefyUnitTest.Tests
{
    public class FragranceServiceTest
    {
        private readonly Mock<IFragranceService> _mockFragranceService;
        private readonly FragranceController _fragranceController;

        public FragranceServiceTest()
        {
            _mockFragranceService = new Mock<IFragranceService>();
            _fragranceController = new FragranceController(_mockFragranceService.Object);
        }

        [Fact]
        public async Task AddFragranceAsync_ReturnsCreatedFragrance_WhenValidInput()
        {
            var fragranceDto = new PostFragranceDTO
            {
                Name = "Mystic Musk",
                Description = "A rich, musky scent.",
                Cost = 49.99m,
                ExpiryDate = DateTime.UtcNow.AddYears(1),
                Volume = 100
            };

            var newFragrance = new Fragrance
            {
                Id = 1,
                Name = fragranceDto.Name,
                Description = fragranceDto.Description,
                Cost = fragranceDto.Cost,
                ExpiryDate = fragranceDto.ExpiryDate,
                Volume = fragranceDto.Volume
            };

            _mockFragranceService.Setup(x => x.AddFragrance(It.IsAny<Fragrance>())).ReturnsAsync(newFragrance);

            var result = await _fragranceController.AddFragrance(fragranceDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Fragrance>(createdResult.Value);

            Assert.Equal(newFragrance.Id, returnValue.Id);
            Assert.Equal(newFragrance.Name, returnValue.Name);
        }

        [Fact]
        public async Task GetFragrances_ReturnsOkResult_WithListOfFragrances()
        {
            var fragrances = new List<Fragrance>
            {
                new Fragrance { Id = 1, Name = "Fresh Breeze", Description = "Fresh scent", Cost = 29.99m, ExpiryDate = DateTime.UtcNow.AddYears(1), Volume = 100 }
            };

            _mockFragranceService.Setup(x => x.GetAllFragrances()).ReturnsAsync(fragrances);

            var result = await _fragranceController.GetFragrances();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<FragranceDTO>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("Fresh Breeze", returnValue[0].Name);
        }

        [Fact]
        public async Task GetFragrance_ReturnsFragrance_WhenFound()
        {
            var fragrance = new Fragrance
            {
                Id = 1,
                Name = "Ocean Mist",
                Description = "Cool and salty",
                Cost = 39.99m,
                ExpiryDate = DateTime.UtcNow.AddYears(1),
                Volume = 200
            };

            _mockFragranceService.Setup(x => x.GetFragranceById(1)).ReturnsAsync(fragrance);

            var result = await _fragranceController.GetFragrance(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<FragranceDTO>(okResult.Value);
            Assert.Equal("Ocean Mist", dto.Name);
        }

        [Fact]
        public async Task GetFragrance_ReturnsNotFound_WhenFragranceMissing()
        {
            _mockFragranceService.Setup(x => x.GetFragranceById(99)).ReturnsAsync((Fragrance)null!);

            var result = await _fragranceController.GetFragrance(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetFragranceByName_ReturnsOk_WhenFound()
        {
            var fragrance = new Fragrance
            {
                Id = 1,
                Name = "Citrus Twist",
                Description = "Zesty and fresh",
                Cost = 45.00m,
                ExpiryDate = DateTime.UtcNow.AddYears(2),
                Volume = 150
            };

            _mockFragranceService.Setup(x => x.GetFragranceByName("Citrus Twist")).ReturnsAsync(fragrance);

            var result = await _fragranceController.GetFragranceByName("Citrus Twist");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<FragranceDTO>(okResult.Value);
            Assert.Equal("Citrus Twist", dto.Name);
        }

        [Fact]
        public async Task GetFragranceByName_ReturnsNotFound_WhenMissing()
        {
            _mockFragranceService.Setup(x => x.GetFragranceByName("Nonexistent")).ReturnsAsync((Fragrance)null!);

            var result = await _fragranceController.GetFragranceByName("Nonexistent");

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutFragrance_ReturnsNoContent_WhenUpdateSucceeds()
        {
            var dto = new PostFragranceDTO
            {
                Name = "New Name",
                Description = "Updated Desc",
                Cost = 59.99m,
                ExpiryDate = DateTime.UtcNow.AddYears(2),
                Volume = 250
            };

            _mockFragranceService.Setup(x => x.UpdateFragrance(1, It.IsAny<Fragrance>())).ReturnsAsync(true);

            var result = await _fragranceController.PutFragrance(1, dto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutFragrance_ReturnsNotFound_WhenUpdateFails()
        {
            var dto = new PostFragranceDTO
            {
                Name = "Failing Name",
                Description = "No Update",
                Cost = 0,
                ExpiryDate = DateTime.UtcNow,
                Volume = 0
            };

            _mockFragranceService.Setup(x => x.UpdateFragrance(99, It.IsAny<Fragrance>())).ReturnsAsync(false);

            var result = await _fragranceController.PutFragrance(99, dto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteFragrance_ReturnsNoContent_WhenSuccessful()
        {
            _mockFragranceService.Setup(x => x.DeleteFragrance(1)).ReturnsAsync(true);

            var result = await _fragranceController.DeleteFragrance(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteFragrance_ReturnsNotFound_WhenFragranceMissing()
        {
            _mockFragranceService.Setup(x => x.DeleteFragrance(999)).ReturnsAsync(false);

            var result = await _fragranceController.DeleteFragrance(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
