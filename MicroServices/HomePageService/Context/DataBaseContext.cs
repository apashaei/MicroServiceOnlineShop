using HomPageServices.Attributes;
using HomPageServices.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomPageServices.Context
{
   public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options) { }
        public DbSet<HomePageParts> HomePageParts { get; set; }

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


            modelBuilder.Entity<HomePageParts>().HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var modefiedEntries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified ||
                p.State == EntityState.Added ||
                p.State == EntityState.Deleted);
            foreach (var entry in modefiedEntries)
            {
                var entityType = entry.Context.Model.FindEntityType(entry.Entity.GetType());
                if (entityType != null)
                {
                    var inserted = entityType.FindProperty("InsertTime");
                    var updated = entityType.FindProperty("UpdateTime");
                    var removeTime = entityType.FindProperty("RemoveTime");
                    var removed = entityType.FindProperty("IsRemoved");
                    if (entry.State == EntityState.Added && inserted != null)
                    {
                        entry.Property("InsertTime").CurrentValue = DateTime.Now;
                    }
                    if (entry.State == EntityState.Modified && updated != null)
                    {
                        entry.Property("UpdateTime").CurrentValue = DateTime.Now;
                    }
                    if (entry.State == EntityState.Deleted && removed != null && removeTime != null)
                    {
                        entry.Property("RemoveTime").CurrentValue = DateTime.Now;
                        entry.Property("IsRemoved").CurrentValue = true;
                        entry.State = EntityState.Modified;
                    }
                }
            }
            return base.SaveChanges();
        }
    }

    
}
