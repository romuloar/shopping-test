using Carpo.Core.Extension;
using Carpo.Core.ResultState;
using ShoppingTest.Campaign.Core.Domain;
using ShoppingTest.Campaign.Core.Interface;

namespace ShoppingTest.Campaign.Application.Case
{
    public class AddCampaignCase
    {
        private IAddCampaign _contract;
        public AddCampaignCase(IAddCampaign contract)
        {
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
        }

        public Task<ResultStateCore<CampaignDomain>> Execute(CampaignDomain campaign)
        {
            if (campaign == null)
            {
                return Task.Run(() => new CampaignDomain().GetResultStateError("Campaign is required"));
            }

            if (!string.IsNullOrEmpty(campaign.Id))
            {
                return Task.Run(() => new CampaignDomain().GetResultStateError("Id not empty"));
            }

            if(!campaign.IsValidDomain)
            {
                return Task.Run(() => campaign.GetResultStateError("Domain is not valid"));
            }

           return _contract.AddCampaign(campaign);
        }
    }
}
