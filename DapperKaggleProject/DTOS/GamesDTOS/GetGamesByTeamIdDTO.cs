namespace DapperKaggleProject.DTOS.GamesDTOS
{
    public class GetGamesByTeamIdDTO
    {
        public int TeamId { get; set; }
        public List<GameDto> Games { get; set; } = new List<GameDto>();
        public GamesPaginationDTO Pagination { get; set; } = new GamesPaginationDTO();
        public DateTime? FilterStartDate { get; set; }
        public DateTime? FilterEndDate { get; set; }
        public string? FilterSeasonType { get; set; }
        
        public class GameDto
        {
            public string GameId { get; set; } = null!;
            public DateTime GameDate { get; set; }
            public int SeasonId { get; set; }
            public string SeasonType { get; set; } = null!;
            
            public long TeamIdHome { get; set; }
            public string TeamAbbreviationHome { get; set; } = null!;
            public string TeamNameHome { get; set; } = null!;
            public string MatchupHome { get; set; } = null!;
            public string WlHome { get; set; } = null!; 
            public int? PtsHome { get; set; }
            public int? FgmHome { get; set; }
            public int? FgaHome { get; set; }
            public double? FgPctHome { get; set; }
            public int? Fg3mHome { get; set; }
            public int? Fg3aHome { get; set; }
            public double? Fg3PctHome { get; set; }
            public int? FtmHome { get; set; }
            public int? FtaHome { get; set; }
            public double? FtPctHome { get; set; }
            public int? RebHome { get; set; }
            public int? AstHome { get; set; }
            public int? StlHome { get; set; }
            public int? BlkHome { get; set; }
            public int? TovHome { get; set; }
            public int? PfHome { get; set; }
            public int? PlusMinusHome { get; set; }

            
            public long TeamIdAway { get; set; }
            public string TeamAbbreviationAway { get; set; } = null!;
            public string TeamNameAway { get; set; } = null!;
            public string MatchupAway { get; set; } = null!;
            public string WlAway { get; set; } = null!; 
            public int? PtsAway { get; set; }
            public int? FgmAway { get; set; }
            public int? FgaAway { get; set; }
            public double? FgPctAway { get; set; }
            public int? Fg3mAway { get; set; }
            public int? Fg3aAway { get; set; }
            public double? Fg3PctAway { get; set; }
            public int? FtmAway { get; set; }
            public int? FtaAway { get; set; }
            public double? FtPctAway { get; set; }
            public int? RebAway { get; set; }
            public int? AstAway { get; set; }
            public int? StlAway { get; set; }
            public int? BlkAway { get; set; }
            public int? TovAway { get; set; }
            public int? PfAway { get; set; }
            public int? PlusMinusAway { get; set; }

            
            public bool IsHomeGame(long teamId) => TeamIdHome == teamId;
            public bool IsAwayGame(long teamId) => TeamIdAway == teamId;
            
            public string GetOpponent(long teamId)
            {
                return IsHomeGame(teamId) ? TeamAbbreviationAway : TeamAbbreviationHome;
            }
            
            public string GetOpponentName(long teamId)
            {
                return IsHomeGame(teamId) ? TeamNameAway : TeamNameHome;
            }
            
            public string GetResult(long teamId)
            {
                return IsHomeGame(teamId) ? WlHome : WlAway;
            }
            
            public int? GetTeamScore(long teamId)
            {
                return IsHomeGame(teamId) ? PtsHome : PtsAway;
            }
            
            public int? GetOpponentScore(long teamId)
            {
                return IsHomeGame(teamId) ? PtsAway : PtsHome;
            }

            public string GetMatchupDisplay(long teamId)
            {
                return IsHomeGame(teamId) ? MatchupHome : MatchupAway;
            }
            
            public string GetLocationIndicator(long teamId)
            {
                return IsHomeGame(teamId) ? "vs" : "@";
            }
            
            
            public string GetGameTypeIcon()
            {
                return SeasonType?.ToLower() switch
                {
                    "regular season" => "fas fa-calendar-alt",
                    "playoffs" => "fas fa-trophy",
                    "preseason" => "fas fa-clipboard-check",
                    _ => "fas fa-basketball-ball"
                };
            }
            
            public string GetResultColor(long teamId)
            {
                var result = GetResult(teamId);
                return result?.ToUpper() switch
                {
                    "W" => "text-success",
                    "L" => "text-danger",
                    _ => "text-muted"
                };
            }
        }
        
        public class GamesPaginationDTO
        {
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }
            public bool HasPreviousPage { get; set; }
            public bool HasNextPage { get; set; }
            
            public int StartRecord => ((CurrentPage - 1) * PageSize) + 1;
            public int EndRecord => Math.Min(CurrentPage * PageSize, TotalCount);
        }
    }
}
