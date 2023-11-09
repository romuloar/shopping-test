using ShoppingTest.Campaign.Application.Case;
using ShoppingTest.Campaign.Application.UnitTest.Memory;
using ShoppingTest.Campaign.Core.Domain;

namespace ShoppingTest.Campaign.Application.UnitTest.Test
{
    public class AddCampaignUnitTest
    {
        [Fact]
        public async void AddCampaignTest()
        {
            var mem = new CampaignMemory();
            var add = new AddCampaignCase(mem);

            var result = await add.Execute(null);
            Assert.False(result.IsSuccess);
            var campaign = new CampaignDomain();

            result = await add.Execute(campaign);
            Assert.False(result.IsSuccess);

            campaign = new CampaignDomain
            {
                Description = "Test description",
                Name = "Test Name",
                EndDate = DateTime.Now.AddMonths(2),
                InitialDate = DateTime.Now.AddDays(10)
            };

            result = await add.Execute(campaign);
            Assert.True(result.IsSuccess);
        }
    }
}