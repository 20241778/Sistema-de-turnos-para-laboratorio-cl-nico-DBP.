using LabClinic.Application.DATA;

namespace LabClinic.Application.Interfaces
{
    public interface ICitaService
    {
        Task<Citadata?> GetByIdAsync(Guid id);
        Task<IEnumerable<Citadata>> GetAllAsync();
        Task<Citadata> CreateAsync(CreateCitadata dto);
        Task UpdateFechaAsync(Guid id, DateTime nuevaFecha);
        Task CancelAsync(Guid id);
        Task<IEnumerable<Citadata>> GetByPacienteAsync(Guid pacienteId);
        Task<IEnumerable<Citadata>> GetByTecnicoAndRangeAsync(Guid tecnicoId, DateTime from, DateTime to);
    }
}