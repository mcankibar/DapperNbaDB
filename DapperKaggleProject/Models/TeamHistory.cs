using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class TeamHistory
{
    public long TeamId { get; set; }

    public string City { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public int YearFounded { get; set; }

    public int YearActiveTill { get; set; }

    public virtual Team Team { get; set; } = null!;
}
