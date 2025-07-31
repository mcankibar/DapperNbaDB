using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class Team
{
    public long Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Abbreviation { get; set; } = null!;

    public string Nickname { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public int YearFounded { get; set; }

    public string? Arena { get; set; }
    public int? ArenaCapacity { get; set; }
    public string? Owner { get; set; }
    public string? GeneralManager { get; set; }
    public string? HeadCoach { get; set; }
    public string? DLeagueAffiliation { get; set; }

    public string LogoPath => GetLogoPath();

    private string GetLogoPath()
    {
        if (string.IsNullOrEmpty(Abbreviation))
            return "/logos/default.png";

        var logoName = Abbreviation.ToLowerInvariant() switch
        {
            "atl" => "atlanta",           // Atlanta Hawks
            "bos" => "boston",            // Boston Celtics
            "cle" => "cleveland",         // Cleveland Cavaliers
            "nop" => "neworleans",        // New Orleans Pelicans
            "chi" => "chicago",           // Chicago Bulls
            "dal" => "dallas",            // Dallas Mavericks
            "den" => "dever",             // Denver Nuggets
            "gsw" => "goldenstate",       // Golden State Warriors
            "hou" => "houston",           // Houston Rockets
            "lac" => "clipper",           // Los Angeles Clippers
            "lal" => "lakers",            // Los Angeles Lakers
            "mia" => "miami",             // Miami Heat
            "mil" => "milwaukee",         // Milwaukee Bucks
            "min" => "minnesota",         // Minnesota Timberwolves
            "bkn" => "nets",              // Brooklyn Nets
            "nyk" => "newyork",           // New York Knicks
            "orl" => "orlando",           // Orlando Magic
            "ind" => "pacers",            // Indiana Pacers
            "phi" => "philadelphia",      // Philadelphia 76ers
            "phx" => "phoenix",           // Phoenix Suns
            "por" => "portland",          // Portland Trail Blazers
            "sac" => "sacramento",        // Sacramento Kings
            "sas" => "spurs",             // San Antonio Spurs
            "okc" => "oklahoma",          // Oklahoma City Thunder
            "tor" => "toronto",           // Toronto Raptors
            "uta" => "utah",              // Utah Jazz
            "mem" => "memphis",           // Memphis Grizzlies
            "was" => "wizards",           // Washington Wizards
            "det" => "detroit",           // Detroit Pistons
            "cha" => "charlotte",         // Charlotte Hornets
            _ => Abbreviation.ToLowerInvariant()   // Fallback to abbreviation
        };

        return $"/logos/{logoName}.png";
    }

    public virtual ICollection<DraftHistory> DraftHistories { get; set; } = new List<DraftHistory>();

    public virtual ICollection<Game> GameTeamIdAwayNavigations { get; set; } = new List<Game>();

    public virtual ICollection<Game> GameTeamIdHomeNavigations { get; set; } = new List<Game>();

    public virtual TeamDetail? TeamDetail { get; set; }

    public virtual ICollection<TeamHistory> TeamHistories { get; set; } = new List<TeamHistory>();
}

public class TeamStats
{
    public long TeamId { get; set; }
    public string? TeamAbbreviation { get; set; }
    public int TotalGames { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public decimal WinPercentage { get; set; }
    public int HomeGames { get; set; }
    public int AwayGames { get; set; }
    public int TotalPoints { get; set; }
    public decimal AveragePoints { get; set; }
    public int TotalAssists { get; set; }
    public decimal AverageAssists { get; set; }
    public int TotalRebounds { get; set; }
    public decimal AverageRebounds { get; set; }
    public DateTime? LastUpdated { get; set; }
}
