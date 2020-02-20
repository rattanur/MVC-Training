using System.Linq;
using Microsoft.EntityFrameworkCore;
using SCGEX_TRAINING.Models;

namespace SCGEX_TRAINING.Data
{
    public class AppDb : DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<Company> Companies { get; set; }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slot> Slot { get; set; }

        // Config Connect Database on Startup -> appsettings
        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {
            //
        }

        // Config Connect Database on Appdb

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                //.UseLazyLoadingProxies() // Lazy Loading
                .UseSqlServer("Server=103.55.1.123,1433;Database=SCGEX_TRAINING;User ID=sa;Password=TIDCcl0ud; " +
                "Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Machine>()
                .Property(x => x.AcceptableCoins)
                .HasConversion(
                    x => string.Join(',', x),
                    x => x.Split(new char[] { ',' })
                            .Select(y => decimal.Parse(y))
                            .ToList()
                );
        }
    }
}