using LabClinic.Infrastructure.Interfaces;


namespace LabClinic.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPacienteRepository Pacientes { get; }
        ITecnicoRepository Tecnicos { get; }
        IPruebaRepository Pruebas { get; }
        ICitaRepository Citas { get; }


        Task<int> CommitAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
