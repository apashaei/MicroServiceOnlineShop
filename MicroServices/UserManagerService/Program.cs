using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;

using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;
using UserManagerService.MessagingBus.SendMessage;
using UserManagerService.Models.DBContext;
using UserManagerService.Models.Entities;
using UserManagerService.Models.IdentityConfigs;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMetrics();
        builder.Services.AddMetricsTrackingMiddleware();

        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration) // Reads settings from appsettings.json
            .Enrich.FromLogContext()
            .CreateLogger();


        builder.Host.UseSerilog((context, services, config) =>
        {
            config
            .MinimumLevel.Information()
            .Enrich.WithMachineName()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(
                new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearch:Uri"] ??
                                                        "https://localhost:9200"))
                {
                    IndexFormat = "AppLogs",
                    AutoRegisterTemplate = true,
                    DetectElasticsearchVersion = true,
                    FailureCallback = (logEvent, ex) =>
                Console.WriteLine($"Unable to submit event: {logEvent.RenderMessage()} - Exception: {ex.Message}"),
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
            EmitEventFailureHandling.WriteToFailureSink |
            EmitEventFailureHandling.RaiseCallback,
                    FailureSink = new FileSink($"./fail-{DateTime.UtcNow:yyyyMM}.txt",
    new JsonFormatter(), null, null),
                    ModifyConnectionSettings = x =>
                x.BasicAuthentication(context.Configuration["ElasticSearch:User"],
                    context.Configuration["ElasticSearch:Password"])


                }).Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
.ReadFrom.Configuration(context.Configuration);
        });






        //builder.Host.UseMetrics(Options =>
        //{
        //    Options.EndpointOptions = endpointoptions =>
        //    {
        //        endpointoptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
        //        endpointoptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
        //        endpointoptions.EnvironmentInfoEndpointEnabled = false;

        //    };
        //});

        builder.Services.AddHealthChecks()
            .AddSqlServer("Server=Ali\\SQLEXPRESS;Database=UserServiceDb;Trusted_Connection=True;TrustServerCertificate=True;")
            .AddRabbitMQ(rabbitConnectionString: "amqp://guest:guest@localhost:5672", tags: new string[] { "rabbitmq" });

        //builder.Services.AddHealthChecksUI(p => p.AddHealthCheckEndpoint("UserManagementHealthCkeck", "/health"))
        //    .AddInMemoryStorage();

        builder.Services.Configure<RabbitMqConfiguration>(
            builder.Configuration.GetSection("RabbitMQ")
        );
        builder.Services.AddScoped<IMessageBus, RabbitMqMessageBus>();

        builder.Services.AddDbContext<DataBaseContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));


        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<DataBaseContext>()
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




        var app = builder.Build();

        //using (var scope = app.Services.CreateScope())
        //{
        //    var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
        //    db.Database.Migrate();
        //}

        app.UseMetricsAllMiddleware();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });

        //app.UseHealthChecksUI(delegate (Options options)
        //{
        //    options.UIPath = "/healthui";
        //    options.ApiPath = "/healthuiapi";
        //});
        app.UseHttpsRedirection();
        //app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();
        //app.MapHealthChecks("/health");

        app.Run();
    }
}