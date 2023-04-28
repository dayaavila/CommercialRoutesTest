using CommercialRoutes.Infrastructure.Interfaces;
using CommercialRoutes.Infrastructure.Models;
using CommercialRoutes.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CommercialRoutes.Infrastructure.Services;

public class PlanetsApiService : IPlanetsApiService
{
    private readonly string _planetsUrl;
    private readonly IMemoryCache _cache;
    private readonly ILogger<PlanetsApiService> _logger;

    public PlanetsApiService(IOptions<UrlEndpoints> urlEndpoints, IMemoryCache cache, ILogger<PlanetsApiService> logger)
    {
        _planetsUrl = urlEndpoints.Value.PlanetsUrl;
        _cache = cache;
        _logger = logger;
    }

    public async Task<List<Planets>> GetPlanets()
    {
        try
        {
            var planets = _cache.Get<List<Planets>>("planets");
            if (planets != null)
            {
                return planets;
            }

            planets = await _planetsUrl.GetJsonAsync<List<Planets>>();
            _cache.Set("planets", planets, TimeSpan.FromMinutes(10));
            return planets;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            throw;
        }
    }
}
