using CommercialRoutes.Domain.Models;

namespace CommercialRoutes.Domain.Interfaces;

public interface IRebelsService
{
    public Task<Taxes> GetTaxes(string originCode, string destinationCode);
}
