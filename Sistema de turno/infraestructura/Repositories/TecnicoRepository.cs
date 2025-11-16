using Microsoft.EntityFrameworkCore;
using LabClinic.Domain.Entities;
using LabClinic.Infrastructure.Core;
using LabClinic.Infrastructure.Context;
using LabClinic.Infrastructure.Interfaces;


namespace LabClinic.Infrastructure.Repositories
{
    public class TecnicoRepository : BaseRepository<Tecnico>, ITecnicoRepository
    {
        public TecnicoRepository(LabClinicContext context) : base(context) { }


        public async Task<IEnumerable<Tecnico>> GetByEspecialidadAsync(string especialidad)
        {
            return await _dbSet.AsNoTracking()
            .Where(t => t.Especialidad == especialidad)
            .ToListAsync();
        }
    }
}
