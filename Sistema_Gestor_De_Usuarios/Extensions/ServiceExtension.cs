using Microsoft.OpenApi.Models;

namespace Sistema_Gestor_De_Usuarios.Extensions
{
    public static class ServiceExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API BHD - Gestor de Usuarios",
                    Description = "Una API para una aplicación de gestión de usuarios",
                    Contact = new OpenApiContact
                    {
                        Name = "Eliott",
                        Email = "eliottreyes22@gmail.com"
                    }
                });

                options.EnableAnnotations();
                options.DescribeAllParametersInCamelCase();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here}"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        }, new List<string>()
                    }
                });
            });
        }
    }
}
