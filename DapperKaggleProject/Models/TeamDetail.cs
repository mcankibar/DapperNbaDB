using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class TeamDetail
{
    public long TeamId { get; set; }

    public string Abbreviation { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public int Yearfounded { get; set; }

    public string City { get; set; } = null!;

    public string? Arena { get; set; }

    public int? Arenacapacity { get; set; }

    public string? Owner { get; set; }

    public string? Generalmanager { get; set; }

    public string? Headcoach { get; set; }

    public string? Dleagueaffiliation { get; set; }

    public string? Facebook { get; set; }

    public string? Instagram { get; set; }

    public string? Twitter { get; set; }

    public virtual Team Team { get; set; } = null!;
}
