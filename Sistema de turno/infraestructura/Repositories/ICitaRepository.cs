using LabClinic.Domain.Entities;
using LabClinic.Domain.Repository;


namespace LabClinic.Infrastructure.Interfaces
{
    public interface ICitaRepository : IRepository<Cita>
    {
        Task<IEnumerable<Cita>> GetByPacienteAsync(Guid pacienteId);
        Task<IEnumerable<Cita>> GetByTecnicoAndDateRangeAsync(Guid tecnicoId, DateTime from, DateTime to);
        Task<bool> HasConflictingAppointmentAsync(Guid tecnicoId, DateTime fecha);
    }
}