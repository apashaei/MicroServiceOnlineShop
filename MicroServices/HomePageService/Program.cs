using HomPageServices.Context;
using HomPageServices.Features.HomePage.Command;
using HomPageServices.Features.HomePage.Queries;
using HomPageServices.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHomePageServices, HomePageServices>();
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(AddHomePagePartItemsCommand).GetTypeInfo().Assembly));
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(GetHomePagePartsItemsQuery).GetTypeInfo().Assembly));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});


builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")), ServiceLifetime.Singleton);

builder.Services.AddSingleton<IGetImageSrc, GetImageSrc>();
builder.Services.AddAuthentication(Options =>
{
    Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(Options =>
    {
        Options.Authority = "https://localhost:7036";
        Options.Audience = "homepageservice";
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
