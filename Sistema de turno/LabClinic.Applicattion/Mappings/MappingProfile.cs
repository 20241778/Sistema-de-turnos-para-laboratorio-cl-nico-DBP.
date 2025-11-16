using AutoMapper;
using LabClinic.Applicattion.DATA;

namespace LabClinic.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Paciente
            CreateMap<Paciente, Pacientedata>().ReverseMap();
            CreateMap<CreateUpdatePacientedata, Paciente>();

            // Tecnico
            CreateMap<Tecnico, Tecnicodata>().ReverseMap();
            CreateMap<CreateUpdateTecnicodata, Tecnico>();

            // Prueba
            CreateMap<Prueba, Pruebadata>().ReverseMap();
            CreateMap<CreateUpdatePruebadata, Prueba>();

            // Cita
            CreateMap<Cita, Citadata>()
                .ForMember(d => d.Estado, opt => opt.MapFrom(s => s.Estado.ToString()));
            CreateMap<CreateCitadata, Cita>();
        }
    }
}