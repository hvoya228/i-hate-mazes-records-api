using AutoMapper;
using Moq;
using Records.BLL.Services;
using Records.DAL.Interfaces;
using Records.Data.DataTransferObjects;
using Records.Data.Models;

namespace UnitTests.ServicesTests;

public class PlayerServiceTests
{
    [Fact]
    public async Task GetPlayerById_ExistingId_ReturnsPlayer()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockMapper = new Mock<IMapper>();

        var expectedPlayer = new Player
        {
            Id = Guid.NewGuid(),
            Name = "Test Player",
            Email = "test@gmail.com",
            Password = "test_password",
            BestRecordId = Guid.NewGuid()
        };

        var expectedPlayerDto = new PlayerDto
        {
            Id = expectedPlayer.Id,
            Name = expectedPlayer.Name,
            Email = expectedPlayer.Email,
            Password = expectedPlayer.Password,
            BestRecordId = expectedPlayer.BestRecordId
        };

        mockRepo.Setup(repo => repo.GetByIdAsync(expectedPlayer.Id)).ReturnsAsync(expectedPlayer);
        mockUnitOfWork.Setup(uow => uow.PlayerRepository).Returns(mockRepo.Object);
        mockMapper.Setup(mapper => mapper.Map<PlayerDto>(expectedPlayer)).Returns(expectedPlayerDto);

        var playerService = new PlayerService(mockUnitOfWork.Object, mockMapper.Object);

        // Act
        var result = await playerService.GetById(expectedPlayer.Id);

        // Assert
        Assert.Equal(expectedPlayerDto, result.Data);
    }
    
    [Fact]
    public async Task GetPlayerById_NonExistingId_ReturnsPlayer()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockMapper = new Mock<IMapper>();
        var nonExistingId = Guid.NewGuid();

        mockRepo.Setup(repo => repo.GetByIdAsync(nonExistingId))!.ReturnsAsync((Player)null!);
        mockUnitOfWork.Setup(uow => uow.PlayerRepository).Returns(mockRepo.Object);

        var playerService = new PlayerService(mockUnitOfWork.Object, mockMapper.Object);

        // Act
        var result = await playerService.GetById(nonExistingId);

        // Assert
        Assert.Null(result.Data);
    }
}