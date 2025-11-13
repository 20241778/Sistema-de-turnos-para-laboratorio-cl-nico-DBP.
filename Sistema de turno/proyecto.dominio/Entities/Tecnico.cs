using LabClinic.Domain.Entities;

namespace LabClinic.Domain.Entities
{
    public class Tecnico
    {
        public string Nombre { get; private set; }
        public string Especialidad { get; private set; }

        protected Tecnico() { }

        public Tecnico(string nombre, string especialidad)
        {
            Nombre = nombre;
            Especialidad = especialidad;
        }
    }
}



