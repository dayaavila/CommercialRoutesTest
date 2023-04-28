using CommercialRoutes.Domain.Interfaces;
using CommercialRoutes.Domain.Models;

namespace CommercialRoutes.Domain.Services;

public class RoutesService : IRoutesService
{
    private readonly IPlanetsService _planetsService;
    private readonly IDistancesService _distancesService;
    private readonly IPricesService _pricesService;
    private readonly IRebelsService _rebelsService;

    public RoutesService(
        IPlanetsService planetsService,
        IDistancesService distancesService,
        IPricesService pricesService,
        IRebelsService rebelsService)
    {
        _planetsService = planetsService;
        _distancesService = distancesService;
        _pricesService = pricesService;
        _rebelsService = rebelsService;
    }

    public async Task<Route> GetRoute(string origin, string destination)
    {
        var originPlanet = await _planetsService.GetPlanetByName(origin) ?? throw new ArgumentException("Invalid origin planet");
        var destinationPlanet = await _planetsService.GetPlanetByName(destination) ?? throw new ArgumentException("Invalid destination planet");
        var lunarDaysDistance = await _distancesService.GetLunarDaysDistance(originPlanet.code, destinationPlanet.code);
        return new Route
        {
            Origin = originPlanet.planetName,
            Destination = destinationPlanet.planetName,
            Distance = Convert.ToInt32(lunarDaysDistance)
        };
    }

    public async Task<RoutesPrices> GetRoutesPrice(string origin, string destination)
    {
        var currentDay = (int)DateTime.UtcNow.DayOfWeek;
        var originPlanet = await _planetsService.GetPlanetByName(origin) ?? throw new ArgumentException("Invalid origin planet");
        var destinationPlanet = await _planetsService.GetPlanetByName(destination) ?? throw new ArgumentException("Invalid destination planet");
        var lunarDaysDistance = await _distancesService.GetLunarDaysDistance(originPlanet.code, destinationPlanet.code);
        var pricesPerLunarDay = await _pricesService.GetPricesPerLunarDay(originPlanet.sector, destinationPlanet.sector, currentDay);
        var taxes = await _rebelsService.GetTaxes(originPlanet.code, destinationPlanet.code);
        var routesPrices = CalculateRoutePrices(lunarDaysDistance, pricesPerLunarDay, taxes);
        return routesPrices;
    }

    private static RoutesPrices CalculateRoutePrices(double lunarDaysDistance, double pricesPerLunarDay, Taxes taxes)
    {
        var basePrice = Math.Round(lunarDaysDistance * pricesPerLunarDay, 2);
        var totalTaxes = Math.Round(taxes.originDefenseCost + taxes.destinationDefenseCost + taxes.eliteDefenseCost, 2);
        var routesPrice = new RoutesPrices
        {
            totalAmount = Math.Round(basePrice + basePrice * totalTaxes, 2),
            pricePerLunarDay = Math.Round(pricesPerLunarDay + pricesPerLunarDay * totalTaxes, 2),
            taxes = taxes
        };
        return routesPrice;
    }
}
