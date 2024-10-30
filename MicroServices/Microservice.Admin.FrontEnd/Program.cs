using FluentValidation.AspNetCore;
using HealthChecks.UI.Configuration;
using Microservice.Admin.FrontEnd.Messaging.Configs;
using Microservice.Admin.FrontEnd.Messaging.SendMessage.ProductNameMessage;
using Microservice.Admin.FrontEnd.MiddleWares;
using Microservice.Admin.FrontEnd.Models.ViewServices.HomePage;
using Microservice.Admin.FrontEnd.Models.ViewServices.Poduct;
using Microservice.Admin.FrontEnd.Models.ViewServices.StaticFile;
using Microservice.Admin.FrontEnd.Models.ViewServices.User;
using Microservice.Admin.FrontEnd.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHealthChecksUI(p => p.AddHealthCheckEndpoint("UserManagementHealthCkeck", "https://localhost:44369/health"))
    .AddInMemoryStorage();

builder.Services.AddHealthChecksUI(p => p.AddHealthCheckEndpoint("productHealthCkeck", "https://localhost:44345/health"))
    .AddInMemoryStorage();

builder.Services.AddHealthChecksUI(p => p.AddHealthCheckEndpoint("orderHealthCkeck", "https://localhost:44361/health"))
    .AddInMemoryStorage();



var configuration = builder.Configuration;
string productServiceUri = configuration["MicroServiceAddress:APiGateWay:Uri"];
string userServiceUri = configuration["MicroServiceAddress:APiGateWay:Uri"];

//builder.Services.AddScoped<IProductServicecs>(p =>
//{
//    return new ProductServicecs(new RestSharp.RestClientOptions(productServiceUri), new HttpContextAccessor());
//});

builder.Services.AddHttpClient<IProductService, ProductService>(p =>
{

    p.BaseAddress = new Uri(productServiceUri);
    p.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}).AddUserAccessTokenHandler();

builder.Services.AddHttpClient<IUserServices, UserServices>(p =>
{

    p.BaseAddress = new Uri(userServiceUri);
    p.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}).AddUserAccessTokenHandler();


builder.Services.AddHttpClient<IStaticFileServices, StaticFileServices>(p =>
{
    p.BaseAddress = new Uri(userServiceUri);
    p.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}).AddUserAccessTokenHandler();


builder.Services.AddHttpClient<IHomePageServices, HomePageServices>(p =>
{

    p.BaseAddress = new Uri(userServiceUri);
    p.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}).AddUserAccessTokenHandler();

builder.Services.AddAccessTokenManagement();

builder.Services.Configure<RabbitMqConfigurations>(
    builder.Configuration.GetSection("RabbitMQ")
);

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IMessageBus, RabbitMQMessageBus>();


builder.Services.AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<CreateUserValidation>());


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.SlidingExpiration = true;
})
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = "https://localhost:44312";
    options.ClientId = "adminfrontendcode";
    options.ClientSecret = "123456";
    options.ResponseType = "code";
    options.GetClaimsFromUserInfoEndpoint = true;
    options.SaveTokens = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("product.getproduct");
    options.Scope.Add("apigatewayforadmin.fullAccesss");
    options.Scope.Add("roles");
    options.Scope.Add("user.fullaccess");
    options.Scope.Add("homepage.edithomepage");
    options.Scope.Add("staticfile.uploadfile");
    options.ClaimActions.MapUniqueJsonKey("role", "role");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "name",
        RoleClaimType = "role"
    };

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecksUI(delegate (Options options)
{
    options.UIPath = "/healthui";
    options.ApiPath = "/healthuiapi";
});

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
