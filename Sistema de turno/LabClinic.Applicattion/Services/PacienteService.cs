using AutoMapper;
using LabClinic.Application.DATA;
using LabClinic.Application.Interfaces;
using LabClinic.Infrastructure.UnitOfWork;
using LabClinic.Domain.Entities;

namespace LabClinic.Application.Services
{
    public class PacienteService : IPacienteService
 {
        private readonly IUnitOfWork _uow;
   private readonly IMapper _mapper;

     public PacienteService(IUnitOfWork uow, IMapper mapper)
        {
   _uow = uow;
      _mapper = mapper;
        }

        public async Task<Pacientedata> CreateAsync(CreateUpdatePacientedata dto)
   {
     var entity = _mapper.Map<Paciente>(dto);
      await _uow.Pacientes.AddAsync(entity);
    await _uow.CommitAsync();
   return _mapper.Map<Pacientedata>(entity);
  }

      public async Task DeleteAsync(Guid id)
     {
    var entity = await _uow.Pacientes.GetByIdAsync(id)
  ?? throw new KeyNotFoundException("Paciente no encontrado");
   
 _uow.Pacientes.Remove(entity);
   await _uow.CommitAsync();
    }

        public async Task<IEnumerable<Pacientedata>> GetAllAsync()
        {
     var lista = await _uow.Pacientes.GetAllAsync();
     return _mapper.Map<IEnumerable<Pacientedata>>(lista);
}

    public async Task<Pacientedata?> GetByIdAsync(Guid id)
   {
   var e = await _uow.Pacientes.GetByIdAsync(id);
     return e == null ? null : _mapper.Map<Pacientedata>(e);
  }

        public async Task<IEnumerable<Pacientedata>> SearchByNameAsync(string term)
        {
    var res = await _uow.Pacientes.SearchByNameAsync(term);
      return _mapper.Map<IEnumerable<Pacientedata>>(res);
 }

 public async Task UpdateAsync(Guid id, CreateUpdatePacientedata dto)
     {
    var existing = await _uow.Pacientes.GetByIdAsync(id)
   ?? throw new KeyNotFoundException("Paciente no encontrado");

     // Map properties from DTO to existing entity
            _mapper.Map(dto, existing);

_uow.Pacientes.Update(existing);
     await _uow.CommitAsync();
 }
    }
}
