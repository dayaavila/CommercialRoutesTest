
namespace CommercialRoutes.Domain.Interfaces;

public interface IPricesService
{
    public Task<double> GetPricesPerLunarDay(string originSector, string destinationSector, int currentDay);
}
