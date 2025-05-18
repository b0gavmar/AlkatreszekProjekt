using System;
using System.Collections.Generic;

namespace AlkatreszekProjekt.Models;

public partial class Alkatreszek
{
    public int? Id { get; set; }

    public string? Nev { get; set; }

    public int? Ar { get; set; }

    public int? Raktaron { get; set; }

    public string? LaptopAlkatresz { get; set; }

    public int? BeszallitoId { get; set; }

    public int? KategoriaId { get; set; }
}
