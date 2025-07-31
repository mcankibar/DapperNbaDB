using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class DraftHistory
{
    public long PersonId { get; set; }

    public string? PlayerName { get; set; }

    public int Season { get; set; }

    public int? RoundNumber { get; set; }

    public int? RoundPick { get; set; }

    public int? OverallPick { get; set; }

    public string? DraftType { get; set; }

    public long? TeamId { get; set; }

    public string? TeamCity { get; set; }

    public string? TeamName { get; set; }

    public string? TeamAbbreviation { get; set; }

    public string? Organization { get; set; }

    public string? OrganizationType { get; set; }

    public int? PlayerProfileFlag { get; set; }

    public virtual Team? Team { get; set; }
}
