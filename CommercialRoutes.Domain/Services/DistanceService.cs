using CommercialRoutes.Domain.Interfaces;
using CommercialRoutes.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace CommercialRoutes.Domain.Services;

public class DistanceService : IDistancesService
{
    private readonly IDistancesApiService _distancesApiService;
    private readonly ILogger<DistanceService> _logger;

    public DistanceService(
        IDistancesApiService distancesApiService,
        ILogger<DistanceService> logger)
    {
        _distancesApiService = distancesApiService;
        _logger = logger;
    }

    public async Task<double> GetLunarDaysDistance(string originCode, string destinationCode)
    {
        try
        {
            var distances = await _distancesApiService.GetDistances();
            var destinationDistance = distances[originCode]
                .Single(distance => distance.code == destinationCode);
            return Math.Round(destinationDistance.lunarYears * 365.0, 2);
        }
        catch (Exception exception)
        {
            if (exception is KeyNotFoundException or InvalidOperationException)
            {
                _logger.LogTrace(exception, "Distance not found");
                throw;
            }

            _logger.LogError(exception, exception.Message);
            throw;
        }
        
    }
}
