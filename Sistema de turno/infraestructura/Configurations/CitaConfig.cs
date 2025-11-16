using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LabClinic.Domain.Entities;


namespace LabClinic.Infrastructure.Configurations
{
    public class CitaConfig : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ToTable("Citas");
            builder.HasKey(c => c.Id);


            builder.Property(c => c.Fecha).IsRequired();
            builder.Property(c => c.Estado).IsRequired();


            // Relaciones
            builder.HasOne<Paciente>()
            .WithMany()
            .HasForeignKey(c => c.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne<Tecnico>()
            .WithMany()
            .HasForeignKey(c => c.TecnicoId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne<Prueba>()
            .WithMany()
            .HasForeignKey(c => c.PruebaId)
            .OnDelete(DeleteBehavior.Restrict);


            // Índice para búsquedas por fecha/técnico
            builder.HasIndex(c => new { c.TecnicoId, c.Fecha });
        }
    }
}