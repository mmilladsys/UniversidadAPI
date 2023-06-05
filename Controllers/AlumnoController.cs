using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Models;

namespace UniversidadAPI.Controllers
{
    [ControllerName("Alumno")]
    [Route("[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly DBConnection _db = new DBConnection();

        [HttpGet(Name = "GetAlumnos")]
        public List<AlumnoCandidato> ProximosGraduados(float porcentajeCarrera)
        {
            var materiasAlumno = _db.VW_ALUMNO_MATERIAS.
                Where(e => (float)e.MATERIAS_APROBADAS / (float)e.CANTIDAD_MATERIAS >= porcentajeCarrera).Select(a => new AlumnoCandidato
                { // & para concatenar Where
                    Nombre = a.NOMBRE,
                    Apellido = a.APELLIDO,
                });
            return materiasAlumno.ToList();

        }
    }

}
