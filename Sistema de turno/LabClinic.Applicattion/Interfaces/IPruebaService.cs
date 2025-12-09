using LabClinic.Application.DATA;

namespace LabClinic.Application.Interfaces
{
    public interface IPruebaService
    {
        Task<Pruebadata?> GetByIdAsync(Guid id);
        Task<IEnumerable<Pruebadata>> GetAllAsync();
        Task<Pruebadata> CreateAsync(CreateUpdatePruebadata dto);
        Task UpdateAsync(Guid id, CreateUpdatePruebadata dto);
        Task DeleteAsync(Guid id);
        Task<Pruebadata?> GetByCodigoAsync(string codigo);
    }
}