namespace CommercialRoutes.Application.Dtos;

public class RoutePricesDto
{
    public double totalAmount { get; set; }
    public double pricePerLunarDay { get; set; }
    public TaxesDto taxes { get; set; }
}
