using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UniversidadAPI.DBModels;
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
            /*var materiasAlumno = _db.U_ALUMNO.
                Where(e => e.U_HISTORIA_ACADEMICA.Count > 0). // & para concatenar Where
                Where(i => 
                (float)i.U_HISTORIA_ACADEMICA.Count(i => i.ESTADO == "Aprobado") / i.U_HISTORIA_ACADEMICA.Count > porcentajeCarrera).
                Select(a => new Alumno
                {
                    Nombre = a.NOMBRE,
                    Apellido = a.APELLIDO,
                    Dni = a.DNI,
                    Email = a.EMAIL
                });*/
            var materiasAlumno = _db.VW_ALUMNO_MATERIAS.
                Where(e => (float)e.MATERIAS_APROBADAS / (float)e.CANTIDAD_MATERIAS >= porcentajeCarrera).Select(a => new AlumnoCandidato
                {
                    Nombre = a.NOMBRE,
                    Apellido = a.APELLIDO,
            });
            return materiasAlumno.ToList();

        }
    }
   
}
