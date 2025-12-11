using LabClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabClinic.Infrastructure.Context
{
    public class LabClinicContext : DbContext
    {
        public LabClinicContext(DbContextOptions<LabClinicContext> options)
            : base(options)
        {
        }

        public DbSet<Cita> Citas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Prueba> Pruebas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ------------------------------
            // CONFIGURACIÓN PARA EL ENUM ESTADO
            // ------------------------------
            modelBuilder.Entity<Cita>()
                .Property(c => c.Estado)
                .HasConversion<string>()      // Guarda el enum como texto
                .HasMaxLength(20);            // Opcional pero recomendado

            // CONFIGURACIÓN OPCIONAL (si tienes nombres de tabla diferentes):
            modelBuilder.Entity<Cita>().ToTable("Citas");
            modelBuilder.Entity<Paciente>().ToTable("Pacientes");
            modelBuilder.Entity<Tecnico>().ToTable("Tecnicos");
            modelBuilder.Entity<Prueba>().ToTable("Pruebas");
        }
    }
}
