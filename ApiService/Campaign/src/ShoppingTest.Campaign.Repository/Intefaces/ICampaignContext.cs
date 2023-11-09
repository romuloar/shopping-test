using Microsoft.EntityFrameworkCore;
using ShoppingTest.Campaign.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingTest.Campaign.Repository.Intefaces
{
    public interface ICampaignContext
    {
        public DbSet<CampaignDomain> CampaignDomain { get; set; }
    }
}
