using Microsoft.EntityFrameworkCore;
using OdeToFood.Data;
using OdeToFood.Models;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private OdeToFoodDbContext _context;

        // Inyecta el DbContext para poder acceder a la base de datos
        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants.OrderBy(x => x.Name);
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(x => x.Id == id);
        }

        public Restaurant Add(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return restaurant;
        }

        public Restaurant Update(Restaurant restaurant)
        {
            // Avisa a EntityFramework que esta entidad fue modificada cambiando su estado
            _context.Attach(restaurant).State = EntityState.Modified;

            // Guarda los cambios
            _context.SaveChanges();
            return restaurant;
        }
    }
}