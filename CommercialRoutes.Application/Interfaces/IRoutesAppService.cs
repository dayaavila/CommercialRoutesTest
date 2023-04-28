using CommercialRoutes.Application.Dtos;

namespace CommercialRoutes.Application.Interfaces;

public interface IRoutesAppService
{
    public Task<RouteDto> GetRoute(string origin, string destination);

    public Task<RoutePricesDto> GetRoutePrices(string origin, string destination);
}
