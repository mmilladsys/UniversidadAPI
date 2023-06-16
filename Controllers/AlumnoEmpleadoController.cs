using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
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
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        [HttpGet(Name = "GetAlumnosProgramadores")]
        public List<AlumnoCandidato> AlumnosProgramadores()
        {
            try
            {
                //Pedir token
                var epLogin = $"{configuration.GetSection("RRHHApi:Url").Value}/Login";
                var token = HttpApiHelper.GetAuth(
                    epLogin,
                    new
                    {
                        Password = configuration.GetSection("RRHHApi:Password").Value,
                        Username = configuration.GetSection("RRHHApi:User").Value
                    }
                );
                //Pedir datos
                var epProgrammers = $"{configuration.GetSection("RRHHApi:Url").Value}/Programmers";
                var programadores = HttpApiHelper.GetWAuth<List<EmployeeProgrammer>>(
                    epProgrammers,
                    token
                );
                if (programadores.Count > 0)
                {
                    var resultSet = _db.U_ALUMNO
                        .AsEnumerable()
                        .Where(e => programadores.Exists(p => p.Name == $"{e.NOMBRE} {e.APELLIDO}"))
                        .Select(
                            a => new AlumnoCandidato { Nombre = a.NOMBRE, Apellido = a.APELLIDO }
                        );
                    return resultSet.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
