using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class LineScore
{
    public DateOnly? GameDateEst { get; set; }

    public int? GameSequence { get; set; }

    public string? GameId { get; set; }

    public long? TeamIdHome { get; set; }

    public string? TeamAbbreviationHome { get; set; }

    public string? TeamCityNameHome { get; set; }

    public string? TeamNicknameHome { get; set; }

    public string? TeamWinsLossesHome { get; set; }

    public int? PtsQtr1Home { get; set; }

    public int? PtsQtr2Home { get; set; }

    public int? PtsQtr3Home { get; set; }

    public int? PtsQtr4Home { get; set; }

    public int? PtsOt1Home { get; set; }

    public int? PtsOt2Home { get; set; }

    public int? PtsOt3Home { get; set; }

    public int? PtsOt4Home { get; set; }

    public int? PtsOt5Home { get; set; }

    public int? PtsOt6Home { get; set; }

    public int? PtsOt7Home { get; set; }

    public int? PtsOt8Home { get; set; }

    public int? PtsOt9Home { get; set; }

    public int? PtsOt10Home { get; set; }

    public int? PtsHome { get; set; }

    public long? TeamIdAway { get; set; }

    public string? TeamAbbreviationAway { get; set; }

    public string? TeamCityNameAway { get; set; }

    public string? TeamNicknameAway { get; set; }

    public string? TeamWinsLossesAway { get; set; }

    public int? PtsQtr1Away { get; set; }

    public int? PtsQtr2Away { get; set; }

    public int? PtsQtr3Away { get; set; }

    public int? PtsQtr4Away { get; set; }

    public int? PtsOt1Away { get; set; }

    public int? PtsOt2Away { get; set; }

    public int? PtsOt3Away { get; set; }

    public int? PtsOt4Away { get; set; }

    public int? PtsOt5Away { get; set; }

    public int? PtsOt6Away { get; set; }

    public int? PtsOt7Away { get; set; }

    public int? PtsOt8Away { get; set; }

    public int? PtsOt9Away { get; set; }

    public int? PtsOt10Away { get; set; }

    public int? PtsAway { get; set; }

    public virtual Game? Game { get; set; }

    public virtual Team? TeamIdAwayNavigation { get; set; }

    public virtual Team? TeamIdHomeNavigation { get; set; }
}
