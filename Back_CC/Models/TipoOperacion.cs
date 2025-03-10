using System;
using System.Collections.Generic;

namespace Back_CC.Models;

public partial class TipoOperacion
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<MovimientosLog> MovimientosLogs { get; set; } = new List<MovimientosLog>();
}
