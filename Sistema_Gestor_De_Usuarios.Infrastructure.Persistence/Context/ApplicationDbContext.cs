

using Microsoft.EntityFrameworkCore;
using Sistema_Gestor_De_Usuarios.Core.Domain.Entities;

namespace Sistema_Gestor_De_Usuarios.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
