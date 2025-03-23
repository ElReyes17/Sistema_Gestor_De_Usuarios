using Sistema_Gestor_De_Usuarios.Middlewares;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Sistema_Gestor_De_Usuarios.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "sistema_gestor_de_usuarios_API");
                options.DefaultModelRendering(ModelRendering.Model);
            });
        }
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
