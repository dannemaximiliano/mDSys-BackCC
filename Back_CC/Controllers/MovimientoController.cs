using Back_CC.Services;
using Back_CC.DTOs.MovimientoDTO;
using Microsoft.AspNetCore.Mvc;

namespace Back_CC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly MovimientoService _movimientoService;

        public MovimientoController(MovimientoService movimientoService)
        {
            _movimientoService = movimientoService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> NuevoMovimiento([FromBody] MovimientoDto movimientoDto)
        {
            if (movimientoDto.Importe <= 0)
                return BadRequest("El importe debe ser mayor a 0.");

            var resultado = await _movimientoService.NuevoMovimiento(movimientoDto);

            if (!resultado)
                return StatusCode(500, "Error al registrar el movimiento.");

            return Ok("Movimiento registrado con éxito.");
        }
    }
}
