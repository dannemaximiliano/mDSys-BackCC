using System;
using System.Collections.Generic;

namespace Back_CC.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public int IdPersona { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdTipoUsuario { get; set; }

    public DateTime FechaAlta { get; set; }

    public bool Activo { get; set; }

    public DateTime? FechaBaja { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<MovimientosLog> MovimientosLogs { get; set; } = new List<MovimientosLog>();
}
