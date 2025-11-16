using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LabClinic.Domain.Entities;


namespace LabClinic.Infrastructure.Configurations
{
    public class PruebaConfig : IEntityTypeConfiguration<Prueba>
    {
        public void Configure(EntityTypeBuilder<Prueba> builder)
        {
            builder.ToTable("Pruebas");
            builder.HasKey(p => p.Id);


            builder.Property(p => p.Codigo).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Nombre).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Precio).HasColumnType("decimal(18,2)");


            builder.HasIndex(p => p.Codigo).IsUnique();
        }
    }
}