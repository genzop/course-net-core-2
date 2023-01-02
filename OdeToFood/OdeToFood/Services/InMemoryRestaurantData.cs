using OdeToFood.Models;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> _restaurants;

        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant{ Id = 1, Name = "Scott's Pizza Place" },
                new Restaurant{ Id = 2, Name = "Tersiguels" },
                new Restaurant{ Id = 3, Name = "King's Contrivance" }
            };
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants.OrderBy(x => x.Name);
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(x => x.Id == id);
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurant.Id = _restaurants.Max(x => x.Id) + 1;
            _restaurants.Add(restaurant);
            return _restaurants.Last();
        }

        public Restaurant Update(Restaurant restaurant)
        {
            throw new System.NotImplementedException();
        }
    }
}
