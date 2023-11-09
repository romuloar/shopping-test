using Carpo.Core.Extension;
using Carpo.Core.ResultState;
using ShoppingTest.Campaign.Core.Domain;
using ShoppingTest.Campaign.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingTest.Campaign.Application.UnitTest.Memory
{
    public class CampaignMemory : IAddCampaign, IGetCampaign
    {
        private List<CampaignDomain> _listCampaign;
        public List<CampaignDomain> ListCampaign
        {
            get { return _listCampaign; }
        }

        public CampaignMemory()
        {
            _listCampaign = new List<CampaignDomain>
            {
                new CampaignDomain
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Campaign 1",
                    Description = "Campaign 1 description",
                    InitialDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(3),
                }
            };
        }

        public Task<ResultStateCore<CampaignDomain>> AddCampaign(CampaignDomain campaign)
        {
            campaign.Id = Guid.NewGuid().ToString();
            _listCampaign.Add(campaign);
            return Task.Run(() => campaign.GetResultStateSuccess());
        }

        public Task<ResultStateCore<CampaignDomain>> GetCampaign(string id)
        {
            var domain = _listCampaign.FirstOrDefault(c => c.Id == id) ?? new CampaignDomain();
            
            return Task.Run(() => domain.GetResultStateSuccess());
        }
    }
}
