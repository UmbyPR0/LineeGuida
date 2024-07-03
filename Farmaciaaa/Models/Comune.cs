using System;
using System.Collections.Generic;

namespace Farmaciaaa.Models;

public partial class Comune
{
    public int Id { get; set; }

    public int? IdProvincia { get; set; }

    public string? Denominazione { get; set; }

    public virtual ICollection<Farmacie> Farmacies { get; set; } = new List<Farmacie>();

    public virtual ICollection<Frazione> Fraziones { get; set; } = new List<Frazione>();

    public virtual Provincium? IdProvinciaNavigation { get; set; }
}
