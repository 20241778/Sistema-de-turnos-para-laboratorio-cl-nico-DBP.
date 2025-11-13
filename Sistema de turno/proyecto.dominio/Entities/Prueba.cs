
using LabClinic.Domain.Entities;

namespace LabClinic.Domain.Entities
{
    public class Prueba
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public decimal Precio { get; private set; }

        protected Prueba() { }

        public Prueba(string codigo, string nombre, decimal precio)
        {
            Codigo = codigo;
            Nombre = nombre;
            Precio = precio;
        }
    }
}





