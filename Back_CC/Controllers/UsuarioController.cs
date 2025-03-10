using Back_CC.DTOs;
using Back_CC.DTOs.UsuarioDTO;
using Back_CC.Models;
using Back_CC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Back_CC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
                return BadRequest("Debe ingresar usuario y contraseña.");

            var usuario = await _usuarioService.ObtenerUsuarioPorUsername(loginDto.Username);

            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            if (usuario.Activo == false)
                return Unauthorized("Usuario inactivo.");

            if (!_usuarioService.VerificarPassword(loginDto.Password, usuario.Password))
                return Unauthorized("Contraseña incorrecta.");

            return Ok(usuario.Id);
        }


        [HttpPost("alta")]
        public async Task<IActionResult> AltaUsuario([FromBody] UsuarioAltaDto request)
        {
            var (exito, mensaje) = await _usuarioService.AltaUsuario(request);
            if (!exito)
                return BadRequest(mensaje);

            return Ok(new { mensaje });
        }

        [HttpPut("inactivate/{id}")]
        public async Task<IActionResult> InactivateUsuario(int id, [FromBody] bool activo)
        {
            var (exito, mensaje) = await _usuarioService.InactivarUsuario(id, activo);
            if (!exito)
                return NotFound(mensaje);

            return Ok(new { mensaje });
        }

        [HttpPut("modificar/{idUsuario}")]
        public async Task<IActionResult> ModificarUsuario(int idUsuario, [FromBody] UsuarioModificarDto request)
        {
            var (exito, mensaje) = await _usuarioService.ModificarUsuario(idUsuario, request);
            if (!exito)
                return BadRequest(mensaje);

            return Ok(new { mensaje });
        }


    }
}
