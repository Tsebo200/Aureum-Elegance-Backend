using Microsoft.AspNetCore.Mvc;
using Moq;
using Mystefy.Controllers;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MystefyUnitTest.Tests
{
    public class FinishedProductServiceTest
    {
        private readonly Mock<IFinishedProductService> _mockFinishedProductService;
        private readonly FinishedProductController _finishedProductController;

        public FinishedProductServiceTest()
        {
            _mockFinishedProductService = new Mock<IFinishedProductService>();
            _finishedProductController = new FinishedProductController(_mockFinishedProductService.Object);
        }

        [Fact]
        public async Task GetProducts_ReturnsListOfFinishedProductDTOs()
        {
            var products = new List<FinishedProduct>
            {
                new FinishedProduct
                {
                    ProductID = 1,
                    FragranceID = 1,
                    ProductName = "Ocean Breeze",
                    Quantity = 100,
                    FinishedProductPackaging = new List<FinishedProductPackaging>()
                }
            };

            _mockFinishedProductService.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(products);

            var result = await _finishedProductController.GetProducts();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<FinishedProductDTO>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("Ocean Breeze", returnValue[0].ProductName);
        }

        [Fact]
        public async Task GetProduct_ReturnsProduct_WhenFound()
        {
            var product = new FinishedProduct
            {
                ProductID = 1,
                FragranceID = 1,
                ProductName = "Lavender Bliss",
                Quantity = 50,
                FinishedProductPackaging = new List<FinishedProductPackaging>()
            };

            _mockFinishedProductService.Setup(s => s.GetProductByIdAsync(1)).ReturnsAsync(product);

            var result = await _finishedProductController.GetProduct(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<FinishedProductDTO>(okResult.Value);
            Assert.Equal("Lavender Bliss", returnValue.ProductName);
        }

        [Fact]
        public async Task GetProductByName_ReturnsProduct_WhenFound()
        {
            var product = new FinishedProduct
            {
                ProductID = 2,
                FragranceID = 2,
                ProductName = "Vanilla Dream",
                Quantity = 200,
                FinishedProductPackaging = new List<FinishedProductPackaging>()
            };

            _mockFinishedProductService.Setup(s => s.GetFinishedProductByName("Vanilla Dream")).ReturnsAsync(product);

            var result = await _finishedProductController.GetProductByName("Vanilla Dream");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<FinishedProductDTO>(okResult.Value);
            Assert.Equal("Vanilla Dream", returnValue.ProductName);
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedAtActionResult_WhenValidInput()
        {
            var dto = new PostFinishedProductDTO
            {
                ProductID = 3,
                FragranceID = 3,
                ProductName = "Citrus Burst",
                Quantity = 75
            };

            var createdProduct = new FinishedProduct
            {
                ProductID = 3,
                FragranceID = 3,
                ProductName = "Citrus Burst",
                Quantity = 75
            };

            _mockFinishedProductService.Setup(s => s.CreateProductAsync(It.IsAny<FinishedProduct>())).ReturnsAsync(createdProduct);

            var result = await _finishedProductController.CreateProduct(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<FinishedProduct>(createdResult.Value);
            Assert.Equal("Citrus Burst", returnValue.ProductName);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNoContent_WhenSuccessful()
        {
            var dto = new PostFinishedProductDTO
            {
                ProductID = 4,
                FragranceID = 4,
                ProductName = "Mint Rush",
                Quantity = 60
            };

            _mockFinishedProductService.Setup(s => s.UpdateProductAsync(It.IsAny<FinishedProduct>())).ReturnsAsync(true);

            var result = await _finishedProductController.UpdateProduct(4, dto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent_WhenSuccessful()
        {
            _mockFinishedProductService.Setup(s => s.DeleteProductAsync(5)).ReturnsAsync(true);

            var result = await _finishedProductController.DeleteProduct(5);

            Assert.IsType<NoContentResult>(result);
        }
    }
}