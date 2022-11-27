
using eStore.Models.Product;

namespace eStore.Services.ProductInputExtractor;
public interface IProductInputExtractor
{
    public IEnumerable<ProductBase> ExtractProducts(string input);
}

