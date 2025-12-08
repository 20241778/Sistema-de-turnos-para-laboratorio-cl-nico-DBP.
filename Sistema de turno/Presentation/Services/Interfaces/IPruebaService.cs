using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Services.Interfaces
{
    public interface IPruebaService
    {
        Task<IEnumerable<Pruebadata>> GetAllAsync();
        Task<Pruebadata?> GetByIdAsync(Guid id);
        Task<Pruebadata> CreateAsync(Pruebadata dto);
        Task<Pruebadata> UpdateAsync(Pruebadata dto);
        Task<bool> DeleteAsync(Guid id);
        Task<Pruebadata?> GetByCodigoAsync(string codigo);
    }
}
