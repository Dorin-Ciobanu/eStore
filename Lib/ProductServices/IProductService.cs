
using eStore.Models.Product;

namespace eStore.Services.ProductServices;
public interface IProductService
{
    public IList<ProductBase> GetAvailableProducts();
}

