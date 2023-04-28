namespace CommercialRoutes.Domain.Models;

public  class RoutesPrices
{
    public double totalAmount { get; set; }
    public double pricePerLunarDay { get; set; }
    public Taxes taxes { get; set; }
}
