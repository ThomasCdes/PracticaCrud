using PracticaCrud.Models;
using System.Data;
using Dapper;
using System;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;


namespace PracticaCrud.Services
{
    public class UsuarioService
    {
        private readonly string _connectionString;

        public UsuarioService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<(bool, string)> CrearUsuarioAsync(Usuario usuario)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Ejecutar el SP con Dapper
                    var parameters = new DynamicParameters();
                    parameters.Add("@Nombre", usuario.Nombre ?? (object)DBNull.Value);
                    parameters.Add("@Email", usuario.Email ?? (object)DBNull.Value);

                    // Ejecutar el SP
                    await connection.ExecuteAsync("sp_AgregarUsuario", parameters, commandType: CommandType.StoredProcedure);

                    return (true, "Usuario creado correctamente.");
                }
            }
            catch (Exception ex)
            {
                return (false, "Error al crear el usuario: " + ex.Message);
            }
        }
        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            try
            {
                // Establece la conexión a la base de datos
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync(); // Abre la conexión asincrónicamente

                    // Ejecuta el SP y mapea los resultados directamente a la lista de objetos Usuario
                    var usuarios = await connection.QueryAsync<Usuario>("sp_VerUsuarios", commandType: System.Data.CommandType.StoredProcedure);

                    return usuarios.ToList(); // Convierte el resultado a una lista
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores, puedes registrar el error o retornar algo en caso de fallo
                throw new Exception("Error al obtener los usuarios: " + ex.Message);
            }
        }
        public async Task<List<Usuario>> ObtenerUsuariosxIdAsync(Usuario usuario)
        {
            try
            {

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Ejecutar el SP con Dapper
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", usuario.Id);

                    // Ejecutar el SP
                    var usuarios = await connection.QueryAsync<Usuario>("sp_VerUsuarioPorId", parameters, commandType: CommandType.StoredProcedure);
                    return usuarios.ToList();

                }


            }
            catch (Exception ex)
            {
                // Manejo de errores, puedes registrar el error o retornar algo en caso de fallo
                throw new Exception("Error al obtener los usuarios: " + ex.Message);
            }
        }
        public async Task<(bool, string)> ActualizarUsuarioAsync(Usuario usuario)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", usuario.Id, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@Nombre", usuario.Nombre ?? (object)DBNull.Value);
                    parameters.Add("@Email", usuario.Email ?? (object)DBNull.Value);

                    await connection.ExecuteAsync("sp_EditarUsuario", parameters, commandType: CommandType.StoredProcedure);

                    return (true, "Usuario actualizado correctamente.");
                }
            }
            catch (Exception ex)
            {
                return (false, "Error al actualizar el usuario: " + ex.Message);
            }
        }

    }
}
