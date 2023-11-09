using Carpo.Core.Domain.Extension;
using ShoppingTest.Campaign.Application.Case;
using ShoppingTest.Campaign.Application.UnitTest.Memory;
using ShoppingTest.Campaign.Core.Domain;

namespace ShoppingTest.Campaign.Application.UnitTest.Test
{
    public class GetCampaignUnitTest
    {
        [Fact]
        public async void GetCampaignTest()
        {
            var mem = new CampaignMemory();
            var get = new GetCampaignCase(mem);

            var id = "";
            var result = await get.Execute(id);
            Assert.False(result.IsSuccess);

            id = mem.ListCampaign.FirstOrDefault()?.Id;
            result = await get.Execute(id);
            Assert.True(result.IsSuccess);
        }
    }
}