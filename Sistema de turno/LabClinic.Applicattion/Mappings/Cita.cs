
namespace LabClinic.Application.Mappings
{
    internal class Cita
    {
        private object pacienteId;
        private object tecnicoId;
        private object pruebaId;
        private DateTime nuevaFecha;

        public Cita(object pacienteId, object tecnicoId, object pruebaId, DateTime nuevaFecha)
        {
            this.pacienteId = pacienteId;
            this.tecnicoId = tecnicoId;
            this.pruebaId = pruebaId;
            this.nuevaFecha = nuevaFecha;
        }

        public object Estado { get; internal set; }

        internal void Cancelar()
        {
            throw new NotImplementedException();
        }

        internal void Completar()
        {
            throw new NotImplementedException();
        }
    }
}