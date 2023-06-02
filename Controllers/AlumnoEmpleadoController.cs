﻿using Microsoft.AspNetCore.Mvc;
using UniversidadAPI.Helpers;
using UniversidadAPI.Models;

namespace UniversidadAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlumnoProgramadorController : ControllerBase
    {

        public class EmployeeProgrammer
        {
            public string? Name { get; set; }

            public string? PhoneNumber { get; set; } = null!;

        }

        private readonly DBConnection _db = new DBConnection();

        [HttpGet(Name = "GetAlumnosProgramadores")]
        public List<AlumnoCandidato> AlumnosProgramadores()
        {
            try
            {
                var endpoint = "/Programmers";
                var programadores = HttpApiHelper.Get<List<EmployeeProgrammer>>("https://localhost:44348" + endpoint);
                var resultSet = _db.U_ALUMNO.AsEnumerable().
                    Where(e => programadores.Exists(p => p.Name == e.NOMBRE + " " + e.APELLIDO)).
                    Select(
                    a => new AlumnoCandidato
                    {
                        Nombre = a.NOMBRE,
                        Apellido = a.APELLIDO
                    });
                return resultSet.ToList();
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}