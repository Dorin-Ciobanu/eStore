using eStore.Models.Product;
using eStore.Models.Product.Implementations;
using Microsoft.Extensions.Caching.Memory;

namespace eStore.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private const string _cachedAvailableProductsListName  = "cachedAvailableProductsList";
        private const string _cachedAvailableProductsHashSetName  = "cachedAvailableProductsHashSet";

        private readonly IMemoryCache _memoryCache;

        public ProductService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public IList<ProductBase> GetAvailableProducts()
        {
            var products = new List<ProductBase>();

            foreach (var i in Enumerable.Range(1, 10))
            {
                products.Add(new Apples { Id = i, Name = nameof(Apples), Price = 20, ProductCategoryId = 1 });
                products.Add(new Bread { Id = i, Name = nameof(Bread), Price = 20, ProductCategoryId = 2 });
                products.Add(new Cheese { Id = i, Name = nameof(Cheese), Price = 20, ProductCategoryId = 3 });
                products.Add(new Chocolate{ Id = i, Name = nameof(Chocolate), Price = 20, ProductCategoryId = 4 });
                products.Add(new Cookies { Id = i, Name = nameof(Cookies), Price = 20, ProductCategoryId = 5 });
                products.Add(new Pizza { Id = i, Name = nameof(Pizza), Price = 20, ProductCategoryId = 6 });
                products.Add(new Soda { Id = i, Name = nameof(Soda), Price = 20, ProductCategoryId = 7 });
            }

            return products; ;
        }
    }
}
