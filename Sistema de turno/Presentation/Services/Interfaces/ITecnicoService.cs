using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Services.Interfaces
{
    public interface ITecnicoService
    {
        Task<IEnumerable<Tecnicodata>> GetAllAsync();
        Task<Tecnicodata?> GetByIdAsync(Guid id);
        Task<Tecnicodata> CreateAsync(Tecnicodata dto);
        Task<Tecnicodata> UpdateAsync(Tecnicodata dto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Tecnicodata>> GetByEspecialidadAsync(string especialidad);
    }
}
