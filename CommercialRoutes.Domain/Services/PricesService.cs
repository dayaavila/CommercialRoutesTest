using CommercialRoutes.Domain.Interfaces;
using CommercialRoutes.Infrastructure.Interfaces;

namespace CommercialRoutes.Domain.Services;

public class PricesService : IPricesService
{
    private readonly IPricesApiService _pricesApiService;

    public PricesService(IPricesApiService pricesApiService)
    {
        _pricesApiService = pricesApiService;
    }

    public async Task<double> GetPricesPerLunarDay(string originSector, string destinationSector, int currentDay)
    {
        if (originSector == destinationSector)
        {
            return await PricesPerLunarDay(originSector, currentDay);
        }

        var originPricesPerLunarDay = await PricesPerLunarDay(originSector, currentDay);
        var destinationPricesPerLunarDay = await PricesPerLunarDay(destinationSector, currentDay);
        var averagePricePerLunarDay = (originPricesPerLunarDay + destinationPricesPerLunarDay) / 2.0;
        return Math.Round(averagePricePerLunarDay, 2);
    }

    private async Task<double> PricesPerLunarDay(string originSector, int currentDay)
    {
        var prices = await _pricesApiService.GetPrices();
        var pricePerLunarDay = prices
            .Single(price => price.dayOfTheWeek == currentDay && price.sector == originSector);
        return Math.Round(pricePerLunarDay.pricesPerLunarDay, 2);
    }
}
