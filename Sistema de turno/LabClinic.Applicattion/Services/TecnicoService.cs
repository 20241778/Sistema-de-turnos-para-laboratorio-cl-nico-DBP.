using AutoMapper;
using LabClinic.Application.DATA;
using LabClinic.Application.Interfaces;
using LabClinic.Infrastructure.UnitOfWork;
using LabClinic.Domain.Entities;

namespace LabClinic.Application.Services
{
    public class TecnicoService : ITecnicoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TecnicoService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

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
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Data transfer object cannot be null.");
            }

            var existingTecnico = await _uow.Tecnicos.GetByIdAsync(id);
            if (existingTecnico == null)
            {
                throw new KeyNotFoundException($"Technician with ID {id} not found.");
            }

            // Map properties from DTO to existing entity
            _mapper.Map(dto, existingTecnico);

            _uow.Tecnicos.Update(existingTecnico);
            await _uow.CommitAsync();
        }
    }
}