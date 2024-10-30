using Grpc.Net.Client;
using Microservice.Web.FronEnd.MiddleWares;
using Microservice.Web.FronEnd.Services.BasketServices;
using Microservice.Web.FronEnd.Services.DiscountServices;
using Microservice.Web.FronEnd.Services.HomePage;
using Microservice.Web.FronEnd.Services.OrderServices;
using Microservice.Web.FronEnd.Services.PaymentServices;
using Microservice.Web.FronEnd.Services.ProductServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
string productServiceUri = configuration["MicroServiceAddress:APiGateWay:Uri"];
string basketServiceUri = configuration["MicroServiceAddress:APiGateWay:Uri"];
string ordertServiceUri = configuration["MicroServiceAddress:APiGateWay:Uri"];
string paymentServiceUri = configuration["MicroServiceAddress:APiGateWay:Uri"];
string dsicountServiceRestFullApi = configuration["MicroServiceAddress:APiGateWay:Uri"];

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IproductSevice, productSevice>(p =>
{

    p.BaseAddress = new Uri(productServiceUri);
    p.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}).AddUserAccessTokenHandler();



builder.Services.AddHttpClient<IDiscountServicesRestfull, DiscountServicesRestfull>(p =>
{

    p.BaseAddress = new Uri(dsicountServiceRestFullApi);
    p.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}

).AddUserAccessTokenHandler();


builder.Services.AddHttpClient<IBasketServices, BasketServices>(p =>
{

    p.BaseAddress = new Uri(basketServiceUri);
    p.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}

).AddUserAccessTokenHandler();

builder.Services.AddHttpClient<IHomePageServices, HomePageServices>(p =>
{

    p.BaseAddress = new Uri(basketServiceUri);
    p.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}

);

builder.Services.AddHttpClient<IOrderServices, OrderServices>(p =>
p.BaseAddress = new Uri(ordertServiceUri)).AddUserAccessTokenHandler();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IPaymentServices>(p =>
{
    return new PaymentServices(new RestSharp.RestClientOptions(paymentServiceUri));
});

builder.Services.AddScoped<IDiscountServices, DiscountService>();
builder.Services.AddAccessTokenManagement();

var discountServiceUri = configuration["MicroServiceAddress:APiGateWay:Uri"];

// Register the GrpcChannel with the DI container
builder.Services.AddSingleton(GrpcChannel.ForAddress(discountServiceUri));
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = "https://localhost:7036";
    options.ClientId = "webfrontendcode";
    options.ClientSecret = "123456";
    options.ResponseType = "code";
    options.GetClaimsFromUserInfoEndpoint = true;
    options.SaveTokens = true;
    //options.Scope.Add("profile");
    //options.Scope.Add("openid");
    options.Scope.Add("order.getorder");
    options.Scope.Add("basket.fullAccess");
    options.Scope.Add("apigatewayforweb.fullAccess");
    options.Scope.Add("product.getproduct");
    options.Scope.Add("offline_access");

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseExceptionHandlerMiddleWare();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
