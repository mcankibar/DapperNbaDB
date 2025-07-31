using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class GameSummary
{
    public DateOnly? GameDateEst { get; set; }

    public int? GameSequence { get; set; }

    public string GameId { get; set; } = null!;

    public int? GameStatusId { get; set; }

    public string? GameStatusText { get; set; }

    public string? Gamecode { get; set; }

    public long? HomeTeamId { get; set; }

    public long? VisitorTeamId { get; set; }

    public int? Season { get; set; }

    public int? LivePeriod { get; set; }

    public string? LivePcTime { get; set; }

    public string? NatlTvBroadcasterAbbreviation { get; set; }

    public string? LivePeriodTimeBcast { get; set; }

    public int? WhStatus { get; set; }

    public virtual Game Game { get; set; } = null!;
}
