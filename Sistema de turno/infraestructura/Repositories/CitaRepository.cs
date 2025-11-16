using Microsoft.EntityFrameworkCore;
using LabClinic.Domain.Entities;
using LabClinic.Infrastructure.Core;
using LabClinic.Infrastructure.Context;
using LabClinic.Infrastructure.Interfaces;


namespace LabClinic.Infrastructure.Repositories
{
    public class CitaRepository : BaseRepository<Cita>, ICitaRepository
    {
        public CitaRepository(LabClinicContext context) : base(context) { }


        public async Task<IEnumerable<Cita>> GetByPacienteAsync(Guid pacienteId)
        {
            return await _dbSet.AsNoTracking()
            .Where(c => c.PacienteId == pacienteId)
            .ToListAsync();
        }


        public async Task<IEnumerable<Cita>> GetByTecnicoAndDateRangeAsync(Guid tecnicoId, DateTime from, DateTime to)
        {
            return await _dbSet.AsNoTracking()
            .Where(c => c.TecnicoId == tecnicoId && c.Fecha >= from && c.Fecha <= to)
            .ToListAsync();
        }


        public async Task<bool> HasConflictingAppointmentAsync(Guid tecnicoId, DateTime fecha)
        {
            // Conflicto simple: existe otra cita para el mismo técnico en la misma fecha y estado programada
            return await _dbSet.AnyAsync(c => c.TecnicoId == tecnicoId && c.Fecha == fecha && c.Estado == EstadoCita.Programada);
        }
    }
}