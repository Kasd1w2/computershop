using Microsoft.EntityFrameworkCore;

namespace ComputeShopApi.Models
{
    public class ComputerShopDbContext : DbContext
    {
        public ComputerShopDbContext(DbContextOptions options) : base(options)
        {
        }

        public ComputerShopDbContext()
        {
        }

        public DbSet<Computer> Computers { get; set; }
        public DbSet<Osystem> Osystems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = "server=localhost;database=computer13bcsop2;user=root;password=";

            optionsBuilder.UseMySql(conn, ServerVersion.AutoDetect(conn));
        }
    }
}
