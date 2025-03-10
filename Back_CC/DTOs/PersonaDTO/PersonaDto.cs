namespace Back_CC.DTOs.PersonaDTO
{
    public class PersonaDto
    {
        public string Nombre { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telefono { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
    }
}
