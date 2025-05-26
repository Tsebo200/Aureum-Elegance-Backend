using System;
using Xunit;
using Moq;
using Mystefy.Services;
using Mystefy.Interfaces;
using Mystefy.Data;
using Mystefy.Models;
using Mystefy.Controllers;

namespace MystefyUnitTest.Tests;

public class PackagingRepositoryServiceTest
{
// One issue is that we are testing our function and not the actual repository
// MOCK FUNCTION TESTING
// TODO: Mock Actual Database
private readonly Mock<IPackagingRepository> _mockRepo;
private readonly Mock<PackagingController> _mockPackagingController;
private readonly PackagingRepositoryService _service;

public PackagingRepositoryServiceTest()
{
    _mockPackagingController = new Mock<PackagingController>();
    // Getting an error:  Argument 1: cannot convert from 'Mystefy.Controllers.PackagingController' to 'Mystefy.Data.MystefyDbContext'
    // _service = new PackagingRepositoryService(_mockPackagingController.Object);
}






// Getting Packages
//TODO: GET All Packaging  
//TODO: Test Input incorrect - item was not found (return null)

}
