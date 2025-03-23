using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.Repositories;
using Sistema_Gestor_De_Usuarios.Core.Application.Abstractions.UnitOfWork;
using Sistema_Gestor_De_Usuarios.Infrastructure.Persistence.Context;
using Sistema_Gestor_De_Usuarios.Infrastructure.Persistence.Repositories;

namespace Sistema_Gestor_De_Usuarios.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuracion del Contexto y Conexión a Base de Datos
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                                                      configuration.GetConnectionString("DefaultConnection"),
                                                      m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            //Configurando Dependencias 

            //Repositorios
            services.AddTransient<IUserRepository, UserRepository>();


            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
