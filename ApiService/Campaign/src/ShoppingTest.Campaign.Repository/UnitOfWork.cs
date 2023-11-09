using ShoppingTest.Campaign.Repository.Context;
using ShoppingTest.Campaign.Repository.Intefaces;
using ShoppingTest.Campaign.Repository.Repository;

namespace ShoppingTest.Campaign.Repository
{
    public class UnitOfWork: IDisposable, IUnitOfWork   
    {
        private ICampaignContext _context;
        public ICampaignContext Context { get { return _context; } } 

        public UnitOfWork(ICampaignContext context)
        {
            this._context = context;
        }

        private CampaignRepository _campaignRepository { get; set; }

        public CampaignRepository CampaignRepository
        {
            get
            {
                if (this._campaignRepository == null) this._campaignRepository = new CampaignRepository(this);
                return this._campaignRepository;
            }
        }

        ~UnitOfWork()
        {
            // Dispose
            Dispose();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
