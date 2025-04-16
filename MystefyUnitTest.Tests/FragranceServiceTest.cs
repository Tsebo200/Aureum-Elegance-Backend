using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Mystefy.Controllers;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;
using Mystefy.Services;
namespace MystefyUnitTest.Tests;

public class FragranceServiceTest
{
    private readonly Mock<IFragranceService> _mockFragranceService;

    private readonly FragranceController _newFragranceController;

    public FragranceServiceTest(){

        _mockFragranceService = new Mock<IFragranceService>();

        _newFragranceController = new FragranceController(_mockFragranceService.Object);
    }

    [Fact]

    public async Task AddFragranceAsync_ReturnsCreatedFragrance_WhenValidInput(){

        //Act
        //Organising DTO and fragrance values
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
        //Arrange
        //Gets result
        var result = await _newFragranceController.AddFragrance(fragranceDto);

        //Assert
        // Checks that the recieved result is of type CreatedAtActionResult
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        //Checkcs that the the returned value is of type fragrance
        var returnValue = Assert.IsType<Fragrance>(createdResult.Value);

        // Had to do each value since looking at the whole object may cause a problem when testing
        Assert.Equal(newFragrance.Id, returnValue.Id);
        Assert.Equal(newFragrance.Name, returnValue.Name);
        Assert.Equal(newFragrance.Description, returnValue.Description);
        Assert.Equal(newFragrance.Cost, returnValue.Cost);
        Assert.Equal(newFragrance.ExpiryDate, returnValue.ExpiryDate);
        Assert.Equal(newFragrance.Volume, returnValue.Volume);
        Assert.NotNull(returnValue);
    }
}
