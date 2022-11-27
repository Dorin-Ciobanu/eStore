using eStore;
using eStore.Services.DialogueProviders.Greeting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


IHost _host = Host.CreateDefaultBuilder().ConfigureServices(
    services => ServicesInstaller.ConfigureServices(services))
    .Build();

Store _esTore = new Store(_host.Services.GetService<IGreetingDialogueProvider>());
_esTore.RunStore();
