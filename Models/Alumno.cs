namespace UniversidadAPI.Models
{
    public class Alumno
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public int Dni { get; set; }

        public string? Email { get; set; }
    }
}
