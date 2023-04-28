using CommercialRoutes.Infrastructure.Interfaces;
using CommercialRoutes.Infrastructure.Models;
using CommercialRoutes.Infrastructure.Options;
using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CommercialRoutes.Infrastructure.Services;

public class DistanceApiService : IDistancesApiService
{
    private readonly string _distanceUrl;
    private readonly IMemoryCache _cache;
    private readonly ILogger<DistanceApiService> _logger;

    public DistanceApiService(IOptions<UrlEndpoints> urlEndpoints, IMemoryCache cache,  ILogger<DistanceApiService> logger)
    {
        _distanceUrl = urlEndpoints.Value.DistancesUrl;
        _cache = cache;
        _logger = logger;
    }

    public async Task<Dictionary<string, List<Distances>>> GetDistances()
    {
        try
        {
            var distances = _cache.Get<Dictionary<string, List<Distances>>>("distances");
            if (distances != null)
            {
                return distances;
            }

            distances = await _distanceUrl.GetJsonAsync<Dictionary<string, List<Distances>>>();
            _cache.Set("distances", distances, TimeSpan.FromMinutes(10));
            return distances;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            throw;
        }
    }
}
