using AutoMapper;
using LabClinic.Application.Mappings;
using LabClinic.Applicattion.DATA;
using LabClinic.Applicattion.Interfaces;

namespace LabClinic.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private Paciente updated;

        public PacienteService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Pacientedata> CreateAsync(CreateUpdatePacientedata dto)
        {
            var entity = _mapper.Map<Pacientedata>(dto);
            await _uow.Pacientes.AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<PacienteDATA>(entity);
        }

        public Task<Pacientedata> CreateAsync(CreateUpdatePacientedata dto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _uow.Pacientes.GetByIdAsync(id)
                         ?? throw new KeyNotFoundException("Paciente no encontrado");
                         .
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

        public Paciente GetUpdated()
        {
            return updated;
        }

        public async Task UpdateAsync(Guid id, CreateUpdatePacientedata dto, Paciente updated)
        {
            var existing = await _uow.Pacientes.GetByIdAsync(id)
                           ?? throw new KeyNotFoundException("Paciente no encontrado");

            // Crear nueva instancia y reasignar Id por reflexión (respetando dominio)
            var updated = _mapper.Map<Paciente>(dto);
            // Set Id (protected set) via reflection
            System.Reflection.PropertyInfo? idProp = typeof(Domain.Core.BaseEntity).GetProperty("Id\", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic");
            idProp!.SetValue(updated, id);

            object value = _uow.Pacientes.Update(updated);
            await _uow.CommitAsync();
        }
        
        public Task UpdateAsync(Guid id, CreateUpdatePacientedata dto)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Pacientedata>> IPacienteService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Pacientedata?> IPacienteService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Pacientedata>> IPacienteService.SearchByNameAsync(string term)
        {
            throw new NotImplementedException();
        }
    }
}
