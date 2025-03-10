using System;
using System.Collections.Generic;

namespace Back_CC.Models;

public partial class Saldo
{
    public int Id { get; set; }

    public int IdPersona { get; set; }

    public decimal Saldo1 { get; set; }

    public DateTime FechaUltimaAct { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;
}
