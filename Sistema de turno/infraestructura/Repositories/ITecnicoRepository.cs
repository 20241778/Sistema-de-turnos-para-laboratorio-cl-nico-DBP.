using LabClinic.Domain.Entities;
using LabClinic.Domain.Repository;


namespace LabClinic.Infrastructure.Interfaces
{
    public interface ITecnicoRepository : IRepository<Tecnico>
    {
        Task<IEnumerable<Tecnico>> GetByEspecialidadAsync(string especialidad);
    }
}
