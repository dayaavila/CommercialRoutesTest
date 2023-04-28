using CommercialRoutes.Application.Dtos;
using CommercialRoutes.Application.Interfaces;
using CommercialRoutes.Domain.Interfaces;

namespace CommercialRoutes.Application.Services;

public class RoutesAppService : IRoutesAppService
{
    private readonly IRoutesService _routesService;

    public RoutesAppService(IRoutesService routesService)
    {
        _routesService = routesService;
    }

    public async Task<RouteDto> GetRoute(string origin, string destination)
    {
        var route = await _routesService.GetRoute(origin, destination);
        var routeDto = new RouteDto
        {
            Origin = route.Origin,
            Destination = route.Destination,
            Distance = route.Distance
        };

        return routeDto;
    }

    public async Task<RoutePricesDto> GetRoutePrices(string origin, string destination)
    {
        var routesPrice = await _routesService.GetRoutesPrice(origin, destination);
        var routePricesDto = new RoutePricesDto
        {
            totalAmount = routesPrice.totalAmount,
            pricePerLunarDay = routesPrice.pricePerLunarDay,
            taxes = new TaxesDto
            {
                originDefenseCost = routesPrice.taxes.originDefenseCost,
                destinationDefenseCost = routesPrice.taxes.destinationDefenseCost,
                eliteDefenseCost = routesPrice.taxes.eliteDefenseCost
            }
        };

        return routePricesDto;
    }
}
