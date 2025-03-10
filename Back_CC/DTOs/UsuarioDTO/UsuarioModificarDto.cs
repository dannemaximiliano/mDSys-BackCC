namespace Back_CC.DTOs.UsuarioDTO
{
    public class UsuarioModificarDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdTipoUsuario { get; set; }

    }
}
