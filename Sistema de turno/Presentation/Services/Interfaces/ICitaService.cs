using Presentation.Models;

namespace Presentation.Services.Interfaces
{
    public interface ICitaService
    {
        Task<IEnumerable<Citadata>> GetAllAsync();
        Task<Citadata?> GetByIdAsync(Guid id);
        Task<IEnumerable<Citadata>> GetByPacienteAsync(Guid pacienteId);
        Task<IEnumerable<Citadata>> GetByTecnicoAndDateRangeAsync(Guid tecnicoId, DateTime from, DateTime to);
        Task<bool> HasConflictingAppointmentAsync(Guid tecnicoId, DateTime fecha);
    }
}
