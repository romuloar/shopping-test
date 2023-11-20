using Carpo.Core.Extension;
using Carpo.Core.ResultState;
using ShoppingTest.Campaign.Core.Domain;
using ShoppingTest.Campaign.Core.Interface;

namespace ShoppingTest.Campaign.Application.Case
{
    public class GetCampaignCase
    {
        private IGetCampaign _contract;
        public GetCampaignCase(IGetCampaign contract)
        {
            _contract = contract ?? throw new ArgumentNullException(nameof(contract)); ; 
        }

        public Task<ResultStateCore<CampaignDomain>> Execute(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Task.Run(() => new CampaignDomain().GetResultStateError("Id is empty"));
            }

           return Task.Run(() => _contract.GetCampaign(id));
        }
    }
}
