using eStore.Services.Basket;
using eStore.Services.Campaigns.ComboCampaign;
using eStore.Services.DialogueProviders.ChooseProducts;
using eStore.Services.DialogueProviders.Greeting;
using eStore.Services.ProductInputExtractor;
using eStore.Services.ProductServices;
using Microsoft.Extensions.DependencyInjection;

namespace eStore;

internal class ServicesInstaller
{
    internal static void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IBasket, Basket>();
        serviceCollection.AddSingleton<IChooseProductsDialogueProvider, ChooseProductsDialogueProvider>();
        serviceCollection.AddSingleton<IGreetingDialogueProvider, GreetingDialogueProvider>();
        serviceCollection.AddSingleton<IProductService, ProductService>();
        serviceCollection.AddSingleton<IProductInputExtractor, ProductInputExtractor>();
        serviceCollection.AddSingleton<IComboCampaign, ComboCampaign>();
        serviceCollection.AddMemoryCache();
    }
}
