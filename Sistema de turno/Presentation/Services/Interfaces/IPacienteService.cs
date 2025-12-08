using Presentation.Models;

namespace Presentation.Services.Interfaces
{
    public interface IPacienteService
    {
        Task<IEnumerable<Pacientedata>> GetAllAsync();
        Task<Pacientedata?> GetByIdAsync(Guid id);
        Task<Pacientedata> CreateAsync(Pacientedata dto);
        Task<Pacientedata> UpdateAsync(Pacientedata dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
