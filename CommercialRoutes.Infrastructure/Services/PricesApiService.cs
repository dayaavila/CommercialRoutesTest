using CommercialRoutes.Infrastructure.Interfaces;
using CommercialRoutes.Infrastructure.Models;
using CommercialRoutes.Infrastructure.Options;
using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CommercialRoutes.Infrastructure.Services;

public class PricesApiService : IPricesApiService
{
    private readonly string _pricesUrl;
    private readonly IMemoryCache _cache;
    private readonly ILogger<PricesApiService> _logger;

    public PricesApiService(IOptions<UrlEndpoints> urlEndpoints, IMemoryCache cache, ILogger<PricesApiService> logger)
    {
        _pricesUrl = urlEndpoints.Value.PricesUrl;
        _cache = cache;
        _logger = logger;
    }

    public async Task<List<Prices>> GetPrices()
    {
        try
        {
            var prices = _cache.Get<List<Prices>>("prices");
            if (prices != null)
            {
                return prices;
            }

            prices = await _pricesUrl.GetJsonAsync<List<Prices>>();
            _cache.Set("prices", prices, TimeSpan.FromMinutes(10));
            return prices;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            throw;
        }
    }
}
