using System;
using System.Collections.Generic;

namespace Back_CC.Models;

public partial class Persona
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Dni { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public DateTime FechaAlta { get; set; }

    public bool Activo { get; set; }

    public DateTime? FechaBaja { get; set; }

    public virtual ICollection<MovimientosLog> MovimientosLogs { get; set; } = new List<MovimientosLog>();

    public virtual Saldo? Saldo { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
