using BasketServices.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BasketServices.Infrastructure
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options) { }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItems> Items { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
