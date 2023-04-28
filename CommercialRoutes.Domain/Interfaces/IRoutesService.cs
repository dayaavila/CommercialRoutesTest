using CommercialRoutes.Domain.Models;

namespace CommercialRoutes.Domain.Interfaces;

public interface IRoutesService
{
    public Task<Route> GetRoute(string origin, string destination);

    public Task<RoutesPrices> GetRoutesPrice(string origin, string destination);
}
