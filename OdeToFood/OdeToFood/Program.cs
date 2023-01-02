using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OdeToFood
{    
    public class Program
    {
        // Este es el punto de partida de la aplicacion, al igual que una aplicacion de consola
        public static void Main(string[] args)
        {
            // Crea un IWebHost y lo ejecuta
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                // Especifica que se usara la clase Startup para configurar como se comporta la aplicacion
                .UseStartup<Startup>()
                // Crea el IWebHost donde se ejecutara la aplicacion
                .Build();
    }
}
