using Restate.WebApi.Application.Abstractions.Persistence;
using Restate.WebApi.Application.Offers.CreateOffer;
using Restate.WebApi.Application.Offers.GetOffer;
using Restate.WebApi.Domain.Enums;
using Restate.WebApi.IntegrationTests.Helpers;
using System.Net;
using System.Net.Http.Json;

namespace Restate.WebApi.IntegrationTests;

public class OffersControllerTests : IClassFixture<RestateWebApiFactory>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly IApplicationDbContext _dbContext;
    private readonly DatabaseRespawner _respawner;

    public OffersControllerTests(RestateWebApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
        _dbContext = apiFactory.DatabaseContext;
        _respawner = apiFactory.DatabaseRespawner;
    }

    public async Task InitializeAsync()
    {
        await _respawner.ResetAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async void CreateOfferAsync_ShouldCreateOfferWithCreatedStatusCode_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateOfferCommand
        {
            Title = "Mikrokawalerka w centrum",
            EstateKind = EstateKind.Flat,
            Description = "Mikrokawalerka w centrum Katowic.",
            Area = 24m,
            Price = 1_300_000m,
        };

        // Act
        var response = await _client.PostAsJsonAsync("Offers", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async void CreateOfferAsync_ShouldCreateOfferWithSameIdInLocationHeaderAndInRequestBody_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateOfferCommand
        {
            Title = "Mikrokawalerka w centrum",
            EstateKind = EstateKind.Flat,
            Description = "Mikrokawalerka w centrum Katowic.",
            Area = 24m,
            Price = 1_300_000m,
        };

        // Act
        var response = await _client.PostAsJsonAsync("Offers", request);

        // Assert
        var locationHeader = response.Headers.Location!.ToString();
        var createdOffer = await response.Content.ReadFromJsonAsync<GetOfferResult>(TestDefaults.JsonOptions);
        var expectedLocationUrl = $"http://localhost/Offers/{createdOffer!.Id}";
        Assert.Equal(expectedLocationUrl, locationHeader);
    }
}
