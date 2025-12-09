using AutoMapper;
using LabClinic.Application.Interfaces;
using LabClinic.Application.Mappings;
using LabClinic.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LabClinic.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLabClinicApplication(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Servicios
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<ITecnicoService, TecnicoService>();
            services.AddScoped<IPruebaService, PruebaService>();
            services.AddScoped<ICitaService, CitaService>();

            return services;
        }
    }
}

