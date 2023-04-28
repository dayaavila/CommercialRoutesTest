using Autofac;
using CommercialRoutes.Infrastructure.Models;
using CommercialRoutes.Infrastructure.Options;
using CommercialRoutes.Infrastructure.Services;
using Flurl.Http;
using Flurl.Http.Testing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CommercialRoutes.Infrastructure.Test.Services;

public class DistanceApiServiceTests
{
    [Fact]
    public async Task GivenRoute_WhenGetDistances_ThenReturnsExpectedValues()
    {
        // Arrange
        var origin = "TAT";
        var destination = "ALD";
        var distances = new Dictionary<string, List<Distances>>
        {
            { origin, new List<Distances> { new() { code = destination, lunarYears = 87.43 } } }
        };
        using var httpTest = new HttpTest();
        using var mock = AutoMock.GetLoose(RegisterMemoryCache);

        mock.Mock<IOptions<UrlEndpoints>>().Setup(options => options.Value)
            .Returns(() => new UrlEndpoints { DistancesUrl = "http://localhost:5000/api/distances" });
        httpTest.RespondWithJson(distances);
        var sut = mock.Create<DistanceApiService>();

        // Act
        var actual = await sut.GetDistances();

        // Assert
        Assert.Equal(distances[origin][0].code, actual[origin][0].code);
        Assert.Equal(distances[origin][0].lunarYears, actual[origin][0].lunarYears);
    }

    [Fact]
    public async Task GivenRoute_WhenGetDistancesFailed_ThenReturnsExpectedException()
    {
        // Arrange
        using var httpTest = new HttpTest();
        using var mock = AutoMock.GetLoose(RegisterMemoryCache);

        mock.Mock<IOptions<UrlEndpoints>>().Setup(options => options.Value)
            .Returns(() => new UrlEndpoints { DistancesUrl = "http://localhost:5000/api/distances" });
        httpTest.RespondWith(status: 404);
        var sut = mock.Create<DistanceApiService>();

        // Act
        var actual = sut.GetDistances();

        // Assert error
        await Assert.ThrowsAsync<FlurlHttpException>(async () => await actual);
    }

    private void RegisterMemoryCache(ContainerBuilder cfg)
    {
        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        cfg.RegisterInstance(memoryCache).As<IMemoryCache>();
    }
}