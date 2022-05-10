global using Microsoft.EntityFrameworkCore;
global using ProductsRestApi.Models;

namespace ProductsRestApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}

