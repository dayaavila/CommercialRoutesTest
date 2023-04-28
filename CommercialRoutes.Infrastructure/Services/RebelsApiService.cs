using CommercialRoutes.Infrastructure.Interfaces;
using CommercialRoutes.Infrastructure.Models;
using CommercialRoutes.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CommercialRoutes.Infrastructure.Services;

public class RebelsApiService : IRebelsApiService
{
    private readonly string _rebelsUrl;
    private readonly IMemoryCache _cache;
    private readonly ILogger<RebelsApiService> _logger;

    public RebelsApiService(IOptions<UrlEndpoints> urlEndpoints, IMemoryCache cache, ILogger<RebelsApiService> logger)
    {
        _rebelsUrl = urlEndpoints.Value.RebelsUrl;
        _cache = cache;
        _logger = logger;
    }

    public async Task<List<Rebels>> GetRebels()
    {
        try
        {
            var rebels = _cache.Get<List<Rebels>>("rebels");
            if (rebels != null)
            {
                return rebels;
            }

            rebels = await _rebelsUrl.GetJsonAsync<List<Rebels>>();
            _cache.Set("rebels", rebels, TimeSpan.FromMinutes(10));
            return rebels;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            throw;
        }
    }
}
