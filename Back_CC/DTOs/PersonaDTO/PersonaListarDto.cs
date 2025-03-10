namespace Back_CC.DTOs.PersonaDTO
{
    public class PersonaListarDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telefono { get; set; }

    }
}
