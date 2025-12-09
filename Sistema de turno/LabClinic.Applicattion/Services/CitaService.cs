using AutoMapper;
using LabClinic.Application.Interfaces;
using LabClinic.Application.DATA;
using LabClinic.Infrastructure.UnitOfWork;
using LabClinic.Domain.Entities;

namespace LabClinic.Application.Services
{
    public class CitaService : ICitaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CitaService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Citadata> CreateAsync(CreateCitadata dto)
        {
            // Validaciones básicas
            if (await _uow.Citas.HasConflictingAppointmentAsync(dto.TecnicoId, dto.Fecha))
                throw new InvalidOperationException("El técnico tiene otra cita en la misma fecha/hora.");

            var entity = _mapper.Map<Cita>(dto);
            await _uow.Citas.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<Citadata>(entity);
        }

        public async Task CancelAsync(Guid id)
        {
            var e = await _uow.Citas.GetByIdAsync(id) ?? throw new KeyNotFoundException("Cita no encontrada");
            e.Cancelar();
            _uow.Citas.Update(e);
            await _uow.CommitAsync();
        }

        public async Task<IEnumerable<Citadata>> GetAllAsync()
        {
            var list = await _uow.Citas.GetAllAsync();
            return _mapper.Map<IEnumerable<Citadata>>(list);
        }

        public async Task<Citadata?> GetByIdAsync(Guid id)
        {
            var e = await _uow.Citas.GetByIdAsync(id);
            return e == null ? null : _mapper.Map<Citadata>(e);
        }

        public async Task<IEnumerable<Citadata>> GetByPacienteAsync(Guid pacienteId)
        {
            var list = await _uow.Citas.GetByPacienteAsync(pacienteId);
            return _mapper.Map<IEnumerable<Citadata>>(list);
        }

        public async Task<IEnumerable<Citadata>> GetByTecnicoAndRangeAsync(Guid tecnicoId, DateTime from, DateTime to)
        {
            var list = await _uow.Citas.GetByTecnicoAndDateRangeAsync(tecnicoId, from, to);
            return _mapper.Map<IEnumerable<Citadata>>(list);
        }

        public async Task UpdateFechaAsync(Guid id, DateTime nuevaFecha)
        {
            var e = await _uow.Citas.GetByIdAsync(id) ?? throw new KeyNotFoundException("Cita no encontrada");
            if (await _uow.Citas.HasConflictingAppointmentAsync(e.TecnicoId, nuevaFecha))
                throw new InvalidOperationException("El técnico tiene otra cita en la misma fecha/hora.");

            // crear nueva instancia para mantener inmutabilidad del dominio
            var updated = new Cita(e.PacienteId, e.TecnicoId, e.PruebaId, nuevaFecha);
            var idProp = typeof(LabClinic.Domain.Core.BaseEntity).GetProperty("Id", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            idProp!.SetValue(updated, e.Id);
            // preservar estado si era completada/cancelada
            if (e.Estado == EstadoCita.Completada) updated.Completar();
            if (e.Estado == EstadoCita.Cancelada) updated.Cancelar();

            _uow.Citas.Update(updated);
            await _uow.CommitAsync();
        }
    }
}