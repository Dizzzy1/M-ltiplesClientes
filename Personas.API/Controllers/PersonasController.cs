using Microsoft.AspNetCore.Mvc;
using Dapper;
using Personas.API.Data;
using Personas.API.Models;

namespace Personas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly Conexion _conexion;

        public PersonasController(Conexion conexion)
        {
            _conexion = conexion;
        }

        // GET: api/Personas
        [HttpGet]
        public async Task<IActionResult> ObtenerPersonas()
        {
            using var connection = _conexion.CrearConexion();

            string sql = "SELECT * FROM Personas";

            var personas = await connection.QueryAsync<Persona>(sql);

            return Ok(personas);
        }

        // POST: api/Personas
        [HttpPost]
        public async Task<IActionResult> CrearPersona(Persona persona)
        {
            using var connection = _conexion.CrearConexion();

            string sql = @"
                INSERT INTO Personas (Nombre, Apellido, Edad)
                VALUES (@Nombre, @Apellido, @Edad)";

            await connection.ExecuteAsync(sql, persona);

            return Ok("Persona guardada correctamente");
        }
    }
}
