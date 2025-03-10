namespace Back_CC.DTOs
{
    public class ModificarPersonaDto
    {
        public string Nombre { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telefono { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public string Usuario { get; set; } = null!; // Usuario que realiza la modificación
    }

}

