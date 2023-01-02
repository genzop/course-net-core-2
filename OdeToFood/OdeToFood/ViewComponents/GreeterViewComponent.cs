using Microsoft.AspNetCore.Mvc;
using OdeToFood.Services;

namespace OdeToFood.ViewComponents
{
    // Crea un ViewComponent
    public class GreeterViewComponent : ViewComponent
    {
        private IGreeter _greeter;

        // Inyecta el servicio IGreeter
        public GreeterViewComponent(IGreeter greeter)
        {
            _greeter = greeter;
        }

        // Envia el modelo al ViewComponent
        public IViewComponentResult Invoke()
        {
            var model = _greeter.GetMessageOfTheDay();

            // Envia al view un string por lo que es necesario especificar el nombre del view.
            // Default es el nombre por defecto de un ViewComponent.
            return View("Default", model); 
        }
    }
}