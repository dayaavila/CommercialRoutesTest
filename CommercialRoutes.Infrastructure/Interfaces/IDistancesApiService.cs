using CommercialRoutes.Infrastructure.Models;

namespace CommercialRoutes.Infrastructure.Interfaces;

public interface IDistancesApiService
{
    public Task<Dictionary<string, List<Distances>>> GetDistances();
}
