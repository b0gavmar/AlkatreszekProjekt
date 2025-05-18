using System;
using System.Collections.Generic;

namespace AlkatreszekProjekt.Models;

public partial class Megrendelok
{
    public int? Id { get; set; }

    public string? MegrendeloNev { get; set; }

    public string? Lakhely { get; set; }

    public string? Ferfi { get; set; }
}
