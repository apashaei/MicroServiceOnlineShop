using BasketServices.Infrastructure;
using BasketServices.Infrastructure.MappinProfiles;
using BasketServices.MessagingBus;
using BasketServices.MessagingBus.RecieveMessage.UpdateProductNameMessage;
using BasketServices.Services;
using BasketServices.Services.DiscountServices;
using BasketServices.Services.Product;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")),ServiceLifetime.Singleton);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBasketServices, BasketServices.Services.BasketServices>();
builder.Services.AddScoped<IDiscountServices, BasketServices.Services.DiscountServices.DiscountServices>();
builder.Services.Configure<RabbitMqConfiguration>(
    builder.Configuration.GetSection("RabbitMQ")
);

builder.Services.AddAutoMapper(typeof(BasketitemMapper));
builder.Services.AddScoped<IMassageBus, RabbitMQMessageBus>();

var configuration = builder.Configuration;
var discountServiceUri = configuration["MicroServiceAddress:Discount:Uri"];

// Register the GrpcChannel with the DI container
builder.Services.AddSingleton(GrpcChannel.ForAddress(discountServiceUri));
builder.Services.AddTransient<IProductServices, ProductServices>();
builder.Services.AddHostedService<UpdateProducName>();


builder.Services.AddAuthentication(Options =>
{
    Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(Options =>
    {
        Options.Authority = "https://localhost:7036";
        Options.Audience = "basketService";
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
