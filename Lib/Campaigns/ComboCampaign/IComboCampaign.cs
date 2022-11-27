using eStore.Models.Product;

namespace eStore.Services.Campaigns.ComboCampaign;
public interface IComboCampaign
{
    public decimal CalculateBasketPrice(IEnumerable<ProductBase> products);
}
