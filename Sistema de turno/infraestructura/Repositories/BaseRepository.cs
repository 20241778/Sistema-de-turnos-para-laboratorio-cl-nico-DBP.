using LabClinic.Domain.Repository;
using LabClinic.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace LabClinic.Infrastructure.Core
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly LabClinicContext _context;
        protected readonly DbSet<T> _dbSet;


        public BaseRepository(LabClinicContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);


        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();


        public virtual async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);


        public virtual void Remove(T entity) => _dbSet.Remove(entity);


        public virtual void Update(T entity) => _dbSet.Update(entity);
    }
}