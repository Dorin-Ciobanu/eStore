using eStore.Models.Product;
using eStore.Services.ProductServices;

namespace eStore.Services.ProductInputExtractor;
public class ProductInputExtractor : IProductInputExtractor
{
    private readonly IProductService _productService;

    public ProductInputExtractor(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    public IEnumerable<ProductBase> ExtractProducts(string input)
    {
        var basketProducts = new List<ProductBase>();

        var cachedAvailableProductsDictionary = _productService.GetCachedAvailableProductsDictionary();

        var inputProducts = input.Split(',');

        for(int i = 0; i < inputProducts.Length; i++)
        {
            if(cachedAvailableProductsDictionary.TryGetValue(int.Parse(inputProducts[i]), out ProductBase value))
            {
                basketProducts.Add(value);
            }
            else
            {
                throw new Exception($"Cache error of {nameof(cachedAvailableProductsDictionary)} at {nameof(ProductInputExtractor)}.");
            }
        }

        return basketProducts;
    }
}

