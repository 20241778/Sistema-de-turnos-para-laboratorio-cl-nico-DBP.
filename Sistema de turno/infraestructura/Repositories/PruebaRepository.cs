using Microsoft.EntityFrameworkCore;
using LabClinic.Domain.Entities;
using LabClinic.Infrastructure.Core;
using LabClinic.Infrastructure.Context;
using LabClinic.Infrastructure.Interfaces;


namespace LabClinic.Infrastructure.Repositories
{
    public class PruebaRepository : BaseRepository<Prueba>, IPruebaRepository
    {
        public PruebaRepository(LabClinicContext context) : base(context) { }


        public async Task<Prueba?> GetByCodigoAsync(string codigo)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Codigo == codigo);
        }
    }
}