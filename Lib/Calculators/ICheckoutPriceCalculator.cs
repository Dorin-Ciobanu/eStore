using eStore.Models.Product;

namespace eStore.Services.Calculators;
public interface ICheckoutPriceCalculator
{
    public decimal CalculateRegularPrice(IEnumerable<ProductBase> products);
    public decimal CalculateComboCampaignBasketPrice(IEnumerable<ProductBase> products);
    public decimal CalculateVolumeCampaignBasketPrice(IEnumerable<ProductBase> products);
}
