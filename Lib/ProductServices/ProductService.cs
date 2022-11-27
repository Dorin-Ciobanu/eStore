using eStore.Models.Product;
using eStore.Models.Product.Implementations;
using Microsoft.Extensions.Caching.Memory;

namespace eStore.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private const string _cachedAvailableProductsListName  = "cachedAvailableProductsList";
        private const string _cachedAvailableProductsDictionaryName  = "cachedAvailableProductsDictionary";

        private readonly IMemoryCache _memoryCache;

        public ProductService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public IList<ProductBase> GetAvailableProducts()
        {
            return new List<ProductBase>
            {
                new Apples{ Id = 1, Name = nameof(Apples), Price = 20 },
                new Bread{ Id = 2, Name = nameof(Bread), Price = 20 },
                new Cheese{ Id = 3, Name = nameof(Cheese), Price = 20 },
                new Chocolate{ Id = 4, Name = nameof(Chocolate), Price = 20 },
                new Cookies{ Id = 5, Name = nameof(Cookies), Price = 20 },
                new Pizza{ Id = 6, Name = nameof(Pizza), Price = 20 },
                new Soda{ Id = 7, Name = nameof(Soda), Price = 20 }
            };
        }

        //A CachedEntity class shall do this work
        public IList<ProductBase> GetCachedAvailableProductsList()
        {
            var cachedAvailableProducts = _memoryCache.Get(_cachedAvailableProductsListName);

            if (cachedAvailableProducts == null)
            {
                cachedAvailableProducts = GetAvailableProducts();
                _memoryCache.Set(_cachedAvailableProductsListName, cachedAvailableProducts, TimeSpan.FromMinutes(5));
            }

            return (List<ProductBase>)cachedAvailableProducts;
        }

        public IDictionary<int, ProductBase> GetCachedAvailableProductsDictionary()
        {
            var cachedAvailableProducts = _memoryCache.Get(_cachedAvailableProductsDictionaryName);

            if (cachedAvailableProducts == null)
            {
                cachedAvailableProducts = GetAvailableProducts().ToDictionary( x=> x.Id, x => x);
                _memoryCache.Set(_cachedAvailableProductsDictionaryName, cachedAvailableProducts, TimeSpan.FromMinutes(5));
            }

            return (Dictionary<int, ProductBase>)cachedAvailableProducts;
        }
    }
}
