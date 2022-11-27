using eStore.Models.Product;

namespace eStore.Services.Campaigns.ComboCampaign;
public class ComboCampaign : IComboCampaign
{
    private readonly HashSet<int> _campaignProducts;
    private readonly int _comboQuantity;
    private readonly decimal _comboPrice;


    public ComboCampaign()
    {
        _campaignProducts = new HashSet<int>() { 1, 3, 5 };
        _comboQuantity = 2;
        _comboPrice = 30;
    }

    public decimal CalculateBasketPrice(IEnumerable<ProductBase> products)
    {
        var productsList = products.ToList();

        var comboProducts = new List<ProductBase>();

        for (int i = productsList.Count - 1; i >= 0; i--)
        {
            if(_campaignProducts.Contains(productsList[i].Id))
            {
                comboProducts.Add(productsList[i]);
                productsList.RemoveAt(i);
            }
        }

        if (comboProducts.Count % _comboQuantity != 0)
        {
            productsList.Add(comboProducts.Last());
            comboProducts = comboProducts.Skip(1).ToList();
        }

        int validCombos = comboProducts.Count / _comboQuantity;

        var ordinaryProductsPriceSum = productsList.Sum(p => p.Price);

        var comboProductsPriceSum = validCombos * _comboPrice;

        return ordinaryProductsPriceSum + comboProductsPriceSum;
    }
}

