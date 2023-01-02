using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {        
        private IConfiguration _configuration;

        // Inyecta IConfiguration para poder obtener el connection string configurado en appsettings.json
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Registra diferentes servicios al container que luego podran ser injectados 
        public void ConfigureServices(IServiceCollection services)
        {
            // Registra un servicio de autenticacion
            services.AddAuthentication(options =>
            {
                // Especifica el esquema de autenticacion por defecto, en este caso el de OpenId
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

                // Especifica el nombre del esquema por defecto, en este caso Cookies
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;                
            })  
            .AddOpenIdConnect(options => 
            {
                // Especifica con que proveedor de identidad se va a trabajar, cargando los datos del appsettings
                _configuration.Bind("AzureAd", options);
            })
            .AddCookie();            

            // Registra la interface IGreeter y su implementacion Greeter como un Singleton, es decir que solo va a haber una instancia de su implementacion durante la vida de la aplicacion.            
            services.AddSingleton<IGreeter, Greeter>();

            // Registra el DbContext utilizando SqlServer
            services.AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("OdeToFood")));

            // Registra la interface IRestaurantData con su implementacion SqlRestaurantData como scoped, es decir que va a crear una instancia de ese objeto por request y reutilizar esa instancia en ese request.            
            services.AddScoped<IRestaurantData, SqlRestaurantData>();

            // Registra el framework MVC
            services.AddMvc();
        }

        // Define que middlewares van a intervenir en la respuesta a cada mensaje HTTP
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IGreeter greeter, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                // Si es ambiente de desarrollo, intercepta todas las excepciones que ocurran durante la ejecucion del pipeline y lo muestra en una pantalla descriptiva
                app.UseDeveloperExceptionPage();
            }

            // Redirecciona todos los request que vengan por HTTP a HTTPS
            app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());

            #region DefaultFiles Middleware
            // Establece como pagina por defecto a archivos con nombres estandard como index.html
            /* app.UseDefaultFiles(); */
            #endregion

            // Permite acceso a los archivos que se encuentran en la carpeta wwwroot
            app.UseStaticFiles();

            // Permite acceso a los archivos en node_modules
            app.UseNodeModules(env.ContentRootPath);

            #region FilesServer Middleware
            // Cumple con la funcion de DefaultFiles y StaticFiles
            /* app.UseFileServer(); */
            #endregion

            #region Custom Middleware
            // Crea un middleware que utiliza la interface ILogger para escribir logs a la consola
            /*
            app.Use(next =>
            {
                return async context =>
                {
                    logger.LogInformation("Request incoming");

                    // Si la ruta comienza con "/custom"
                    if (context.Request.Path.StartsWithSegments("/custom"))
                    {
                        // Escribe en la respuesta un mensaje
                        await context.Response.WriteAsync("Hit!!");
                        logger.LogInformation("Request handled");
                    }
                    else
                    {
                        // Deja que el request siga por el pipeline
                        await next(context);
                        logger.LogInformation("Request outgoing");
                    }
                };
            });
            */
            #endregion

            #region UseWelcomePage Middleware
            // Agrega el middleware UseWelcomePage al pipeline      
            /*
            app.UseWelcomePage(new WelcomePageOptions
            {
                Path = "/welcome"
            });
            */
            #endregion

            // Habilita la autenticacion 
            app.UseAuthentication();

            // Configura MVC y las rutas
            app.UseMvc(ConfigureRoutes);
        }

        // Configura como se mapearan las rutas de la url a los actions en los controllers
        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // Ruta por defecto, con Home y Index como controller y action por defecto. El id es opcional.
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
