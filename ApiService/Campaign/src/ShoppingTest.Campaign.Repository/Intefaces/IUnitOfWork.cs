namespace ShoppingTest.Campaign.Repository.Intefaces
{
    public interface IUnitOfWork
    {
        public ICampaignContext Context { get; }
    }
}
