using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    // Verifica que el usuario este autenticado
    [Authorize]
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        // Permite el acceso a un usuario no autenticado
        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
                Restaurants = _restaurantData.GetAll(),
                CurrentMessage = _greeter.GetMessageOfTheDay()
            };

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);
            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // Valida que el formulario generado para el cliente haya sido creado por la aplicacion
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditModel model)
        {
            // Valida todas las meta-etiquetas especificadas en el modelo
            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant
                {
                    Name = model.Name,
                    Cuisine = model.Cuisine
                };

                newRestaurant = _restaurantData.Add(newRestaurant);

                // Redirecciona al usuario al detalle de ese restaurant
                return RedirectToAction("Details", new { id = newRestaurant.Id });
            }
            else
            {
                // Muestra al usuario los mensajes con las validaciones que no se cumplieron
                return View();
            }
        }
    }
}
