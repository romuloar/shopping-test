using MMLib.SwaggerForOcelot.DependencyInjection;
using Newtonsoft.Json.Linq;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddOcelotWithSwaggerSupport(o =>
{
    o.Folder = "Routes";
});
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerForOcelotUI(o =>
    {
        o.PathToSwaggerGenerator = "/swagger/docs";
        o.ReConfigureUpstreamSwaggerJson = AlterUpstreamSwaggerJson;
    }).UseOcelot().Wait();
}

string AlterUpstreamSwaggerJson(HttpContext arg1, string arg2)
{
    var swagger = JObject.Parse(arg2);
    return swagger.ToString(Newtonsoft.Json.Formatting.Indented);
}

app.UseHttpsRedirection();
app.Run();

