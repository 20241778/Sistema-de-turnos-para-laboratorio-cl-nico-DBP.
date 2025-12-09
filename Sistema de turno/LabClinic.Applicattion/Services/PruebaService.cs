using AutoMapper;
using LabClinic.Application.Interfaces;
using LabClinic.Application.DATA;
using LabClinic.Infrastructure.UnitOfWork;
using LabClinic.Domain.Entities;

namespace LabClinic.Application.Services
{
    public class PruebaService : IPruebaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PruebaService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Pruebadata> CreateAsync(CreateUpdatePruebadata dto)
        {
            var entity = _mapper.Map<Prueba>(dto);
            await _uow.Pruebas.AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<Pruebadata>(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var e = await _uow.Pruebas.GetByIdAsync(id) ?? throw new KeyNotFoundException("Prueba no encontrada");
            _uow.Pruebas.Remove(e);
            await _uow.CommitAsync();
        }

        public async Task<IEnumerable<Pruebadata>> GetAllAsync()
        {
            var list = await _uow.Pruebas.GetAllAsync();
            return _mapper.Map<IEnumerable<Pruebadata>>(list);
        }

        public async Task<Pruebadata?> GetByCodigoAsync(string codigo)
        {
            var e = await _uow.Pruebas.GetByCodigoAsync(codigo);
            return e == null ? null : _mapper.Map<Pruebadata>(e);
        }

        public async Task<Pruebadata?> GetByIdAsync(Guid id)
        {
            var e = await _uow.Pruebas.GetByIdAsync(id);
            return e == null ? null : _mapper.Map<Pruebadata>(e);
        }

        public async Task UpdateAsync(Guid id, CreateUpdatePruebadata dto)
        {
            var existing = await _uow.Pruebas.GetByIdAsync(id) ?? throw new KeyNotFoundException("Prueba no encontrada");

            // Map properties from DTO to existing entity
            _mapper.Map(dto, existing);

            _uow.Pruebas.Update(existing);
            await _uow.CommitAsync();
        }
    }
}
