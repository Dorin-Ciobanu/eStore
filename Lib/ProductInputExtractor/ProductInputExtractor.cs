using eStore.Models.Product;
using eStore.Services.ProductServices;

namespace eStore.Services.ProductInputExtractor;
public class ProductInputExtractor : IInputProductExtractor
{
    private readonly IProductService _productService;

    public ProductInputExtractor(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    public IEnumerable<ProductBase> ExtractProducts(string input)
    {
        var basketProducts = new List<ProductBase>();

        var availableProducts = _productService.GetAvailableProducts();

        var inputProducts = input.Split(',').GroupBy(i => i);

        //for now I assume that there are enough products available
        foreach(var group in inputProducts)
        {
            basketProducts.AddRange(availableProducts.Where(p => p.ProductCategoryId == int.Parse(group.Key)).Take(group.Count()));
        }

        return basketProducts;
    }
}

