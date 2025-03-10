﻿using System;
using System.Collections.Generic;

namespace Back_CC.Models;

public partial class TipoUsuario
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
