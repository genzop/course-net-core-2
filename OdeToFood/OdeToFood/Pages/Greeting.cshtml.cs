using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Services;

namespace OdeToFood.Pages
{
    public class GreetingModel : PageModel
    {
        private IGreeter _greeter;

        public string CurrentGreeting { get; set; }
        
        // Inyecta el servicio IGreeter
        public GreetingModel(IGreeter greeter)
        {
            _greeter = greeter;
        }

        // OnGet se ejecuta antes de renderizar la page.
        // Recibe un parametro llamado name que luego utiliza en el mensaje
        public void OnGet(string name)
        {
            CurrentGreeting = $"{name}: {_greeter.GetMessageOfTheDay()}";            
        }
    }
}