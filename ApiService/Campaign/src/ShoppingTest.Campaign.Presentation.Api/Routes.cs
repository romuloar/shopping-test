using Microsoft.AspNetCore.Mvc;
using ShoppingTest.Campaign.Core.Domain;
using ShoppingTest.Campaign.Core.Interface;

namespace ShoppingTest.Campaign.Presentation.Api
{
    public static class Routes
    {
        public static void Map(WebApplication app) {
            app.MapGet("campaign/{id}", async ([FromServices] IGetCampaign contract, string id) => await contract.GetCampaign(id));
            app.MapPost("campaign/add", async ([FromServices] IAddCampaign contract, CampaignDomain campaign) => await contract.AddCampaign(campaign));
            
        }
    }
}
