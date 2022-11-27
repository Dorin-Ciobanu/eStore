
using eStore.Models.Product;

namespace eStore.Services.Calculators;
public class BasePriceCalculator
{
    public decimal CalculatePrice(IEnumerable<ProductBase> productList)
    {
        var price = decimal.Zero;

        foreach (var product in productList)
        {
            price += product.Price;
        }

        return price;
    }
}
