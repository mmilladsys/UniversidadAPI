using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Models;

namespace UniversidadAPI.Controllers
{
    [ControllerName("Historia académica")]
    [Route("[controller]")]
    [ApiController]
    public class HistoriaController : ControllerBase
    {
        private readonly DBConnection _db = new DBConnection();

        [HttpGet(Name = "GetHistoria")]
        public List<HistoriaAcademica> historiaAcademica(int dniAlumno)
        {
            var resultSet = _db.U_HISTORIA_ACADEMICA.Where(h => h.ALUMNO.DNI == dniAlumno).
                Select(j => new HistoriaAcademica
                {
                    NombreAlumno = j.ALUMNO.NOMBRE + " " + j.ALUMNO.APELLIDO,
                    NombreMateria = j.MATERIA.NOMBRE_MATERIA,
                    NombreCarrera = j.CARRERA.NOMBRE_CARRERA,
                    Estado = j.ESTADO,
                    Nota = j.NOTA,
                    FechaInicio = j.FECHA_INICIO,
                    FechaFin = j.FECHA_FIN
                });
            return resultSet.ToList();
        }
    }
}
