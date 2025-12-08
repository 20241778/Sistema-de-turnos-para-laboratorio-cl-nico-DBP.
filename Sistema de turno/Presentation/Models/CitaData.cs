namespace Presentation.Models
{
    public class Citadata
    {
        public Guid Id { get; set; }
        public Guid PacienteId { get; set; }
        public Guid TecnicoId { get; set; }
        public Guid PruebaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = default!;
    }
}
