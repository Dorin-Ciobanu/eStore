using eStore.Models.Product;
using eStore.Services.Campaigns.ComboCampaign;
using eStore.Services.Campaigns.VolumeCampaign;

namespace eStore.Services.Calculators;
public class CheckoutPriceCalculator : ICheckoutPriceCalculator
{
    private readonly IComboCampaign _comboCampaign;
    private readonly IVolumeCampaign _volumeCampaign;

    public CheckoutPriceCalculator(IComboCampaign comboCampaign,
        IVolumeCampaign volumeCampaign)
    {
        _comboCampaign = comboCampaign ?? throw new ArgumentNullException(nameof(comboCampaign));
        _volumeCampaign = volumeCampaign ?? throw new ArgumentNullException(nameof(volumeCampaign));
    }

    public decimal CalculateRegularPrice(IEnumerable<ProductBase> products)
    {
        return products.Sum(p => p.Price);
    }

    public decimal CalculateComboCampaignBasketPrice(IEnumerable<ProductBase> products)
    {
        if (products == null)
        {
            throw new ArgumentNullException(nameof(products));
        }

        if (products.Count() == 0)
        {
            throw new ArgumentNullException($"The basket is empty! at {nameof(ComboCampaign)}");
        }
        
        var campaignProducts = products.Where(p => _comboCampaign.CampaignProductCategories.Contains(p.ProductCategoryId));
        var campaignProductsCount = campaignProducts.Count();
        var campaignProductsCombinations = campaignProductsCount / _comboCampaign.ComboQuantity;

        if(campaignProductsCount % _comboCampaign.ComboQuantity != 0)
        {
            var excessProducts = campaignProductsCount - campaignProductsCombinations * _comboCampaign.ComboQuantity;
            campaignProducts = campaignProducts.SkipLast(excessProducts);
            campaignProductsCount = campaignProductsCount - excessProducts;
        }

        var regularPriceProducts = products.Except(campaignProducts);

        var regularProductsPrice = regularPriceProducts.Sum(p => p.Price);

        var campaignProductsPrice = campaignProductsCombinations * _comboCampaign.ComboPrice;

        return regularProductsPrice + campaignProductsPrice;
    }

    public decimal CalculateVolumeCampaignBasketPrice(IEnumerable<ProductBase> products)
    {
        if (products == null)
        {
            throw new ArgumentNullException(nameof(products));
        }

        if (products.Count() == 0)
        {
            throw new ArgumentNullException($"The basket is empty! at {nameof(CheckoutPriceCalculator)}");
        }

        var basketPrice = decimal.Zero;

        var groupedProductsList = products.GroupBy(p => p.ProductCategoryId).ToList();

        foreach (var group in groupedProductsList)
        {
            if (_volumeCampaign.CampaignProductCategoriesPriceDictionary.ContainsKey(group.Key))
            {
                int regularPriceProducts = 0;

                var groupCount = group.Count();
                var campaignPriceCombinations = groupCount / _volumeCampaign.VolumeCampaignQuantity;

                if (groupCount % _volumeCampaign.VolumeCampaignQuantity != 0)
                {
                    regularPriceProducts = groupCount - campaignPriceCombinations * _volumeCampaign.VolumeCampaignQuantity;
                }

                var campaignProductsPrice = campaignPriceCombinations * _volumeCampaign.CampaignProductCategoriesPriceDictionary[group.Key];

                var regularProductsPrice = group.Take(regularPriceProducts).Sum(p => p.Price);

                basketPrice += campaignProductsPrice + regularProductsPrice;
            }
            else
            {
                basketPrice += group.Sum(p => p.Price);
            }
        }

        return basketPrice;
    }
}
