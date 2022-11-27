using eStore.Models.Product;

namespace eStore.Services.Basket
{
    public class Basket : IBasket
    {
        private readonly List<ProductBase> ProductList;

        public Basket()
        {
            ProductList = new List<ProductBase>();
        }
    }
}
