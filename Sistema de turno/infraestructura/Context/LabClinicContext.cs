
using Microsoft.EntityFrameworkCore;
using LabClinic.Domain.Entities;
using LabClinic.Infrastructure.Configurations;


namespace LabClinic.Infrastructure.Context
{
    public class LabClinicContext : DbContext
    {
        public LabClinicContext(DbContextOptions<LabClinicContext> options) : base(options) { }


        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Prueba> Pruebas { get; set; }
        public DbSet<Cita> Citas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new PacienteConfig());
            modelBuilder.ApplyConfiguration(new TecnicoConfig());
            modelBuilder.ApplyConfiguration(new PruebaConfig());
            modelBuilder.ApplyConfiguration(new CitaConfig());
        }
    }
}