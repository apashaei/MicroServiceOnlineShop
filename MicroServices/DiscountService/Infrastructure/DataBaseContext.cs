using DiscountServices.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DiscountServices.Infrastructure
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        public DbSet<Discount> Discounts { get; set; }
        
}
}
