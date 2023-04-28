using CommercialRoutes.Domain.Services;
using CommercialRoutes.Infrastructure.Interfaces;
using CommercialRoutes.Infrastructure.Models;

namespace CommercialRoutes.Domain.Test.Services;

public class DistanceServiceTests
{
    [Fact]
    public async Task GivenUnavailableRoute_WhenGetLunarDaysDistance_ThenReturnsKeyNotFoundException()
    {
        // Arrange
        var origin = "NA";
        var destination = "NA";
        using var mock = AutoMock.GetLoose();

        mock.Mock<IDistancesApiService>()
            .Setup(service => service.GetDistances())
            .ReturnsAsync(() => new Dictionary<string, List<Distances>>());

        var sut = mock.Create<DistanceService>();

        // Act
        var actual = sut.GetLunarDaysDistance(origin, destination);

        // Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await actual);
    }

    [Fact]
    public async Task GivenAvailableRoute_WhenGetLunarDaysDistance_ThenReturnExpectedDistance()
    {
        // Arrange
        var origin = "TAT";
        var destination = "ALD";
        var expected = 31911.95;
        using var mock = AutoMock.GetLoose();
        var distances = new Dictionary<string, List<Distances>>
        {
            { origin, new List<Distances> { new() { code = destination, lunarYears = 87.43 } } }
        };
        mock.Mock<IDistancesApiService>()
            .Setup(userQuery => userQuery.GetDistances())
            .ReturnsAsync(() => distances);

        var actual = mock.Create<DistanceService>();

        // Act
        var result = await actual.GetLunarDaysDistance(origin, destination);

        // Assert
        Assert.Equal(expected, result);
    }
}