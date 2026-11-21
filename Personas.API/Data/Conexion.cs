using Microsoft.Data.SqlClient;

namespace Personas.API.Data
{
    public class Conexion
    {
        private readonly IConfiguration _configuration;

        public Conexion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection CrearConexion()
        {
            string connectionString = _configuration
                .GetConnectionString("DefaultConnection");

            return new SqlConnection(connectionString);
        }
    }
}