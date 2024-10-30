using Duende.IdentityServer.Models;
using IdentityService.Data;
using IdentityService.Identityconfigs;
using IdentityService.Messaging;
using IdentityService.Messaging.RecieveMessage.AddUser;
using IdentityService.Messaging.RecieveMessage.DeleteUser;
using IdentityService.Messaging.RecieveMessage.UpdateUser;
using IdentityService.Models.Entities;
using IdentityService.SeedUserData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(Options =>
{
    Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(Options =>
    {
        Options.Authority = "https://localhost:7036";
        Options.Audience = "userServices";
    });

builder.Services.Configure<RabbitMqConfiguration>(
    builder.Configuration.GetSection("RabbitMQ")
);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")),ServiceLifetime.Singleton);

builder.Services.AddHostedService<UpdateUserinfo>();
builder.Services.AddHostedService<AddUserMessage>();
builder.Services.AddHostedService<DeleteUserMessage>();


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddErrorDescriber<CustomIdentityError>();




builder.Services.ConfigureApplicationCookie(options =>
{
    //نشان می دهد که بعد از 5 دقیقه به صورت خودکار از سیستم خارج شود.
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Account/Login/Index";
    options.AccessDeniedPath = "/AccessDenied/";
    options.SlidingExpiration = true;
});

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()

    .AddInMemoryClients(new List<Client> {
  
    new Client
    {
        ClientName = "web fronend code",
        ClientId = "webfrontendcode",
        ClientSecrets = {new Secret("123456".Sha256())},
        AllowedGrantTypes= GrantTypes.Code ,
        AllowedScopes = {"openid","profile", "order.getorder", "basket.fullAccess","apigatewayforweb.fullAccess","product.getproduct","roles"},
        RedirectUris = { "https://localhost:44306/signin-oidc" },
        PostLogoutRedirectUris = { "https://localhost:44306/signout-callback-oidc" },
        AllowOfflineAccess = true,
        RefreshTokenUsage = TokenUsage.ReUse,
        RefreshTokenExpiration = TokenExpiration.Sliding,
        AccessTokenLifetime = 60,

    },
    new Client
    {
        ClientName = "Admin fronend code",
        ClientId = "adminfrontendcode",
        ClientSecrets = {new Secret("123456".Sha256())},
        AllowedGrantTypes= GrantTypes.Code ,
        AllowedScopes = {"openid","profile", "product.getproduct", "apigatewayforadmin.fullAccesss","roles","user.fullaccess","homepage.edithomepage"
        ,"staticfile.uploadfile"},
        RedirectUris = { "https://localhost:44369/signin-oidc" },
        PostLogoutRedirectUris = { "https://localhost:44369/signout-callback-oidc" },
    }
    })
    .AddInMemoryIdentityResources(new List<IdentityResource> {
    new IdentityResources.OpenId(),
    new IdentityResources.Profile(),
    new IdentityResource("roles","User role(s)", new List<string>{"role"})

    })
    .AddInMemoryApiScopes(new List<ApiScope>
    {
        new ApiScope("orderservice.fullAccess"),
        new ApiScope("basket.fullAccess"),
        new ApiScope("order.getorder"),
        new ApiScope("order.OrderManagment"),
        new ApiScope("apigatewayforweb.fullAccess", new List<string>{"role"}),
        new ApiScope("apigatewayforadmin.fullAccesss", new List<string>{"role"}),
        new ApiScope("product.fullaccess"),
        new ApiScope("product.getproduct"),
        new ApiScope("user.fullaccess"),
        new ApiScope("homepage.edithomepage"),
        new ApiScope("staticfile.uploadfile")


    })
    .AddInMemoryApiResources(new List<ApiResource>
    {
        new ApiResource("orderService","Order Service Api")
        {
            Scopes = { "orderservice.fullAccess","order.OrderManagment","order.getorder" }
        },
        new ApiResource("basketService","Basket Service Api")
        {
            Scopes = { "basket.fullAccess" }
        },
        new ApiResource("Apigatewayforweb","ApiGateWay for web")
        {
            Scopes = { "apigatewayforweb.fullAccess" }
        },
         new ApiResource("ApigatewayforAdmin","ApiGateWay for admin")
        {
            Scopes = { "apigatewayforadmin.fullAccesss" }
        },
         new ApiResource("productservice","Product service ")
        {
            Scopes = { "product.fullaccess","product.getproduct" }
        },

         new ApiResource("userServices","User service ")
        {
            Scopes = { "user.fullaccess" }
        },

         new ApiResource("homepageservice","homePage edit service ")
        {
            Scopes = { "homepage.edithomepage" }
        },
         new ApiResource("staticfileservice","Static File Service"){Scopes = {"staticfile.uploadfile"} }

    })
    .AddAspNetIdentity<User>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    // Map API controllers
    endpoints.MapControllers();

    // Optionally, also map Razor Pages
    endpoints.MapRazorPages();
});

app.Run();
