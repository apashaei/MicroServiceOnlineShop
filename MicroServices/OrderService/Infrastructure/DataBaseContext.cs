using Microsoft.EntityFrameworkCore;
using OrderSevice.Models.Entities;

namespace OrderSevice.Infrastructure
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options) { }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Orderline> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
