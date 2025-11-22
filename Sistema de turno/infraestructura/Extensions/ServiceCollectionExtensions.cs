using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LabClinic.Infrastructure.Context;
using LabClinic.Infrastructure.Interfaces;
using LabClinic.Infrastructure.Repositories;

namespace LabClinic.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLabClinicInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LabClinicContext>(opt => opt.UseSqlServer(connectionString));


            // Repositorios
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<ITecnicoRepository, TecnicoRepository>();
            services.AddScoped<IPruebaRepository, PruebaRepository>();
            services.AddScoped<ICitaRepository, CitaRepository>();


            // Unit of Work
            services.AddScoped<LabClinic.Infrastructure.UnitOfWork.IUnitOfWork, LabClinic.Infrastructure.UnitOfWork.UnitOfWork>();


            return services;
        }
    }
}
