namespace DealershipAPI.Extensions
{
    public static class ServiceExtensions
    {
        // Extension para la interfaz IServiceCollection, para hacer más limpia la implementación del CORS en el Program.cs
        public static void ConfigureCors(this IServiceCollection services)
        {
            //Se agrega el CORS a la aplicación
            services.AddCors(options =>
            {
                //Se agrega una política llamada CorsPolicy
                options.AddPolicy("CorsPolicy", builder =>
                {
                    //Para cualquier origen, se aceptan solicitudes de cualquier método y con cualquier cabecera
                    //En producción, esto debe ser lo más restrictivo posible
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }

    }
}
