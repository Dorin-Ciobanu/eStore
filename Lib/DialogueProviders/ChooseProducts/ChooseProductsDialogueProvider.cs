using eStore.Models.Product;
using eStore.Services.Calculators;
using eStore.Services.ProductInputExtractor;
using eStore.Services.ProductServices;
using System.Text;
using System.Text.RegularExpressions;

namespace eStore.Services.DialogueProviders.ChooseProducts;
public class ChooseProductsDialogueProvider : DialogueProviderBase, IChooseProductsDialogueProvider
{
    private readonly IProductService _productService;
    private readonly IInputProductExtractor _inputProductExtractor;
    private readonly ICheckoutPriceCalculator _checkoutPriceCalculator;

    public ChooseProductsDialogueProvider(IProductService productService
        , IInputProductExtractor inputProductExtractor
        , ICheckoutPriceCalculator checkoutPriceCalculator)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _inputProductExtractor = inputProductExtractor ?? throw new ArgumentNullException(nameof(inputProductExtractor));
        _checkoutPriceCalculator = checkoutPriceCalculator ?? throw new ArgumentNullException(nameof(checkoutPriceCalculator));
    }

    public override void StartDialogue()
    {
        Console.Clear();

        Console.WriteLine($"You can choose from our range of products by writing down the products number divided by comma." +
            $"{Environment.NewLine}If you want more products of the same type, add it multiple times. " +
            $"{Environment.NewLine}Example \"1,1,1,3,3,4,4,4,6,6\"." +
            $"{Environment.NewLine}The products available :{EnumerateAvailableProducts(_productService.GetAvailableProducts())}");

        var userAnswer = EnsureValidAnswer(Console.ReadLine());

        var productList = _inputProductExtractor.ExtractProducts(userAnswer);

        var regularPrice = _checkoutPriceCalculator.CalculateRegularPrice(productList);

        var comboCampaignPrice = _checkoutPriceCalculator.CalculateComboCampaignBasketPrice(productList);

        var volumeCampaignPrice = _checkoutPriceCalculator.CalculateVolumeCampaignBasketPrice(productList);

        Console.WriteLine($"{Environment.NewLine}The regular checkout price is: {regularPrice}$." +
            $"{Environment.NewLine}The combo campaign checkout price is: {comboCampaignPrice}$." +
            $"{Environment.NewLine}The volume campaign checkout price is: {volumeCampaignPrice}$.");

    }

    protected override string EnsureValidAnswer(string input)
    {
        while(string.IsNullOrEmpty(input))
        {
            Console.WriteLine($"{Environment.NewLine}Basket is empty. Please select at least one item.");
            input = Console.ReadLine();
        }

        var regx = new Regex(@"^\d+(,\d+)*$",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);

        while (!regx.Match(input).Success)
        {
            Console.WriteLine($"{Environment.NewLine}The text you have inserted is in wrong format. Please follow the example above.");
            input = Console.ReadLine();
        }

        return input;
    }

    private string EnumerateAvailableProducts(IList<ProductBase> products)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var group in products.GroupBy(p => p.ProductCategoryId))
        {
            var product = group.First();
            sb.Append($"{Environment.NewLine}{product.ProductCategoryId} - {product.Name}");
        }

        return sb.ToString();
    }
}

