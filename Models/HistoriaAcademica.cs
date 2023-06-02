namespace UniversidadAPI.Models
{
    public class HistoriaAcademica
    {
        public string? NombreAlumno { get; set; }

        public string? NombreMateria { get; set; }

        public string? NombreCarrera { get; set; }

        public string Estado { get; set; } = null!;

        public byte Nota { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }
    }
}
