using LabClinic.Applicattion.DATA;
using System;
namespace LabClinic.Applicattion.Interfaces
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