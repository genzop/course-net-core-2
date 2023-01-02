using Microsoft.EntityFrameworkCore;
using OdeToFood.Models;

namespace OdeToFood.Data
{
    // Punto de acceso a la base de datos
    public class OdeToFoodDbContext : DbContext
    {        
        // Debe haber un DbSet por cada Tabla en la base de datos
        public DbSet<Restaurant> Restaurants { get; set; }

        public OdeToFoodDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
