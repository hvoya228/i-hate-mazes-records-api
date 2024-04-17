using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Records.API;
using Records.Data.DataTransferObjects;

namespace IntegrationTests;

public class BestRecordsTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly WebApplicationFactory<Startup> _factory;
    private readonly HttpClient _client;

    public BestRecordsTests(WebApplicationFactory<Startup> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetBestRecordById_ExistingId_ReturnsBestRecord()
    {
        // Arrange
        var existingId = "dfc8f1bb-0abc-404d-a0b6-e1a148d9c285";

        // Act
        var response = await _client.GetAsync($"http://localhost:5139/api/BestRecord/GetById?id={existingId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var bestRecord = await response.Content.ReadFromJsonAsync<BestRecordDto>();
        bestRecord.Should().NotBeNull();
        bestRecord.Id.Should().Be(existingId);
    }

    [Fact]
    public async Task GetBestRecordById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var nonExistingId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";

        // Act
        var response = await _client.GetAsync($"http://localhost:5139/api/BestRecord/GetById?id={nonExistingId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}