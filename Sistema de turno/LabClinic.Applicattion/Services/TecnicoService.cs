using AutoMapper;
using LabClinic.Application.Mappings;
using LabClinic.Applicattion.DATA;
using LabClinic.Applicattion.Interfaces;

namespace LabClinic.Application.Services
{
    public class TecnicoService(IUnitOfWork uow, IMapper mapper) : ITecnicoService
    {
        private readonly IUnitOfWork _uow = uow;
        private readonly IMapper _mapper = mapper;

        public async Task<Tecnicodata> CreateAsync(CreateUpdateTecnicodata dto)
        {
            var entity = _mapper.Map<Tecnico>(dto);
            await _uow.Tecnicos.AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<Tecnicodata>(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _uow.Tecnicos.GetByIdAsync(id) ?? throw new KeyNotFoundException("Técnico no encontrado");
            _uow.Tecnicos.Remove(e);
            await _uow.CommitAsync();
        }

        public async Task<IEnumerable<Tecnicodata>> GetAllAsync()
        {
            var list = await _uow.Tecnicos.GetAllAsync();
            return _mapper.Map<IEnumerable<Tecnicodata>>(list);
        }

        public async Task<Tecnicodata?> GetByIdAsync(Guid id)
        {
            var e = await _uow.Tecnicos.GetByIdAsync(id);
            return e == null ? null : _mapper.Map<Tecnicodata>(e);
        }

        public async Task<IEnumerable<Tecnicodata>> GetByEspecialidadAsync(string especialidad)
        {
            var r = await _uow.Tecnicos.GetByEspecialidadAsync(especialidad);
            return _mapper.Map<IEnumerable<Tecnicodata>>(r);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateTecnicodata dto)
        {
            var existing = await _uow.Tecnicos.GetByIdAsync(id) ?? throw new KeyNotFoundException("Técnico no encontrado");
            var updated = _mapper.Map<Tecnico>(dto);
            var idProp = typeof(Domain.Core.BaseEntity).GetProperty(\"Id\", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            idProp!.SetValue(updated, id);
            _uow.Tecnicos.Update(updated);
            await _uow.CommitAsync();
        }

        Task<Tecnicodata?> ITecnicoService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Tecnicodata>> ITecnicoService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tecnicodata> CreateAsync(CreateUpdateTecnicodata dto)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Guid id, CreateUpdateTecnicodata dto)
        {
            // Validate the input parameters
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Data transfer object cannot be null.");
            }

            // Retrieve the existing technician record
            var existingTecnicodata = await _uow.Tecnicos.GetByIdAsync(id);
            if (existingTecnicodata == null)
            {
                throw new KeyNotFoundException($"Technician with ID {id} not found.");
            }

            // Update the technician's properties
            existingTecnicodata.Nombre = dto.Nombre;
            existingTecnicodata.Especialidad = dto.Especialidad;

            // Save changes to the database
            await _uow.SaveChangesAsync();
        }

        Task<IEnumerable<Tecnicodata>> ITecnicoService.GetByEspecialidadAsync(string especialidad)
        {
            throw new NotImplementedException();
        }
    }
}