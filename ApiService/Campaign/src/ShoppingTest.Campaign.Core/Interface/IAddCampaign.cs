using Carpo.Core.Interface.Domain;
using Carpo.Core.ResultState;
using ShoppingTest.Campaign.Core.Domain;

namespace ShoppingTest.Campaign.Core.Interface
{
    public interface IAddCampaign : IContractUseCase
    {
        public Task<ResultStateCore<CampaignDomain>> AddCampaign(CampaignDomain campaign);
    }
}
