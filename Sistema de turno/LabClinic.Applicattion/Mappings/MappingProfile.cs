using AutoMapper;
using LabClinic.Applicattion.DATA;
using LabClinic.Domain.Entities;

namespace LabClinic.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Paciente
            CreateMap<Paciente, Pacientedata>().ReverseMap();
            CreateMap<CreateUpdatePacientedata, Paciente>()
                .ConstructUsing(dto => new Paciente(dto.Nombre, dto.Apellido, dto.Telefono, dto.Correo));

            // Tecnico
            CreateMap<Tecnico, Tecnicodata>().ReverseMap();
            CreateMap<CreateUpdateTecnicodata, Tecnico>()
                .ConstructUsing(dto => new Tecnico(dto.Nombre, dto.Especialidad));

            // Prueba
            CreateMap<Prueba, Pruebadata>().ReverseMap();
            CreateMap<CreateUpdatePruebadata, Prueba>()
                .ConstructUsing(dto => new Prueba(dto.Codigo, dto.Nombre, dto.Precio));

            // Cita
            CreateMap<Cita, Citadata>()
                .ForMember(d => d.Estado, opt => opt.MapFrom(s => s.Estado.ToString()));
            CreateMap<CreateCitadata, Cita>()
                .ConstructUsing(dto => new Cita(dto.PacienteId, dto.TecnicoId, dto.PruebaId, dto.Fecha));
        }
    }
}