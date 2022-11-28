
namespace eStore.Services.Campaigns.VolumeCampaign;

public class VolumeCampaign : IVolumeCampaign
{
    private readonly Dictionary<int, decimal> _campaignProductCategoriesPrice;
    private readonly int _volumeQuantity;

    public Dictionary<int, decimal> CampaignProductCategoriesPriceDictionary => _campaignProductCategoriesPrice;
    public int VolumeCampaignQuantity => _volumeQuantity;


    public VolumeCampaign()
    {
        _campaignProductCategoriesPrice = new Dictionary<int, decimal>()
        {
            { 1, 20 },
            { 2, 20 },
            { 6, 20 }
        };
        _volumeQuantity = 2;
    }

    public void AddCampaignProducts(IEnumerable<KeyValuePair<int, decimal>> newCampaignProducts)
    {
        if (newCampaignProducts == null)
        {
            throw new ArgumentNullException(nameof(newCampaignProducts));
        }

        foreach (KeyValuePair<int, decimal> product in newCampaignProducts)
        {
            if (_campaignProductCategoriesPrice.ContainsKey(product.Key))
            {
                throw new Exception($"The product with the id:{product.Key} is already in the volume campaign. You may want to update it's price instead");
            }
            _campaignProductCategoriesPrice.Add(product.Key, product.Value);
        }
    }

    public void UpdateCampaignProduct()
    {
        throw new NotImplementedException(nameof(UpdateCampaignProduct));
    }
}

