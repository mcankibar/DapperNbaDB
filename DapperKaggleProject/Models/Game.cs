using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class Game
{
    public int? SeasonId { get; set; }

    public long? TeamIdHome { get; set; }

    public string? TeamAbbreviationHome { get; set; }

    public string? TeamNameHome { get; set; }

    public string GameId { get; set; } = null!;

    public DateOnly? GameDate { get; set; }

    public string? MatchupHome { get; set; }

    public string? WlHome { get; set; }

    public int? Min { get; set; }

    public decimal? FgmHome { get; set; }

    public decimal? FgaHome { get; set; }

    public decimal? FgPctHome { get; set; }

    public decimal? Fg3mHome { get; set; }

    public decimal? Fg3aHome { get; set; }

    public decimal? Fg3PctHome { get; set; }

    public decimal? FtmHome { get; set; }

    public decimal? FtaHome { get; set; }

    public decimal? FtPctHome { get; set; }

    public decimal? OrebHome { get; set; }

    public decimal? DrebHome { get; set; }

    public decimal? RebHome { get; set; }

    public decimal? AstHome { get; set; }

    public decimal? StlHome { get; set; }

    public decimal? BlkHome { get; set; }

    public decimal? TovHome { get; set; }

    public decimal? PfHome { get; set; }

    public decimal? PtsHome { get; set; }

    public int? PlusMinusHome { get; set; }

    public int? VideoAvailableHome { get; set; }

    public long? TeamIdAway { get; set; }

    public string? TeamAbbreviationAway { get; set; }

    public string? TeamNameAway { get; set; }

    public string? MatchupAway { get; set; }

    public string? WlAway { get; set; }

    public decimal? FgmAway { get; set; }

    public decimal? FgaAway { get; set; }

    public decimal? FgPctAway { get; set; }

    public decimal? Fg3mAway { get; set; }

    public decimal? Fg3aAway { get; set; }

    public decimal? Fg3PctAway { get; set; }

    public decimal? FtmAway { get; set; }

    public decimal? FtaAway { get; set; }

    public decimal? FtPctAway { get; set; }

    public decimal? OrebAway { get; set; }

    public decimal? DrebAway { get; set; }

    public decimal? RebAway { get; set; }

    public decimal? AstAway { get; set; }

    public decimal? StlAway { get; set; }

    public decimal? BlkAway { get; set; }

    public decimal? TovAway { get; set; }

    public decimal? PfAway { get; set; }

    public decimal? PtsAway { get; set; }

    public int? PlusMinusAway { get; set; }

    public int? VideoAvailableAway { get; set; }

    public string? SeasonType { get; set; }

    public int? Period { get; set; }
    public string? GameClock { get; set; }
    public string? GameStatusText { get; set; }

    public virtual GameInfo? GameInfo { get; set; }

    public virtual GameSummary? GameSummary { get; set; }

    public virtual Team? TeamIdAwayNavigation { get; set; }

    public virtual Team? TeamIdHomeNavigation { get; set; }
}
