using ShoppingTest.Campaign.Core.Interface;
using ShoppingTest.Campaign.Presentation.Api;
using ShoppingTest.Campaign.Repository;
using ShoppingTest.Campaign.Repository.Context;
using ShoppingTest.Campaign.Repository.Intefaces;
using ShoppingTest.Campaign.Repository.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICampaignContext, CampaignContext>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAddCampaign, CampaignRepository>();
builder.Services.AddTransient<IGetCampaign, CampaignRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Routes.Map(app);

app.UseHttpsRedirection();

app.Run();