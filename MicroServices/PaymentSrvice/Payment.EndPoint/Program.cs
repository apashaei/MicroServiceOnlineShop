using Application.Interfaces;
using Application.PaymentSRVC;
using Infrastructure.Messaging.Configs;
using Infrastructure.Messaging.RecieveMessage.RecievePaymentMessage;
using Infrastructure.Messaging.SendMessage.SendPaDoneMessage;
using Microsoft.EntityFrameworkCore;
using Persistance.DataBaseContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")),ServiceLifetime.Singleton);

builder.Services.AddTransient<IPaymentServices,PaymentServices>();

builder.Services.AddTransient<IDatabaseContext, DataBaseContext>();
builder.Services.AddTransient<IMessageBus,RabbitMQMassageBus>();

builder.Services.AddHostedService<PayOrderMessage>();
builder.Services.Configure<RabbitMQConfig>(
    builder.Configuration.GetSection("RabbitMQ")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
