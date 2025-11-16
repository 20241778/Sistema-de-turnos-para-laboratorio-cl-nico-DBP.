namespace LabClinic.Applicattion.DATA
{
    public class CreateCitadata
    {
        public Guid PacienteId { get; set; }
        public Guid TecnicoId { get; set; }
        public Guid PruebaId { get; set; }
        public DateTime Fecha { get; set; }
    }
}