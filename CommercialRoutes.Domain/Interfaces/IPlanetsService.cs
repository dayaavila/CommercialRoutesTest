using CommercialRoutes.Infrastructure.Models;

namespace CommercialRoutes.Domain.Interfaces;

public interface IPlanetsService
{
    public Task<Planets?> GetPlanetByName(string planetName);
}
