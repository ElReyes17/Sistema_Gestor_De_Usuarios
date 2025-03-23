

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema_Gestor_De_Usuarios.Core.Domain.Entities;

namespace Sistema_Gestor_De_Usuarios.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Nombre de la tabla en SQL
            builder.ToTable("Users");

            //Seteando la llave primaria
            builder.HasKey(p => p.UserId);


            //Configurando las propiedades
            builder.Property(p => p.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(p => p.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(p => p.Password)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(p => p.Created)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.Property(p => p.Modified)
                   .IsRequired(false);

            builder.Property(p => p.LastLogin)
                   .IsRequired(false);

            builder.Property(p => p.Token)
                   .HasMaxLength(500)
                   .IsRequired();


            //Agregando el filtro de soft delete
            builder.HasQueryFilter(x => x.IsActive);
        }
    }
}
