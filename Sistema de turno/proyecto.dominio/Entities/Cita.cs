using LabClinic.Domain.Core;

namespace LabClinic.Domain.Entities
{
    public enum EstadoCita { Programada, Completada, Cancelada }

    public class Cita : BaseEntity
    {
        public Guid PacienteId { get; private set; }
        public Guid TecnicoId { get; private set; }
        public Guid PruebaId { get; private set; }
        public DateTime Fecha { get; private set; }
        public EstadoCita Estado { get; private set; }

        protected Cita() { }

        public Cita(Guid pacienteId, Guid tecnicoId, Guid pruebaId, DateTime fecha)
        {
            PacienteId = pacienteId;
            TecnicoId = tecnicoId;
            PruebaId = pruebaId;
            Fecha = fecha;
            Estado = EstadoCita.Programada;
        }

        public void Completar() => Estado = EstadoCita.Completada;
        public void Cancelar() => Estado = EstadoCita.Cancelada;
    }
}



