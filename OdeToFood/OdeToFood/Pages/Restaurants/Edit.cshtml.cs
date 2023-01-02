using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Models;
using OdeToFood.Services;

namespace OdeToFood.Pages.Restaurants
{
    [Authorize]
    public class EditModel : PageModel
    {
        private IRestaurantData _restaurantData;

        // Vincula la propiedad para que sea actualizada cuando se envie el formulario
        [BindProperty]
        public Restaurant Restaurant { get; set; }

        // Inyecta el servicio IRestaurantData
        public EditModel(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        // Metodo que se ejecuta cuando se envie un GET 
        public IActionResult OnGet(int id)
        {
            Restaurant = _restaurantData.Get(id);
            if(Restaurant == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return Page();
        }

        // Metodo que se ejecuta cuando se envie un POST
        public IActionResult OnPost()
        {
            // Valida que se respeten las reglas establecidas en el modelo
            if (ModelState.IsValid)
            {
                _restaurantData.Update(Restaurant);
                return RedirectToAction("Details", "Home", new { id = Restaurant.Id });
            }

            return Page();
        }
    }
}