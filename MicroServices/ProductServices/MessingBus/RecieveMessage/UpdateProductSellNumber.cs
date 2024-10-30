
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading.Channels;
using StackExchange.Redis;
using System.Text.Json;
using ProductServices.Services.ProductServices;

namespace ProductServices.MessingBus.RecieveMessage
{
    public class UpdateProductSellNumber : BackgroundService
    {

        private readonly string ConnectionString;
        private readonly string channel_Name;
        private readonly ConnectionMultiplexer connection;
        private readonly IProductServices productServices;

        public UpdateProductSellNumber(IOptions<RedisConfig> options, IProductServices productServices)
        {
            ConnectionString = options.Value.ConnectionString;
            channel_Name = options.Value.channel_Name;
            connection = ConnectionMultiplexer.Connect(ConnectionString);
            this.productServices = productServices;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = connection.GetSubscriber();
            await subscriber.SubscribeAsync(channel_Name, (channel, message) =>
            {
                var productIs = JsonSerializer.Deserialize<UpdateSellNumberMessafgeDto>(message);
                productServices.UpdateProductsSellNumber(productIs);
            });

        }
    }

    public class UpdateSellNumberMessafgeDto 
    {
        public List<Guid> ProductIds { get; set; }
    }
}
