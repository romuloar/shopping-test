using Carpo.Core.Extension;
using Carpo.Core.ResultState;
using Microsoft.EntityFrameworkCore;
using ShoppingTest.Campaign.Core.Domain;
using ShoppingTest.Campaign.Core.Interface;
using ShoppingTest.Campaign.Repository.Context;
using ShoppingTest.Campaign.Repository.Intefaces;

namespace ShoppingTest.Campaign.Repository.Repository
{
    public class CampaignRepository : IAddCampaign, IGetCampaign
    {
        public IUnitOfWork Uow { get; private set; }

        public CampaignRepository(IUnitOfWork unitOfWork)
        {
            Uow = unitOfWork;
        }

        public Task<ResultStateCore<CampaignDomain>> AddCampaign(CampaignDomain campaign)
        {
            try
            {
                campaign.Id = Guid.NewGuid().ToString();
                var addCampaign = Uow.Context.CampaignDomain.Add(campaign);
                return Task.Run(() => addCampaign.Entity.GetResultStateSuccess());
            }
            catch (Exception exc)
            {
                return Task.Run(() => exc.GetResultStateException<CampaignDomain>());
            }
        }

        public Task<ResultStateCore<CampaignDomain>> GetCampaign(string id)
        {
            try
            {
                var list = Uow.Context.CampaignDomain.ToList();
                var campaign = Uow.Context.CampaignDomain.First(c => c.Id == id);

                return Task.Run(() => campaign.GetResultStateSuccess());
            }
            catch (Exception exc)
            {
                return Task.Run(() => exc.GetResultStateException<CampaignDomain>());
            }
        }
    }
}
