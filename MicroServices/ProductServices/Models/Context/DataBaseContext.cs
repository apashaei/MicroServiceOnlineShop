using Microsoft.EntityFrameworkCore;
using ProductServices.Attributes;
using ProductServices.Models.Entities;
using ProductServices.Models.EntityConfiguration;
using ProductServices.Models.Seed.DataBaseContextSeed;
using StackExchange.Redis;

namespace ProductServices.Models.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        private DbSet<Category> Categories { get; set; }
        public DbSet<CategoryComponent> CategoryComponents { get; set; }
        private DbSet<CategoryItem> Items { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<ProductFeatures> ProductFeatures { get; set; }
        public DbSet<Brand> Brands { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("InsertTime").HasDefaultValue(DateTime.Now);
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("UpdateTime");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("RemoveTime");
                    modelBuilder.Entity(entityType.Name).Property<bool>("IsRemoved").HasDefaultValue(false);
                }
            }
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new BrandConfig());
            modelBuilder.Entity<Product>().HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);
            modelBuilder.Entity<CategoryComponent>().HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);
            modelBuilder.Entity<Images>().HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);
            modelBuilder.Entity<ProductFeatures>().HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);


            modelBuilder.Entity<CategoryComponent>()
           .HasOne<CategoryComponent>() // Self-reference
           .WithMany() // One parent can have many children
           .HasForeignKey(c => c.CategoryId)
           .OnDelete(DeleteBehavior.Restrict); // Disable cascading delete

            DataBaseContextSeed.BrandSeeds(modelBuilder);


            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
