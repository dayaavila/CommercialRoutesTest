using CommercialRoutes.Domain.Interfaces;
using CommercialRoutes.Infrastructure.Interfaces;
using CommercialRoutes.Infrastructure.Models;

namespace CommercialRoutes.Domain.Services;

public class PlanetsService : IPlanetsService
{
    private readonly IPlanetsApiService _planetsApiService;

    public PlanetsService(IPlanetsApiService planetsApiService)
    {
        _planetsApiService = planetsApiService;
    }

    public async Task<Planets?> GetPlanetByName(string planetName)
    {
        var planets = await _planetsApiService.GetPlanets();
        return planets.FirstOrDefault(planet => planet.planetName == planetName);
    }
}
