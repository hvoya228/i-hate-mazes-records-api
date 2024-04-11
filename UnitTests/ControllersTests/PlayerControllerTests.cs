using Microsoft.AspNetCore.Mvc;
using Moq;
using Records.API.Controllers;
using Records.BLL.Interfaces;
using Records.Data.DataTransferObjects;
using Records.Data.Enums;
using Records.Data.Responses;

namespace UnitTests.ControllersTests;

public class PlayerControllerTests
{
    [Fact]
    public async Task GetById_ExistingId_ReturnsPlayer()
    {
        // Arrange
        var mockService = new Mock<IPlayerService>();
        var expectedPlayerDto = new PlayerDto
        {
            Id = Guid.NewGuid(),
            Name = "Test Player",
            Email = "test@gmail.com",
            Password = "test_password",
            BestRecordId = Guid.NewGuid()
        };

        mockService.Setup(service => service.GetById(expectedPlayerDto.Id)).ReturnsAsync(new BaseResponse<PlayerDto>
        {
            Data = expectedPlayerDto,
            Description = "Success",
            StatusCode = StatusCode.Ok
        });

        var controller = new PlayerController(mockService.Object);

        // Act
        var result = await controller.GetById(expectedPlayerDto.Id);

        // Assert
        var actionResult = Assert.IsType<ActionResult<PlayerDto>>(result);
        Assert.True(actionResult.Result is OkObjectResult);

        var okResult = actionResult.Result as OkObjectResult;
        var returnValue = Assert.IsType<PlayerDto>(okResult!.Value);
        Assert.Equal(expectedPlayerDto, returnValue);
    }

    [Fact]
    public async Task GetById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var mockService = new Mock<IPlayerService>();
        var nonExistingId = Guid.NewGuid();

        mockService.Setup(service => service.GetById(nonExistingId)).ReturnsAsync(new BaseResponse<PlayerDto>
        {
            Data = null!,
            Description = "Not Found",
            StatusCode = StatusCode.NotFound
        });

        var controller = new PlayerController(mockService.Object);

        // Act
        var result = await controller.GetById(nonExistingId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<PlayerDto>>(result);
        Assert.Null(actionResult.Value);
    }
}