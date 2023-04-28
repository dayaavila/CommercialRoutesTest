using CommercialRoutes.Infrastructure.Models;

namespace CommercialRoutes.Infrastructure.Interfaces;

public interface IPlanetsApiService
{
    public Task<List<Planets>> GetPlanets();
}
