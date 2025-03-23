
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema_Gestor_De_Usuarios.Core.Domain.Entities;

namespace Sistema_Gestor_De_Usuarios.Infrastructure.Persistence.Configurations
{
    public class PhoneConfiguration : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            //Nombre de la tabla en SQL
            builder.ToTable("Phones");

            //Seteando la llave primaria
            builder.HasKey(p => p.PhoneId);


            //Configurando las propiedades
            builder.Property(p => p.Number)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(p => p.CityCode)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(p => p.CountryCode)
                   .HasMaxLength(50)
                   .IsRequired();


            //Estableciendo la relación uno a muchos con Usuarios
            builder.HasOne(p => p.User)
                   .WithMany(u => u.Phones)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
