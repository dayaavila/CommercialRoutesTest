using CommercialRoutes.Domain.Interfaces;
using CommercialRoutes.Domain.Models;
using CommercialRoutes.Infrastructure.Interfaces;

namespace CommercialRoutes.Domain.Services;

public class RebelsService : IRebelsService
{
    private readonly IRebelsApiService _rebelsApiService;

    public RebelsService(IRebelsApiService rebelsApiService)
    {
        _rebelsApiService = rebelsApiService;
    }

    public async Task<Taxes> GetTaxes(string originCode, string destinationCode)
    {
        var rebels = await _rebelsApiService.GetRebels();
        var originRebelInfluence = rebels.Single(rebel => rebel.code == originCode).rebelInfluence;
        var destinationRebelInfluence = rebels.Single(rebel => rebel.code == destinationCode).rebelInfluence;
        var eliteDefenseCost = originRebelInfluence + destinationRebelInfluence > 40 ?
            (40 + Math.Abs(originRebelInfluence - destinationRebelInfluence)) / 100.0 : 0.0;

        return new Taxes
        {
            originDefenseCost = Math.Round(originRebelInfluence / 100.0, 2),
            destinationDefenseCost = Math.Round(destinationRebelInfluence / 100.0, 2),
            eliteDefenseCost = Math.Round(eliteDefenseCost, 2)
        };
    }
}
