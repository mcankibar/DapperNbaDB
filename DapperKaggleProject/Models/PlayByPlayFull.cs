using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class PlayByPlayFull
{
    public string GameId { get; set; } = null!;

    public int Eventnum { get; set; }

    public int Eventmsgtype { get; set; }

    public int Eventmsgactiontype { get; set; }

    public int Period { get; set; }

    public string Wctimestring { get; set; } = null!;

    public string Pctimestring { get; set; } = null!;

    public string? Homedescription { get; set; }

    public string? Neutraldescription { get; set; }

    public string? Visitordescription { get; set; }

    public string? Score { get; set; }

    public string? Scoremargin { get; set; }

    public double? Person1type { get; set; }

    public long? Player1Id { get; set; }

    public string? Player1Name { get; set; }

    public string? Player1TeamId { get; set; }

    public string? Player1TeamCity { get; set; }

    public string? Player1TeamNickname { get; set; }

    public string? Player1TeamAbbreviation { get; set; }

    public double? Person2type { get; set; }

    public long? Player2Id { get; set; }

    public string? Player2Name { get; set; }

    public string? Player2TeamId { get; set; }

    public string? Player2TeamCity { get; set; }

    public string? Player2TeamNickname { get; set; }

    public string? Player2TeamAbbreviation { get; set; }

    public double? Person3type { get; set; }

    public long? Player3Id { get; set; }

    public string? Player3Name { get; set; }

    public string? Player3TeamId { get; set; }

    public string? Player3TeamCity { get; set; }

    public string? Player3TeamNickname { get; set; }

    public string? Player3TeamAbbreviation { get; set; }

    public int VideoAvailableFlag { get; set; }

    public virtual Game Game { get; set; } = null!;
}
