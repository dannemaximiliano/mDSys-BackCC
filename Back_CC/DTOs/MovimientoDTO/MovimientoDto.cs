namespace Back_CC.DTOs.MovimientoDTO
{
    public class MovimientoDto
    {
        public int IdPersona { get; set; }
        public int IdTipoOperacion { get; set; }
        public decimal Importe { get; set; }
        public DateTime FechaOperacion { get; set; }
        public string? Observacion { get; set; }
        public bool DiasDeEnsuenio { get; set; }
        public int IdUsuario { get; set; }

    }
}
