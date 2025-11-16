using LabClinic.Domain.Entities;
using LabClinic.Domain.Repository;


namespace LabClinic.Infrastructure.Interfaces
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Task<Paciente?> GetByCorreoAsync(string correo);
        Task<IEnumerable<Paciente>> SearchByNameAsync(string term);
    }
}