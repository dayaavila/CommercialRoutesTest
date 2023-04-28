using CommercialRoutes.Infrastructure.Models;

namespace CommercialRoutes.Infrastructure.Interfaces;

public interface IPricesApiService
{
    public Task<List<Prices>> GetPrices();
}
