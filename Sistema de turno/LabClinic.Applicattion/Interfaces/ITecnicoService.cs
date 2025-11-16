using LabClinic.Applicattion.DATA;

namespace LabClinic.Applicattion.Interfaces
{
    public interface ITecnicoService
    {
        Task<Tecnicodata?> GetByIdAsync(Guid id);
        Task<IEnumerable<Tecnicodata>> GetAllAsync();
        Task<Tecnicodata> CreateAsync(CreateUpdateTecnicodata dto);
        Task UpdateAsync(Guid id, CreateUpdateTecnicodata dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Tecnicodata>> GetByEspecialidadAsync(string especialidad);
    }
}