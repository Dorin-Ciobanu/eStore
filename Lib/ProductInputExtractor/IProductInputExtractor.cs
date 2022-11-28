
using eStore.Models.Product;

namespace eStore.Services.ProductInputExtractor;
public interface IInputProductExtractor
{
    public IEnumerable<ProductBase> ExtractProducts(string input);
}

