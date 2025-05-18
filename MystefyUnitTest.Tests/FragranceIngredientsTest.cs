using Microsoft.AspNetCore.Mvc;
using Moq;
using Mystefy.Controllers;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;
using Xunit;

namespace MystefyUnitTest.Tests
{
    public class FragranceIngredientsTest
    {
        private readonly Mock<IFragranceIngredientService> _mockFragranceIngredientsService;
        private readonly FragranceIngredientsController _fragranceIngredientsController;

        public FragranceIngredientsTest()
        {
            _mockFragranceIngredientsService = new Mock<IFragranceIngredientService>();
            _fragranceIngredientsController = new FragranceIngredientsController(_mockFragranceIngredientsService.Object);
        }

        [Fact]
        public async Task GetFragranceIngredients_ReturnsOkResult_WithList()
        {
            // Arrange
            var mockData = new List<FragranceIngredient>
            {
                new FragranceIngredient
                {
                    FragranceID = 1,
                    IngredientsID = 2,
                    Amount = 10,
                    Fragrance = new Fragrance
                    {
                        Name = "Fresh",
                        Description = "Fresh scent",
                        Volume = 100,
                        Cost = 200,
                        ExpiryDate = DateTime.Now.AddYears(1)
                    },
                    Ingredients = new Ingredients
                    {
                        Id = 2,
                        Name = "Lemon",
                        Type = "Citrus",
                        Cost = "50",
                        ExpiryDate = DateTime.Now.AddYears(1),
                        IsExpired = false
                    }
                }
            };
            _mockFragranceIngredientsService.Setup(service => service.GetAllFragranceIngredients()).ReturnsAsync(mockData);

            // Act
            var result = await _fragranceIngredientsController.GetFragranceIngredients();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<FragranceIngredientsDTO>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetFragranceIngredient_ReturnsNotFound_IfNotExists()
        {
            // Arrange
            _mockFragranceIngredientsService.Setup(s => s.GetFragranceIgredientsById(1, 2)).ReturnsAsync((FragranceIngredient?)null);

            // Act
            var result = await _fragranceIngredientsController.GetFragranceIngredient(1, 2);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostFragranceIngredient_ReturnsCreatedAtAction()
        {
            // Arrange
            var dto = new PostFragranceIngredientsDTO { FragranceID = 1, IngredientsID = 2, Amount = 30 };
            var mockEntity = new FragranceIngredient { FragranceID = 1, IngredientsID = 2, Amount = 30 };

            _mockFragranceIngredientsService
                .Setup(s => s.AddFragranceIngredient(It.IsAny<FragranceIngredient>()))
                .ReturnsAsync(mockEntity);

            // Act
            var result = await _fragranceIngredientsController.PostFragranceIngredient(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<FragranceIngredient>(createdResult.Value);
            Assert.Equal(1, returnValue.FragranceID);
            Assert.Equal(2, returnValue.IngredientsID);
        }

        [Fact]
        public async Task PutFragranceIngredient_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var dto = new PostFragranceIngredientsDTO { FragranceID = 1, IngredientsID = 2, Amount = 45 };
            _mockFragranceIngredientsService
                .Setup(s => s.UpdateFragranceIngredient(1, 2, It.IsAny<FragranceIngredient>()))
                .ReturnsAsync(true);

            // Act
            var result = await _fragranceIngredientsController.PutFragranceIngredient(1, 2, dto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteFragranceIngredient_ReturnsNoContent_WhenDeleted()
        {
            // Arrange
            _mockFragranceIngredientsService
                .Setup(s => s.DeleteFragranceIngredient(1, 2))
                .ReturnsAsync(true);

            // Act
            var result = await _fragranceIngredientsController.DeleteFragranceIngredient(1, 2);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
