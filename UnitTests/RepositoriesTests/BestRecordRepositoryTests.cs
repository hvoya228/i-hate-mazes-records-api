using Moq;
using Records.DAL.Interfaces;
using Records.Data.Models;

namespace UnitTests.RepositoriesTests;

public class BestRecordRepositoryTests
{
    [Fact]
    public void GetBestRecordById_ExistingId_ReturnsBestRecord()
    {
        // Arrange
        var mockRepo = new Mock<IBestRecordRepository>();
        var expectedBestRecord = new BestRecord
        {
            Id = Guid.NewGuid(),
            TotalScore = 0,
            PinkScore = 0,
            GreenScore = 0,
            PlayerId = Guid.NewGuid()
        };

        mockRepo.Setup(repo => repo.GetByIdAsync(expectedBestRecord.Id)).ReturnsAsync(expectedBestRecord);
        
        // Act
        var result = mockRepo.Object.GetByIdAsync(expectedBestRecord.Id).Result;
        
        // Assert
        Assert.Equal(expectedBestRecord, result);
    }
    
    [Fact]
    public void GetBestRecordById_NotExisting_ReturnsNull()
    {
        // Arrange
        var mockRepo = new Mock<IBestRecordRepository>();
        var nonExistingId = Guid.NewGuid();

        mockRepo.Setup(repo => repo.GetByIdAsync(nonExistingId))!.ReturnsAsync((BestRecord)null!);

        // Act
        var result = mockRepo.Object.GetByIdAsync(nonExistingId).Result;

        // Assert
        Assert.Null(result);
    }
}