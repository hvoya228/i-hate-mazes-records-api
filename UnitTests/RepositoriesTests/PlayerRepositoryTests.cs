using Moq;
using Records.DAL.Interfaces;
using Records.Data.Models;

namespace UnitTests.RepositoriesTests;

public class PlayerRepositoryTests
{
    [Fact]
    public void GetPlayerById_ExistingId_ReturnsPlayer()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerRepository>();
        var expectedPlayer = new Player
        {
            Id = Guid.NewGuid(), 
            Name = "Test Player",
            Email = "test@gmail.com",
            Password = "test_password",
            BestRecordId = Guid.NewGuid()
        };
        
        mockRepo.Setup(repo => repo.GetByIdAsync(expectedPlayer.Id)).ReturnsAsync(expectedPlayer);
        
        // Act
        var result = mockRepo.Object.GetByIdAsync(expectedPlayer.Id).Result;
        
        // Assert
        Assert.Equal(expectedPlayer, result);
    }

    [Fact]
    public void GetPlayerById_NotExisting_ReturnsNull()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerRepository>();
        var nonExistingId = Guid.NewGuid();

        mockRepo.Setup(repo => repo.GetByIdAsync(nonExistingId))!.ReturnsAsync((Player)null!);

        // Act
        var result = mockRepo.Object.GetByIdAsync(nonExistingId).Result;

        // Assert
        Assert.Null(result);
    }
}