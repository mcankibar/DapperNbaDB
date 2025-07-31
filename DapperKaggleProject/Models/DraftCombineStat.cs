using System;
using System.Collections.Generic;

namespace DapperKaggleProject.Models;

public partial class DraftCombineStat
{
    public int? Season { get; set; }

    public long? PlayerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PlayerName { get; set; }

    public string? Position { get; set; }

    public decimal? HeightWoShoes { get; set; }

    public string? HeightWoShoesFtIn { get; set; }

    public decimal? HeightWShoes { get; set; }

    public string? HeightWShoesFtIn { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Wingspan { get; set; }

    public string? WingspanFtIn { get; set; }

    public decimal? StandingReach { get; set; }

    public string? StandingReachFtIn { get; set; }

    public decimal? BodyFatPct { get; set; }

    public decimal? HandLength { get; set; }

    public decimal? HandWidth { get; set; }

    public decimal? StandingVerticalLeap { get; set; }

    public decimal? MaxVerticalLeap { get; set; }

    public decimal? LaneAgilityTime { get; set; }

    public decimal? ModifiedLaneAgilityTime { get; set; }

    public decimal? ThreeQuarterSprint { get; set; }

    public int? BenchPress { get; set; }

    public string? SpotFifteenCornerLeft { get; set; }

    public string? SpotFifteenBreakLeft { get; set; }

    public string? SpotFifteenTopKey { get; set; }

    public string? SpotFifteenBreakRight { get; set; }

    public string? SpotFifteenCornerRight { get; set; }

    public string? SpotCollegeCornerLeft { get; set; }

    public string? SpotCollegeBreakLeft { get; set; }

    public string? SpotCollegeTopKey { get; set; }

    public string? SpotCollegeBreakRight { get; set; }

    public string? SpotCollegeCornerRight { get; set; }

    public string? SpotNbaCornerLeft { get; set; }

    public string? SpotNbaBreakLeft { get; set; }

    public string? SpotNbaTopKey { get; set; }

    public string? SpotNbaBreakRight { get; set; }

    public string? SpotNbaCornerRight { get; set; }

    public string? OffDribFifteenBreakLeft { get; set; }

    public string? OffDribFifteenTopKey { get; set; }

    public string? OffDribFifteenBreakRight { get; set; }

    public string? OffDribCollegeBreakLeft { get; set; }

    public string? OffDribCollegeTopKey { get; set; }

    public string? OffDribCollegeBreakRight { get; set; }

    public string? OnMoveFifteen { get; set; }

    public string? OnMoveCollege { get; set; }

    public virtual Player? Player { get; set; }
}
