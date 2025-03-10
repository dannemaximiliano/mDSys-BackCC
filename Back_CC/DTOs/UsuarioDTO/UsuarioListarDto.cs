namespace Back_CC.DTOs.UsuarioDTO
{
    public class UsuarioListarDto
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdTipoUsuario { get; set; }
        public DateTime FechaAlta { get; set; }

    }
}
