using APiGatWayWebApi.Models.Aggregators;
using APiGatWayWebApi.Models.Services.DiscountServices;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;
var webHostEnvironment = builder.Environment;
builder.Configuration.SetBasePath(webHostEnvironment.ContentRootPath)
    .AddJsonFile("ocelot.json")
    .AddOcelot(webHostEnvironment)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IDiscountServices, APiGatWayWebApi.Models.Services.DiscountServices.DiscountServices>();
builder.Services.AddOcelot(configuration)
    .AddPolly()
    .AddSingletonDefinedAggregator<ProductBasketAggregator>()
    .AddCacheManager(x =>
                {
                    x.WithDictionaryHandle();
                }); ;

var AuthenticationSchemeKey = "ApiGateWayForWebAuthenticationScheme";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(AuthenticationSchemeKey, Options =>
    {
        Options.Authority = "https://localhost:7036";
        Options.Audience = "Apigatewayforweb";
    });
var discountServiceUri = configuration["MicroServiceAddress:Discount:Uri"];

// Register the GrpcChannel with the DI container
builder.Services.AddSingleton(GrpcChannel.ForAddress(discountServiceUri));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.UseOcelot().Wait();

app.Run();
