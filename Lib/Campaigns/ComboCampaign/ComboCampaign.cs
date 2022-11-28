
namespace eStore.Services.Campaigns.ComboCampaign;
public class ComboCampaign : IComboCampaign
{
    private readonly HashSet<int> _campaignProductCategories;
    private readonly int _comboQuantity;
    private readonly decimal _comboPrice;

    public HashSet<int> CampaignProductCategories => _campaignProductCategories;
    public int ComboQuantity => _comboQuantity;
    public decimal ComboPrice => _comboPrice;

    public ComboCampaign()
    {
        _campaignProductCategories = new HashSet<int>() { 1, 3, 5 };
        _comboQuantity = 2;
        _comboPrice = 30;
    }

    public void AddCampaignProducts()
    {
        throw new NotImplementedException(nameof(AddCampaignProducts));
    }

    public void UpdateCampaignProduct()
    {
        throw new NotImplementedException(nameof(UpdateCampaignProduct));
    }
}

