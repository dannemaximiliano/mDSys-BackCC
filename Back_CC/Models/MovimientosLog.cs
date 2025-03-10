using System;
using System.Collections.Generic;

namespace Back_CC.Models;

public partial class MovimientosLog
{
    public int Id { get; set; }

    public int IdPersona { get; set; }

    public int IdTipoOperacion { get; set; }

    public decimal Importe { get; set; }

    public DateTime FechaOperacion { get; set; }

    public string? Observacion { get; set; }

    public bool DiasDeEnsuenio { get; set; }

    public int IdUsuario { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual TipoOperacion IdTipoOperacionNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
