using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LabClinic.Domain.Entities;


namespace LabClinic.Infrastructure.Configurations
{
    public class TecnicoConfig : IEntityTypeConfiguration<Tecnico>
    {
        public void Configure(EntityTypeBuilder<Tecnico> builder)
        {
            builder.ToTable("Tecnicos");
            builder.HasKey(t => t.Id);


            builder.Property(t => t.Nombre).IsRequired().HasMaxLength(150);
            builder.Property(t => t.Especialidad).HasMaxLength(150);
        }
    }
}