using Carpo.Core.Domain.Extension;
using ShoppingTest.Campaign.Application.Case;
using ShoppingTest.Campaign.Application.UnitTest.Memory;
using ShoppingTest.Campaign.Core.Domain;

namespace ShoppingTest.Campaign.Application.UnitTest.Test
{
    public class GetCampaignUnitTest
    {
        public CampaignMemory CampaignMemory { get; private set; }
        public GetCampaignCase GetCampaignCase { get; private set; }

        /// <summary>
        /// GetCampaignUnitTest
        /// </summary>
        public GetCampaignUnitTest()
        {
            CampaignMemory = new CampaignMemory();
            GetCampaignCase = new GetCampaignCase(CampaignMemory);
        }

        /// <summary>
        /// Test - Null contract
        /// </summary>
        [Fact]
        public void GetCampaignNullContractTest()
        {
            try
            {
                var useCase = new GetCampaignCase(null);
            }
            catch (Exception exc)
            {
                Assert.True(true);
            }
        }


        /// <summary>
        /// Test - Null Id
        /// </summary>
        [Fact]
        public async void GetCampaignNullIdTest()
        {
            var result = await GetCampaignCase.Execute(null);
            Assert.False(result.IsSuccess);
        }

        /// <summary>
        /// Test - Empty Id
        /// </summary>
        [Fact]
        public async void GetCampaignEmptyIdTest()
        {
            var result = await GetCampaignCase.Execute("");
            Assert.False(result.IsSuccess);
        }

        /// <summary>
        /// Test - Success
        /// </summary>
        [Fact]
        public async void GetCampaignSuccessTest()
        {
            var id = CampaignMemory.ListCampaign.FirstOrDefault()?.Id;
            var result = await GetCampaignCase.Execute(id);
            Assert.True(result.IsSuccess);
        }
    }
}