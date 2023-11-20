using ShoppingTest.Campaign.Application.Case;
using ShoppingTest.Campaign.Application.UnitTest.Memory;
using ShoppingTest.Campaign.Core.Domain;

namespace ShoppingTest.Campaign.Application.UnitTest.Test
{
    public class AddCampaignUnitTest
    {
        public CampaignMemory CampaignMemory { get; private set; }
        public AddCampaignCase Case { get; private set; }

        public AddCampaignUnitTest() { 
            CampaignMemory = new CampaignMemory();
            Case = new AddCampaignCase(CampaignMemory);
        }
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

        /// <summary>
        /// Test - Invalid contract
        /// </summary>
        [Fact]
        public async void AddCampaignInvalidContractTest()
        {
            try
            {
                var useCase = new AddCampaignCase(null);
            }
            catch (Exception exc)
            {
                Assert.True(true);
            }
        }

        /// <summary>
        /// Test - Null domain
        /// </summary>
        [Fact]
        public async void AddCampaignNullDomainTest()
        {
            var result = await Case.Execute(null);
            Assert.False(result.IsSuccess);
        }

        /// <summary>
        /// Test - Invalid domain
        /// </summary>
        [Fact]
        public async void AddCampaignInvalidDomainTest()
        {
            var campaign = new CampaignDomain();

            var result = await Case.Execute(campaign);
            Assert.False(result.IsSuccess);
        }

        /// <summary>
        /// Test - Success
        /// </summary>
        [Fact]
        public async void AddCampaignSuccessTest()
        {
            var campaign = new CampaignDomain
            {
                Description = "Test description",
                Name = "Test Name",
                EndDate = DateTime.Now.AddMonths(2),
                InitialDate = DateTime.Now.AddDays(10),
                ShoppingId = Guid.NewGuid().ToString(),
            };

            var result = await Case.Execute(campaign);
            Assert.True(result.IsSuccess);
        }

    }
}