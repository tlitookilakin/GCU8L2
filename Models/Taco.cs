﻿using System.Text.Json.Serialization;

namespace TacoStand.Models;

public partial class Taco
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public float? Cost { get; set; }

    public bool? SoftShell { get; set; }

    public bool? Chips { get; set; }

    [JsonIgnore]
    public virtual ICollection<Combo> Combos { get; set; } = new List<Combo>();
}
