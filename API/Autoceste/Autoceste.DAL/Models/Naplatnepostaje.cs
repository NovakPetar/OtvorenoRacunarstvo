using System;
using System.Collections.Generic;

namespace Autoceste.DAL.Models;

public partial class Naplatnepostaje
{
    public int? Autocestaid { get; set; }

    public int Id { get; set; }

    public string? Naziv { get; set; }

    public double? Geoduzina { get; set; }

    public double? Geosirina { get; set; }

    public bool? Imaenc { get; set; }

    public string? Kontakt { get; set; }

    public virtual Autoceste? Autocesta { get; set; }
}
