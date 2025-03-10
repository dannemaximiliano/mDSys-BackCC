using Back_CC.DTOs.MovimientoDTO;
using Back_CC.DTOs.UsuarioDTO;
using Back_CC.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_CC.Services
{
    public class MovimientoService
    {
        private readonly AppDbContext _context;

        public MovimientoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> NuevoMovimiento(MovimientoDto dto)
        {
            var movimiento = new MovimientosLog
            {
                IdPersona = dto.IdPersona,
                IdTipoOperacion = dto.IdTipoOperacion,
                Importe = dto.Importe,
                FechaOperacion = dto.FechaOperacion,
                Observacion = dto.Observacion,
                DiasDeEnsuenio = dto.DiasDeEnsuenio,
                IdUsuario = dto.IdUsuario
            };

            
            await _context.MovimientosLogs.AddAsync(movimiento);
            await _context.SaveChangesAsync();

            return (true);
        }


    }
}
