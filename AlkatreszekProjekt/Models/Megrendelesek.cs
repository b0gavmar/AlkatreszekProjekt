using System;
using System.Collections.Generic;

namespace AlkatreszekProjekt.Models;

public partial class Megrendelesek
{
    public int? Id { get; set; }

    public int? MegrendeloId { get; set; }

    public string? Datum { get; set; }

    public string? Teljesitve { get; set; }
}
