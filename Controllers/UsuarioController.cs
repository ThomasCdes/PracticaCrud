using Microsoft.AspNetCore.Mvc;
using PracticaCrud.Models;
using PracticaCrud.Services;
using System.Threading.Tasks;

namespace PracticaCrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Endpoint para crear un usuario
        [HttpPost("Create")]  // Ruta personalizada para crear un usuario
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            // Verificar si el modelo de usuario es válidoN
            if (!ModelState.IsValid)
            {
                return BadRequest("Los datos del usuario no son válidos.");
            }

            // Llamar al servicio para crear el usuario
            var (success, message) = await _usuarioService.CrearUsuarioAsync(usuario);

            if (success)
            {
                // Si la operación fue exitosa, retornar un código de estado 200 (OK)
                return Ok(new { message });
            }
            else
            {
                // Si hubo un error, retornar un código de estado 500 (Internal Server Error)
                return StatusCode(500, new { message });
            }
        }

        [HttpGet("GetAll")]  // Ruta personalizada para crear un usuario
        public async Task<IActionResult> ObtenerUsuarios()
        {
            try
            {
                // Llamamos al servicio para obtener los usuarios
                IEnumerable<Usuario> usuarios = await _usuarioService.ObtenerUsuariosAsync();

                // Retornamos la lista de usuarios como respuesta HTTP 200 (OK)
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                // En caso de error, retornamos un mensaje con código 500 (Internal Server Error)
                return StatusCode(500, new { message = "Hubo un error al obtener los usuarios: " + ex.Message });
            }
        }
        [HttpPost("GetById")]  // Ruta personalizada para crear un usuario
        public async Task<IActionResult> ObtenerUsuarioxID([FromBody] Usuario usuario)
        {
            // Verificar si el modelo de usuario es válidoN
            if (!ModelState.IsValid)
            {
                return BadRequest("Los datos del usuario no son válidos.");
            }

            // Llamar al servicio para crear el usuario
            try
            {
                // Llamamos al servicio para obtener los usuarios
                IEnumerable<Usuario> usuarios = await _usuarioService.ObtenerUsuariosxIdAsync(usuario);

                // Retornamos la lista de usuarios como respuesta HTTP 200 (OK)
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                // En caso de error, retornamos un mensaje con código 500 (Internal Server Error)
                return StatusCode(500, new { message = "Hubo un error al obtener los usuarios: " + ex.Message });
            }

        }
        [HttpPost("Update")]  // Ruta personalizada para crear un usuario
        public async Task<IActionResult> Update([FromBody] Usuario usuario)
        {
            // Verificar si el modelo de usuario es válidoN
            if (!ModelState.IsValid)
            {
                return BadRequest("Los datos del usuario no son válidos.");
            }

            // Llamar al servicio para crear el usuario
            var (success, message) = await _usuarioService.ActualizarUsuarioAsync(usuario);

            if (success)
            {
                // Si la operación fue exitosa, retornar un código de estado 200 (OK)
                return Ok(new { message });
            }
            else
            {
                // Si hubo un error, retornar un código de estado 500 (Internal Server Error)
                return StatusCode(500, new { message });
            }
        }
    }
}
