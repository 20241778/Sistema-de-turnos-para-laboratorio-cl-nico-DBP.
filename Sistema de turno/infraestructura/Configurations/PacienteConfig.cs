using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LabClinic.Domain.Entities;


namespace LabClinic.Infrastructure.Configurations
{
    public class PacienteConfig : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Pacientes");
            builder.HasKey(p => p.Id);


            builder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Apellido).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Telefono).HasMaxLength(20);
            builder.Property(p => p.Correo).HasMaxLength(200);


            builder.HasIndex(p => p.Correo).IsUnique(false);
        }
    }
}
