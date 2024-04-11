using Microsoft.AspNetCore.Mvc;
using Moq;
using Records.API.Controllers;
using Records.BLL.Interfaces;
using Records.Data.DataTransferObjects;
using Records.Data.Enums;
using Records.Data.Responses;

namespace UnitTests.ControllersTests;

public class BestRecordControllersTest
{
    [Fact]
    public async Task GetById_ExistingId_ReturnsBestRecord()
    {
        // Arrange
        var mockService = new Mock<IBestRecordService>();
        var expectedBestRecordDto = new BestRecordDto
        {
            Id = Guid.NewGuid(),
            PlayerId = Guid.NewGuid(),
            TotalScore = 0,
            PinkScore = 0,
            GreenScore = 0
        };

        mockService.Setup(service => service.GetById(expectedBestRecordDto.Id)).ReturnsAsync(new BaseResponse<BestRecordDto>
        {
            Data = expectedBestRecordDto,
            Description = "Success",
            StatusCode = StatusCode.Ok
        });

        var controller = new BestRecordController(mockService.Object);

        // Act
        var result = await controller.GetById(expectedBestRecordDto.Id);

        // Assert
        var actionResult = Assert.IsType<ActionResult<BestRecordDto>>(result);
        Assert.True(actionResult.Result is OkObjectResult);

        var okResult = actionResult.Result as OkObjectResult;
        var returnValue = Assert.IsType<BestRecordDto>(okResult!.Value);
        Assert.Equal(expectedBestRecordDto, returnValue);
    }

    [Fact]
    public async Task GetById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var mockService = new Mock<IBestRecordService>();
        var nonExistingId = Guid.NewGuid();

        mockService.Setup(service => service.GetById(nonExistingId)).ReturnsAsync(new BaseResponse<BestRecordDto>
        {
            Data = null!,
            Description = "Not Found",
            StatusCode = StatusCode.NotFound
        });

        var controller = new BestRecordController(mockService.Object);

        // Act
        var result = await controller.GetById(nonExistingId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<BestRecordDto>>(result);
        Assert.Null(actionResult.Value);
    }
}