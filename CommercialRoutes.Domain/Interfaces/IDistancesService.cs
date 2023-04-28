namespace CommercialRoutes.Domain.Interfaces;

public interface IDistancesService
{
    public Task<double> GetLunarDaysDistance(string origin, string destination);
}
