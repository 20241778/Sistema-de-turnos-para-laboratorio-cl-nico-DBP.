using AutoMapper;
using LabClinic.Application.Interfaces;
using LabClinic.Application.Mappings;
using LabClinic.Applicattion.DATA;
using LabClinic.Applicattion.Interfaces;

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

        public Task<Pruebadata> CreateAsync(CreateUpdatePruebadata dto)
        {
            throw new NotImplementedException();
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
            var updated = _mapper.Map<Prueba>(dto);
            var idProp = typeof(Domain.Core.BaseEntity).GetProperty("Id\", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            idProp!.SetValue(updated, id);
            _uow.Pruebas.Update(updated);
            await _uow.CommitAsync();
        }

        public Task UpdateAsync(Guid id, CreateUpdatePruebadata dto)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Pruebadata>> IPruebaService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Pruebadata?> IPruebaService.GetByCodigoAsync(string codigo)
        {
            throw new NotImplementedException();
        }

        Task<Pruebadata?> IPruebaService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
