using System;
using System.Collections.Generic;

namespace Farmaciaaa.Models;

public partial class Regione
{
    public int Id { get; set; }

    public string? Denominazione { get; set; }

    public virtual ICollection<Provincium> Provincia { get; set; } = new List<Provincium>();
}
