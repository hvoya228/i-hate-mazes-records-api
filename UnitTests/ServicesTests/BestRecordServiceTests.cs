using AutoMapper;
using Moq;
using Records.BLL.Services;
using Records.DAL.Interfaces;
using Records.Data.DataTransferObjects;
using Records.Data.Models;

namespace UnitTests.ServicesTests;

public class BestRecordServiceTests
{
    [Fact]
    public async Task GetById_ExistingId_ReturnsBestRecord()
    {
        // Arrange
        var mockRepo = new Mock<IBestRecordRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockMapper = new Mock<IMapper>();

        var expectedBestRecord = new BestRecord
        {
            Id = Guid.NewGuid(),
            TotalScore = 0,
            PinkScore = 0,
            GreenScore = 0,
            PlayerId = Guid.NewGuid()
        };

        var expectedBestRecordDto = new BestRecordDto
        {
            Id = expectedBestRecord.Id,
            TotalScore = expectedBestRecord.TotalScore,
            PinkScore = expectedBestRecord.PinkScore,
            GreenScore = expectedBestRecord.GreenScore,
            PlayerId = expectedBestRecord.PlayerId
        };

        mockRepo.Setup(repo => repo.GetByIdAsync(expectedBestRecord.Id)).ReturnsAsync(expectedBestRecord);
        mockUnitOfWork.Setup(uow => uow.BestRecordRepository).Returns(mockRepo.Object);
        mockMapper.Setup(mapper => mapper.Map<BestRecordDto>(expectedBestRecord)).Returns(expectedBestRecordDto);

        var bestRecordService = new BestRecordService(mockUnitOfWork.Object, mockMapper.Object);

        // Act
        var result = await bestRecordService.GetById(expectedBestRecord.Id);

        // Assert
        Assert.Equal(expectedBestRecordDto, result.Data);
    }
    
    [Fact]
    public async Task GetById_NonExistingId_ReturnsBestRecord()
    {
        // Arrange
        var mockRepo = new Mock<IBestRecordRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockMapper = new Mock<IMapper>();
        var nonExistingId = Guid.NewGuid();

        mockRepo.Setup(repo => repo.GetByIdAsync(nonExistingId))!.ReturnsAsync((BestRecord)null!);
        mockUnitOfWork.Setup(uow => uow.BestRecordRepository).Returns(mockRepo.Object);

        var bestRecordService = new BestRecordService(mockUnitOfWork.Object, mockMapper.Object);

        // Act
        var result = await bestRecordService.GetById(nonExistingId);

        // Assert
        Assert.Null(result.Data);
    }
}