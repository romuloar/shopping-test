using Microsoft.EntityFrameworkCore;
using ShoppingTest.Campaign.Core.Domain;
using ShoppingTest.Campaign.Repository.Intefaces;

namespace ShoppingTest.Campaign.Repository.Context
{
    public class CampaignContext : DbContext, ICampaignContext
    {
        public DbSet<CampaignDomain> CampaignDomain { get; set; }

        //Using Sqlite just to initialize, then we need to use another database, SQL, MongoDB, etc.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(connectionString: "DataSource=campaign.db;Cache=Shared");  
    }
}
