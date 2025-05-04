using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Mystefy.Controllers;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;
using Xunit;
namespace MystefyUnitTest.Tests;

public class WarehouseServiceTest
{
    private readonly Mock<IWarehouse> _mockWarehouseService;
        private readonly WarehouseController _warehouseController;

        public WarehouseServiceTest()
        {
            _mockWarehouseService = new Mock<IWarehouse>();
            _warehouseController = new WarehouseController( _mockWarehouseService.Object);
        }
    [Fact]
        public async Task GetWarehouses_ReturnsOkResult_WithListOfWarehouses()
        {
            var warehouses = new List<Warehouse>
            {
                new Warehouse { WarehouseID = 1, Name = "Main", Location = "Cape Town" }
            };

            _mockWarehouseService.Setup(s => s.GetAllWarehouses()).ReturnsAsync(warehouses);

            var result = await _warehouseController.GetWarehouses();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<WarehouseDTO>>(okResult.Value);

            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetWarehouse_ReturnsWarehouse_WhenFound()
        {
            var warehouse = new Warehouse { WarehouseID = 1, Name = "Main", Location = "Cape Town" };
            _mockWarehouseService.Setup(s => s.GetWarehouseById(1)).ReturnsAsync(warehouse);

            var result = await _warehouseController.GetWarehouse(1);
            var returnValue = Assert.IsType<Warehouse>(result.Value);

            Assert.Equal(1, returnValue.WarehouseID);
        }

        [Fact]
        public async Task GetWarehouse_ReturnsNotFound_WhenNotFound()
        {
            _mockWarehouseService.Setup(s => s.GetWarehouseById(1)).ReturnsAsync((Warehouse)null);

            var result = await _warehouseController.GetWarehouse(1);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostWarehouse_ReturnsCreatedWarehouse()
        {
            var dto = new WarehouseDTO { Name = "New", location = "Joburg" };
            var created = new Warehouse { WarehouseID = 2, Name = dto.Name, Location = dto.location };

            _mockWarehouseService.Setup(s => s.MakeWarehouse(It.IsAny<Warehouse>())).ReturnsAsync(created);

            var result = await _warehouseController.PostWarehouse(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Warehouse>(createdResult.Value);

            Assert.Equal("New", returnValue.Name);
        }

        [Fact]
        public async Task PutWarehouse_ReturnsNoContent_WhenSuccessful()
        {
            var warehouse = new Warehouse { WarehouseID = 1, Name = "Updated", Location = "Durban" };

            _mockWarehouseService.Setup(s => s.UpdateWarehouse(1, warehouse)).ReturnsAsync(true);

            var result = await _warehouseController.PutWarehouse(1, warehouse);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutWarehouse_ReturnsNotFound_WhenNotUpdated()
        {
            var warehouse = new Warehouse { WarehouseID = 1, Name = "Updated", Location = "Durban" };

            _mockWarehouseService.Setup(s => s.UpdateWarehouse(1, warehouse)).ReturnsAsync(false);

            var result = await _warehouseController.PutWarehouse(1, warehouse);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteWarehouse_ReturnsNoContent_WhenDeleted()
        {
            _mockWarehouseService.Setup(s => s.DeleteWarehouse(1)).ReturnsAsync(true);

            var result = await _warehouseController.DeleteWarehouse(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteWarehouse_ReturnsNotFound_WhenNotDeleted()
        {
            _mockWarehouseService.Setup(s => s.DeleteWarehouse(1)).ReturnsAsync(false);

            var result = await _warehouseController.DeleteWarehouse(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetWasteLossIngredientsByWarehouseId_ReturnsOkResult_WhenFound()
        {
            var warehouse = new Warehouse
            {
                WarehouseID = 1,
                Name = "Main",
                Location = "Cape Town",
                WasteLossRecordIngredients = new List<WasteLossRecordIngredients>
                {
                    new WasteLossRecordIngredients
                    {
                        Id = 1,
                        QuantityLoss = 10,
                        Reason = "Expired",
                        Ingredients = new Ingredients { Id = 1, Name = "Alcohol", Type = "Liquid", Cost = "50", IsExpired = true },
                        User = new User { UserId = 1, Name = "Michael", Role = UserRole.Manager, Email = "mikejames@gmail.com", Password = "MichaelPatch3432!122"}
                    }
                }
            };

            _mockWarehouseService.Setup(s => s.GetWasteLossIngredientsByWarehouseId(1)).ReturnsAsync(warehouse);

            var result = await _warehouseController.GetWasteLossIngredientsByWarehouseId(1);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var dto = Assert.IsType<WarehouseWasteLossRecordsForIngredientsDTO>(okResult.Value);
            Assert.Equal("Main", dto.Name);
            Assert.Single(dto.WasteLossRecordIngredients);
        }

        [Fact]
        public async Task GetWasteLossIngredientsByWarehouseId_ReturnsNotFound_WhenWarehouseNull()
        {
            _mockWarehouseService.Setup(s => s.GetWasteLossIngredientsByWarehouseId(1)).ReturnsAsync((Warehouse)null);

            var result = await _warehouseController.GetWasteLossIngredientsByWarehouseId(1);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
public async Task GetWasteLossPackagingByWarehouseId_ReturnsOkResult_WhenFound()
{
    var warehouse = new Warehouse
    {
        WarehouseID = 2,
        Name = "Packaging Warehouse",
        Location = "Pretoria",
        WasteLossRecordPackaging = new List<WasteLossRecordPackaging>
        {
            new WasteLossRecordPackaging
            {
                Id = 1,
                QuantityLoss = 5,
                Reason = "Damaged",
                Packaging = new Packaging { Id = 1, Name = "Bottle", Type = "Glass", Stock = 20 },
                User = new User { UserId = 2, Name = "Joseph", Role = UserRole.Manager, Email = "JosephStar@gmail.com", Password = "JoseStar@1211"}
            }
        }
    };

    _mockWarehouseService.Setup(s => s.GetWasteLossPackagingByWarehouseId(2)).ReturnsAsync(warehouse);

    var result = await _warehouseController.GetWasteLossPackagingByWarehouseId(2);
    var okResult = Assert.IsType<OkObjectResult>(result.Result);

    var dto = Assert.IsType<WarehouseWasteLossRecordsForPackagingDTO>(okResult.Value);
    Assert.Equal("Packaging Warehouse", dto.Name);
    Assert.Single(dto.WasteLossRecordPackaging);
}

[Fact]
public async Task GetWasteLossPackagingByWarehouseId_ReturnsNotFound_WhenWarehouseNull()
{
    _mockWarehouseService.Setup(s => s.GetWasteLossPackagingByWarehouseId(2)).ReturnsAsync((Warehouse)null);

    var result = await _warehouseController.GetWasteLossPackagingByWarehouseId(2);
    Assert.IsType<NotFoundResult>(result.Result);
}

[Fact]
public async Task GetWasteLossFragranceByWarehouseId_ReturnsOkResult_WhenFound()
{
    var warehouse = new Warehouse
    {
        WarehouseID = 3,
        Name = "Fragrance Warehouse",
        Location = "Durban",
        WasteLossRecordFragrance = new List<WasteLossRecordFragrance>
        {
            new WasteLossRecordFragrance
            {
                Id = 1,
                QuantityLoss = 8,
                Reason = "Spillage",
                Fragrance = new Fragrance { Name = "Ocean Breeze",
                Description = "A fresh and aquatic scent with marine notes.",
                Cost = 120.50m,
                ExpiryDate = DateTime.UtcNow.AddYears(2),
                Volume = 50.0m,},
                User = new User { UserId = 3, Name = "Joshua", Role = UserRole.Admin, Email = "joshuadean@gmail.com", Password = "JoshDuck@23213" }
            }
        }
    };

    _mockWarehouseService.Setup(s => s.GetWasteLossFragranceByWarehouseId(3)).ReturnsAsync(warehouse);

    var result = await _warehouseController.GetWasteLossFragranceByWarehouseId(3);
    var okResult = Assert.IsType<OkObjectResult>(result.Result);

    var dto = Assert.IsType<WarehouseWasteLossRecordsForFragrancesDTO>(okResult.Value);
    Assert.Equal("Fragrance Warehouse", dto.Name);
    Assert.Single(dto.WasteLossRecordFragrance);
}

[Fact]
public async Task GetWasteLossFragranceByWarehouseId_ReturnsNotFound_WhenWarehouseNull()
{
    _mockWarehouseService.Setup(s => s.GetWasteLossFragranceByWarehouseId(3)).ReturnsAsync((Warehouse)null);

    var result = await _warehouseController.GetWasteLossFragranceByWarehouseId(3);
    Assert.IsType<NotFoundResult>(result.Result);
}

[Fact]
public async Task GetWasteLossFinishedProductByWarehouseId_ReturnsOkResult_WhenFound()
{
    // Arrange
    var warehouse = new Warehouse
    {
        WarehouseID = 4,
        Name = "Finished Goods Warehouse",
        Location = "Johannesburg",
        WasteLossRecordBatchFinishedProducts = new List<WasteLossRecordBatchFinishedProducts>
        {
            new WasteLossRecordBatchFinishedProducts
            {
                Id = 1,
                QuantityLoss = 12,
                Reason = "Recall",
                DateOfLoss = DateTime.UtcNow,
                FinishedProduct = new FinishedProduct
                {
                    ProductID = 1,
                    FragranceID = 2,
                    PackagingID = 3,
                    Quantity = 100
                },
                User = new User
                {
                    UserId = 4,
                    Name = "Timothy",
                    Role = UserRole.Manager,
                    Email = "TimothyGuv@gmail.com",
                    Password = "RachetDuck&1123"
                },
                Batch = new Batch
                {
                    BatchID = 5,
                    ProductionDate = DateTime.UtcNow.AddDays(-10),
                    BatchSize = 200
                }
            }
        }
    };

    _mockWarehouseService
        .Setup(s => s.GetWasteLossBatchFinishedProductsByWarehouseId(4))
        .ReturnsAsync(warehouse);

    // Act
    var result = await _warehouseController.GetWasteLossFinishedProductByWarehouseId(4);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result.Result);
    var dto = Assert.IsType<WarehouseWasteLossRecordsForBatchFinishedProductsDTO>(okResult.Value);

    Assert.Equal("Finished Goods Warehouse", dto.Name);
    Assert.Equal("Johannesburg", dto.location);
    Assert.Single(dto.WasteLossRecordBatchFinishedProduct);
    
    var record = dto.WasteLossRecordBatchFinishedProduct.First();
    Assert.Equal(12, record.QuantityLoss);
    Assert.Equal("Recall", record.Reason);
    Assert.NotNull(record.FinishedProduct);
    Assert.Equal(100, record.FinishedProduct.Quantity);
    Assert.NotNull(record.User);
    Assert.Equal("Timothy", record.User.Name);
    Assert.NotNull(record.Batch);
    Assert.Equal(200, record.Batch.BatchSize);
}

[Fact]
public async Task GetWasteLossFinishedProductByWarehouseId_ReturnsNotFound_WhenWarehouseNull()
{
    _mockWarehouseService
        .Setup(s => s.GetWasteLossBatchFinishedProductsByWarehouseId(4))
        .ReturnsAsync((Warehouse)null);

    var result = await _warehouseController.GetWasteLossFinishedProductByWarehouseId(4);
    
    Assert.IsType<NotFoundResult>(result.Result);
}






}
