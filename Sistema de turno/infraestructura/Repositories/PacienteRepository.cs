using Microsoft.EntityFrameworkCore;
using LabClinic.Domain.Entities;
using LabClinic.Infrastructure.Core;
using LabClinic.Infrastructure.Context;
using LabClinic.Infrastructure.Interfaces;


namespace LabClinic.Infrastructure.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(LabClinicContext context) : base(context) { }


        public async Task<Paciente?> GetByCorreoAsync(string correo)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Correo == correo);
        }


        public async Task<IEnumerable<Paciente>> SearchByNameAsync(string term)
        {
            return await _dbSet.AsNoTracking()
            .Where(p => (p.Nombre + " " + p.Apellido).Contains(term))
            .ToListAsync();
        }
    }
}