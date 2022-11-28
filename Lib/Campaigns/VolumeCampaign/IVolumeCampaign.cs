using eStore.Models.Product;

namespace eStore.Services.Campaigns.VolumeCampaign;
public interface IVolumeCampaign
{
    public Dictionary<int, decimal> CampaignProductCategoriesPriceDictionary { get; }
    public int VolumeCampaignQuantity { get; }

    public void AddCampaignProducts(IEnumerable<KeyValuePair<int, decimal>> newCampaignProducts);
    public void UpdateCampaignProduct();
}
