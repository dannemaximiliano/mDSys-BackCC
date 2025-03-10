using Back_CC.DTOs.PersonaDTO;
using Back_CC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Back_CC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : Controller
    {
        private readonly PersonaService _personaService;

        public PersonaController(PersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpPost("alta")]
        public async Task<IActionResult> AltaPersona([FromBody] PersonaDto personaDto)
        {
            var (esValido, mensaje) = await _personaService.ValidarDatosPersona(personaDto);
            if (!esValido)
                return BadRequest(mensaje);

            var personaCreada = await _personaService.AltaPersona(personaDto);
            //return Ok(new { mensaje = "Persona creada exitosamente", persona = personaCreada });
            return Ok();
        }

        [HttpPut("inactivate/{id}")]
        public async Task<IActionResult> InactivatePersona(int id, [FromBody] bool activo)
        {
            var (exito, mensaje) = await _personaService.InactivarPersona(id, activo);
            if (!exito)
                return NotFound(mensaje);

            return Ok(new { mensaje });
        }

        [HttpPut("modificar/{id}/{username}")]
        public async Task<IActionResult> ModificarPersona(int id, string username, [FromBody] PersonaModificarDto request)
        {
            var (exito, mensaje) = await _personaService.ModificarPersona(id, request, username);
            if (!exito)
                return BadRequest(mensaje);

            return Ok(new { mensaje });
        }

        [HttpGet("listarPersonasActivas")]
        public async Task<IActionResult> ListarPersonasActivas()
        {
            var personas = await _personaService.ListarPersonasActivas();

            if (!personas.Any())
                return NotFound("No hay personas registradas.");

            return Ok(personas);
        }

        [HttpGet("listarPersonas")]
        public async Task<IActionResult> ListarPersonas()
        {
            var personas = await _personaService.ListarPersonas();

            if (!personas.Any())
                return NotFound("No hay personas registradas.");

            return Ok(personas);
        }



    }
}
