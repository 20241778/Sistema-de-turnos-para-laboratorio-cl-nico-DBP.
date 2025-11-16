using LabClinic.Domain.Entities;
using LabClinic.Domain.Repository;


namespace LabClinic.Infrastructure.Interfaces
{
    public interface IPruebaRepository : IRepository<Prueba>
    {
        Task<Prueba?> GetByCodigoAsync(string codigo);
    }
}