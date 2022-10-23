using Microsoft.EntityFrameworkCore;
using Repository.Entity;

namespace Repository
{
    public class CurrencyExchangeContext : DbContext
    {
        public CurrencyExchangeContext()
        {

        }
        public CurrencyExchangeContext(DbContextOptions<CurrencyExchangeContext> options):base(options)
        {

        }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseSqlServer("server=PUNCK2021;Database=CurrencyExchange;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(opt =>
            {
                opt.HasKey(x => x.Id);

                opt.Property(x => x.IsoCode).HasMaxLength(5);
                opt.Property(x => x.Name).HasMaxLength(70);
            });
        }
    }
}
