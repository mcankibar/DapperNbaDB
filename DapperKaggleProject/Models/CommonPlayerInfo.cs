using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class CommonPlayerInfo
{
    public long PersonId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DisplayFirstLast { get; set; } = null!;

    public string DisplayLastCommaFirst { get; set; } = null!;

    public string DisplayFiLast { get; set; } = null!;

    public string PlayerSlug { get; set; } = null!;

    public DateOnly? Birthdate { get; set; }

    public string? School { get; set; }

    public string? Country { get; set; }

    public string? LastAffiliation { get; set; }

    public string? Height { get; set; }

    public int? Weight { get; set; }

    public int? SeasonExp { get; set; }

    public string? Jersey { get; set; }

    public string? Position { get; set; }

    public string Rosterstatus { get; set; } = null!;

    public string GamesPlayedCurrentSeasonFlag { get; set; } = null!;

    public long TeamId { get; set; }

    public string? TeamName { get; set; }

    public string? TeamAbbreviation { get; set; }

    public string? TeamCode { get; set; }

    public string? TeamCity { get; set; }

    public string? Playercode { get; set; }

    public int? FromYear { get; set; }

    public int? ToYear { get; set; }

    public string DleagueFlag { get; set; } = null!;

    public string NbaFlag { get; set; } = null!;

    public string GamesPlayedFlag { get; set; } = null!;

    public string DraftYear { get; set; } = null!;

    public string? DraftRound { get; set; }

    public string? DraftNumber { get; set; }

    public virtual Player Person { get; set; } = null!;
}
