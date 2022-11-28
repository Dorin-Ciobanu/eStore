
namespace eStore.Services.Campaigns.ComboCampaign;
public interface IComboCampaign
{
    public HashSet<int> CampaignProductCategories { get; }
    public int ComboQuantity { get; }
    public decimal ComboPrice { get; }

    public void AddCampaignProducts();

    public void UpdateCampaignProduct();
}
