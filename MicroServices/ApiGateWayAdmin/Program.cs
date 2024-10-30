using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
var webHostEnvironment = builder.Environment;
builder.Configuration.SetBasePath(webHostEnvironment.ContentRootPath)
    .AddJsonFile("ocelot.json")
    .AddOcelot(webHostEnvironment)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(configuration)
    .AddPolly();

var AuthenticationSchemeKey = "ApiGateWayForAdminAuthenticationScheme";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(AuthenticationSchemeKey, Options =>
    {
        Options.Authority = "https://localhost:44312";
        Options.Audience = "ApigatewayforAdmin";
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.UseOcelot().Wait();

app.Run();
