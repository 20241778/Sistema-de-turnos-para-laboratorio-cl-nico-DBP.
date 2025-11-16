namespace LabClinic.Applicattion.DATA
{
    public class Pacientedata
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Apellido { get; set; } = default!;
        public string Telefono { get; set; } = default!;
        public string Correo { get; set; } = default!;
    }
}