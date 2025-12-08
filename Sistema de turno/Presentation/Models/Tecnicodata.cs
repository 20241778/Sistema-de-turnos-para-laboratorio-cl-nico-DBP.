using System;

namespace Presentation.Models
{
    public class Tecnicodata
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
    }
}
