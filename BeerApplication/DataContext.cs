using Microsoft.EntityFrameworkCore;

namespace BeerApplication
{
    public class DataContext : DbContext
    {
        public DbSet<Beer> Beers => Set<Beer>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=BeerApplicationDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
