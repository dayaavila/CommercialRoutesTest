using CommercialRoutes.Infrastructure.Models;

namespace CommercialRoutes.Infrastructure.Interfaces;

public interface IRebelsApiService
{
    public Task<List<Rebels>> GetRebels();
}
