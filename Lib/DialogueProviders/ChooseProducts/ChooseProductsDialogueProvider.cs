using eStore.Models.Product;
using eStore.Services.Campaigns.ComboCampaign;
using eStore.Services.ProductInputExtractor;
using eStore.Services.ProductServices;
using System.Text;
using System.Text.RegularExpressions;

namespace eStore.Services.DialogueProviders.ChooseProducts;
public class ChooseProductsDialogueProvider : DialogueProviderBase, IChooseProductsDialogueProvider
{
    private readonly IProductService _productService;
    private readonly IProductInputExtractor _productInputExtractor;
    private readonly IComboCampaign _comboCampaign;

    public ChooseProductsDialogueProvider(IProductService productService
        , IProductInputExtractor productInputExtractor
        , IComboCampaign comboCampaign)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _productInputExtractor = productInputExtractor ?? throw new ArgumentNullException(nameof(productInputExtractor));
        _comboCampaign = comboCampaign ?? throw new ArgumentNullException(nameof(comboCampaign));
    }

    public override void StartDialogue()
    {
        Console.Clear();

        Console.WriteLine($"You can choose from our range of products by writing down the products number divided by comma." +
            $"{Environment.NewLine}If you want more products of the same type, add it multiple times. " +
            $"{Environment.NewLine}Example \"1,1,1,3,3,4,4,4,9,9\"." +
            $"{Environment.NewLine}The products available :{EnumerateAvailableProducts(_productService.GetAvailableProducts())}");

        var userAnswer = EnsureValidAnswer(Console.ReadLine());

        var productList = _productInputExtractor.ExtractProducts(userAnswer);

        var checkoutPrice = _comboCampaign.CalculateBasketPrice(productList);

        Console.WriteLine($"{Environment.NewLine}Your campaign checkout price is: {checkoutPrice}$.");

    }

    protected override string EnsureValidAnswer(string input)
    {
        var regx = new Regex(@"^\d+(,\d+)*$",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);

        while (!regx.Match(input).Success)
        {
            Console.WriteLine($"{Environment.NewLine}The text you have inserted was in wrong format. Please try again.");
            input = Console.ReadLine();
        }

        return input;
    }

    private string EnumerateAvailableProducts(IList<ProductBase> products)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var product in products)
        {
            sb.Append($"{Environment.NewLine}{product.Id} - {product.Name}");
        }

        return sb.ToString();
    }
}

