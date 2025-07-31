using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class InactivePlayer
{
    public string? GameId { get; set; }

    public long? PlayerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? JerseyNum { get; set; }

    public long? TeamId { get; set; }

    public string? TeamCity { get; set; }

    public string? TeamName { get; set; }

    public string? TeamAbbreviation { get; set; }

    public virtual Game? Game { get; set; }

    public virtual Player? Player { get; set; }

    public virtual Team? Team { get; set; }
}
