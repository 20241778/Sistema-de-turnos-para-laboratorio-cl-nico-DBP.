using LabClinic.Application.DATA;
using System;

namespace LabClinic.Application.Interfaces
{
    public interface IPacienteService
    {
        Task<Pacientedata?> GetByIdAsync(Guid id);
        Task<IEnumerable<Pacientedata>> GetAllAsync();
        Task<Pacientedata> CreateAsync(CreateUpdatePacientedata dto);
        Task UpdateAsync(Guid id, CreateUpdatePacientedata dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Pacientedata>> SearchByNameAsync(string term);
    }
}