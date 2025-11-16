using Microsoft.EntityFrameworkCore.Storage;
using LabClinic.Infrastructure.Context;
using LabClinic.Infrastructure.Interfaces;
using LabClinic.Infrastructure.Repositories;


namespace LabClinic.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LabClinicContext _context;
        private IDbContextTransaction? _transaction;


        private IPacienteRepository? _pacientes;
        private ITecnicoRepository? _tecnicos;
        private IPruebaRepository? _pruebas;
        private ICitaRepository? _citas;


        public UnitOfWork(LabClinicContext context)
        {
            _context = context;
        }


        public IPacienteRepository Pacientes => _pacientes ??= new PacienteRepository(_context);
        public ITecnicoRepository Tecnicos => _tecnicos ??= new TecnicoRepository(_context);
        public IPruebaRepository Pruebas => _pruebas ??= new PruebaRepository(_context);
        public ICitaRepository Citas => _citas ??= new CitaRepository(_context);


        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();


        public async Task BeginTransactionAsync()
        {
            if (_transaction is null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }


        public async Task CommitTransactionAsync()
        {
            if (_transaction is null) return;
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }


        public async Task RollbackTransactionAsync()
        {
            if (_transaction is null) return;
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }


        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
