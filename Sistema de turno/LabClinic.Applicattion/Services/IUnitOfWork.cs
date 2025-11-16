
namespace LabClinic.Application.Services
{
    internal interface IUnitOfWork
    {
        object Pacientes { get; }
        object Tecnicos { get; }
        object Pruebas { get; }
        object Citas { get; }

        Task CommitAsync();
        Task SaveChangesAsync();
    }
}