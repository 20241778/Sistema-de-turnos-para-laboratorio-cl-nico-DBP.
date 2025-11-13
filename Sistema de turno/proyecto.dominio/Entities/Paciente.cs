
using LabClinic.Domain.Entities;

namespace LabClinic.Domain.Entities
{
    public class Paciente 
    {
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string Telefono { get; private set; }
        public string Correo { get; private set; }

        protected Paciente() { }

        public Paciente(string nombre, string apellido, string telefono, string correo)
        {
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Correo = correo;
        }

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}

