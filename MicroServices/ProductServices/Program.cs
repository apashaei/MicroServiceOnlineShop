using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using ProductServices.MessingBus;
using ProductServices.MessingBus.RecieveMessage;
using ProductServices.Models.Context;
using ProductServices.Services.CategoryServices;
using ProductServices.Services.ProductServices;
using ProductServices.MessingBus.SendMessage;
using Serilog;
using ProductServices.Services.BransServices;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using ProductServices.Middlewares;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMqConfiguration>(
    builder.Configuration.GetSection("RabbitMqConfig")
);

builder.Services.AddTransient<IMessageBus, RabbitMqMessageBus>();

builder.Services.AddHealthChecks()
    .AddSqlServer("Server=Ali\\SQLEXPRESS;Database=ProductMicroServices;Trusted_Connection=True;TrustServerCertificate=True;")
    .AddRabbitMQ(rabbitConnectionString: "amqp://guest:guest@localhost:5672", tags: new string[] { "rabbitmq" });


builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    options.AddInterceptors();
    },ServiceLifetime.Singleton);

builder.Services.AddMetrics();
builder.Services.AddMetricsTrackingMiddleware();
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Host.UseMetrics(Options =>
{
    Options.EndpointOptions = endpointoptions =>
    {
        endpointoptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
        endpointoptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
        endpointoptions.EnvironmentInfoEndpointEnabled = false;
    };
});


builder.Services.AddTransient<ICategoryServices, CategoryServices>();
builder.Services.AddTransient<IProductServices, ProductService>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddHostedService<UpdateProductSellNumber>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});


builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection("RedisConfig"));

builder.Services.AddAuthentication(Options =>
{
    Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(Options =>
    {
        Options.Authority = "https://localhost:44312";
        Options.Audience = "productservice";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ProductManagmentPolicy", policy =>
    {
        policy.RequireClaim("scope", "product.fullaccess");
    });
    options.AddPolicy("GetProductPolicy", policy =>
    {
        policy.RequireClaim("scope", "product.getproduct");

    });
});

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Reads settings from appsettings.json
    .Enrich.FromLogContext() // Enriches logs with contextual information
    .CreateLogger();

builder.Host.UseSerilog();


var app = builder.Build();

app.UseMetricsAllMiddleware();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
    db.Database.Migrate();
}

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCustomException();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
