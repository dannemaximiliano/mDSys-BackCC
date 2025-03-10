namespace Back_CC.DTOs.UsuarioDTO
{
    public class UsuarioAltaDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdTipoUsuario { get; set; }

        public int IdPersona { get; set; } //Persona a la que pertenece el usuario
    }
}
