using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderSevice.Infrastructure;
using OrderSevice.MessagingBus;
using OrderSevice.MessagingBus.RecievedMessage;
using OrderSevice.MessagingBus.SendMessage;
using OrderSevice.Services;
using OrderSevice.Services.Product;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString: "Server=Ali\\SQLEXPRESS;Database=OrderMicroServices;Trusted_Connection=True;TrustServerCertificate=True;")
    .AddRabbitMQ(rabbitConnectionString: "amqp://guest:guest@localhost:5672", tags: new string[] { "rabbitmq" });

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")),ServiceLifetime.Singleton);

builder.Services.AddTransient<IOrderServices, OrderServices>();

builder.Services.Configure<RabbotMQConfig>(
    builder.Configuration.GetSection("RabbitMQ")
);

builder.Services.AddHostedService<CreateOrderRecieveMessage>();
builder.Services.AddHostedService<PayDoneInfoMessagecs>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection("RedisConfig"));


builder.Services.AddTransient<RabbitMQSendMessage>();
builder.Services.AddTransient<RedisSendMessage>();
builder.Services.AddTransient<IMassageBus,RabbitMQSendMessage>();

builder.Services.AddHostedService<UpdateProductNameMessage>();

builder.Services.AddAuthentication(Options =>
{
    Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(Options =>
    {
        Options.Authority = "https://localhost:7036";
        Options.Audience = "orderService";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("GetOrderPolicy", policy =>
    {
        policy.RequireClaim("scope", "order.getorder");
    });
});
var app = builder.Build();
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

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
