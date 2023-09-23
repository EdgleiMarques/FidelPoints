using Microsoft.EntityFrameworkCore;

namespace FidelPoints.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Seller> Sellers { get; set; }
        public  DbSet<Product> Products { get; set; }
        public  DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public  DbSet<Car> Cars { get; set; }



    }
}
