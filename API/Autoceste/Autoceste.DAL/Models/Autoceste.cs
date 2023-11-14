using System;
using System.Collections.Generic;

namespace Autoceste.DAL.Models;

public partial class Autoceste
{
    public int Id { get; set; }

    public double Duljina { get; set; }

    public string Oznaka { get; set; } = null!;

    public string? Neformalninaziv { get; set; }

    public string Dionica { get; set; } = null!;

    public virtual ICollection<Naplatnepostaje> Naplatnepostajes { get; set; } = new List<Naplatnepostaje>();
}
