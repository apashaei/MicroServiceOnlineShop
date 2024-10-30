using Microsoft.EntityFrameworkCore;
using ProductServices.Models.Entities;

namespace ProductServices.Models.Seed.DataBaseContextSeed
{
    public static class DataBaseContextSeed
    {
        public static void BrandSeeds(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(BrandData());
        }
        private static List<Brand> BrandData()
        {
            var brands = new List<Brand>()
            {
                new Brand {Id=Guid.NewGuid(),Description="محصولات Apple",Name="Apple"},
                new Brand {Id=Guid.NewGuid(),Description="محصولات Sumsung", Name="Sumsung" },
                new Brand {Id=Guid.NewGuid(),Description="محصولات Nike" , Name="Nike" }
            };
                return brands;
        }
    }
}
